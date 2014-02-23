using System;
using System.Reflection;
using System.Windows.Forms;

namespace PortableLib.Controls
{
    public class oDoubleBufferedDataGridView : DataGridView
    {
        private int _currentSelectedIndex;
        private int _currentScrollRowIndex;
        private int _currentScrollColumnIndex;

        new void DoubleBuffered(bool setting)
        {
            Type type = this.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(this, setting, null);
        }

        public void GetCurrentSelectedIndex()
        {
            if (this.Rows.Count <= 0) return;
            _currentSelectedIndex = this.CurrentCell.RowIndex;
            _currentScrollRowIndex = this.FirstDisplayedScrollingRowIndex;
            _currentScrollColumnIndex = this.FirstDisplayedScrollingColumnIndex;
        }

        public void SetCurrentSelectedIndex()
        {
            if (_currentSelectedIndex == -1) _currentSelectedIndex = 0;
            if (_currentScrollRowIndex == -1) _currentScrollRowIndex = 0;
            if (_currentScrollColumnIndex == -1) _currentScrollColumnIndex = 0;
            if (this.Rows.Count <= _currentSelectedIndex) _currentSelectedIndex = 0;
            this.Rows[_currentSelectedIndex].Selected = true;
            this.FirstDisplayedScrollingRowIndex = _currentScrollRowIndex;
            this.FirstDisplayedScrollingColumnIndex = _currentScrollColumnIndex;
        }

        public void SetDataSource(dynamic obj)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker) (delegate
                {
                    SetDataSource(obj);
                }));
            else
            {
                this.DataSource = null;
                this.DataSource = obj;
            }
        }

        public oDoubleBufferedDataGridView()
        {
            this.DoubleBuffered(true);
        }
    }
}
