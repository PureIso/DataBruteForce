using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace PortableLib
{
    public class oSortableBindingList<T> : BindingList<T>
    {
        List<T> _originalList;
        ListSortDirection _sortDirection;
        PropertyDescriptor _sortProperty;

        Action<oSortableBindingList<T>, List<T>> _populateBaseList = (a, b) => a.ResetItems(b);
        static Dictionary<string, Func<List<T>, IEnumerable<T>>> cachedOrderByExpressions = new Dictionary<string, Func<List<T>, IEnumerable<T>>>();

        public oSortableBindingList() {
            _originalList = new List<T>();
        }

        public oSortableBindingList(IEnumerable<T> enumerable) {
            _originalList = enumerable.ToList();
            _populateBaseList(this, _originalList);
        }

        public oSortableBindingList(List<T> list) {
            _originalList = list;
            _populateBaseList(this, _originalList);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, 
                                ListSortDirection direction) {
            /*
             Look for an appropriate sort method in the cache if not found .
             Call CreateOrderByMethod to create one. 
             Apply it to the original list.
             Notify any bound controls that the sort has been applied.
             */

            _sortProperty = prop;

            var orderByMethodName = _sortDirection == 
                ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

            if (!cachedOrderByExpressions.ContainsKey(cacheKey)) {
                CreateOrderByMethod(prop, orderByMethodName, cacheKey);
            }

            ResetItems(cachedOrderByExpressions[cacheKey](_originalList).ToList());
            ResetBindings();
            _sortDirection = _sortDirection == ListSortDirection.Ascending ? 
                            ListSortDirection.Descending : ListSortDirection.Ascending;
        }


        private void CreateOrderByMethod(PropertyDescriptor prop, 
                     string orderByMethodName, string cacheKey) {

            /*
             Create a generic method implementation for IEnumerable<T>.
             Cache it.
            */

            var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
            var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda =
                Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter, 
                                  accesedMember), lambdaParameter);
            var orderByMethod = typeof(Enumerable).GetMethods()
                                          .Where(a => a.Name == orderByMethodName &&
                                                       a.GetParameters().Length == 2)
                                          .Single()
                                          .MakeGenericMethod(typeof(T), prop.PropertyType);

            var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(
                                        Expression.Call(orderByMethod,
                                                new Expression[] { sourceParameter, 
                                                                   propertySelectorLambda }),
                                                sourceParameter);

            cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
        }

        protected override void RemoveSortCore() {
            ResetItems(_originalList);
        }

        private void ResetItems(List<T> items) {

            base.ClearItems();

            for (int i = 0; i < items.Count; i++) {
                base.InsertItem(i, items[i]);
            }
        }

        protected override bool SupportsSortingCore {
            get {
                // indeed we do
                return true;
            }
        }

        protected override ListSortDirection SortDirectionCore {
            get {
                return _sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore {
            get {
                return _sortProperty;
            }
        }

        protected override void OnListChanged(ListChangedEventArgs e) {
            _originalList = base.Items.ToList();
        }

    }
}
