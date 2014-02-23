using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PortableLib.Controls
{
    public class oDoubleBufferedTreeView : TreeView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
            base.OnHandleCreated(e);
        }

        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
        private const int TVS_EX_DOUBLEBUFFER = 0x0004;
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        new void DoubleBuffered(bool setting)
        {
            Type type = this.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);
            propertyInfo.SetValue(this, setting, null);
        }

        public oDoubleBufferedTreeView()
        {
            this.DoubleBuffered(true);
        }

        public TreeNode FindNodeWithText(string text)
        {
            TreeNode result = null;
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(delegate
                {
                    result = FindNodeWithText(text);
                }));
            else
            {
                foreach (TreeNode node in this.Nodes)
                {
                    if (node.Text == text)
                    {
                        return node;
                    }
                }
            }
            return result;
        }

        public TreeNode FindNodeWithName(string name)
        {
            TreeNode result = null;
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)(delegate
                {
                    result = FindNodeWithName(name);
                }));
            else
            {
                foreach (TreeNode node in this.Nodes)
                {
                    if (node.Name == name)
                    {
                        return node;
                    }
                }
            }
            return result;
        }
    }
}
