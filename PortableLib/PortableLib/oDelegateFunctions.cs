using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Windows.Forms;

namespace PortableLib
{
    public static class oDelegateFunctions
    {
        public static string GetSize(double value)
        {
            if (value < 1024)
                return value + " bytes";
            value = Math.Round(value / 1024);
            if (value < 1024)
                return value + " KB";
            value = Math.Round(value / 1024);
            if (value < 1024)
                return value + " MB";
            value = Math.Round(value / 1024);
            if (value < 1024)
                return value + " GB";
            value = Math.Round(value / 1024);
            if (value < 1024)
                return value + " TB";
            throw new Exception("Unknown Size !");
        }

        public static string GetMachineIP()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily != AddressFamily.InterNetwork)
                        continue;
                    localIP = ip.ToString();
                    break;
                }
            return localIP;
        }

        public static void ChangeSelectedTabControlIndex(TabControl control, int index)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { ChangeSelectedTabControlIndex(control, index); });
            else
            {
                control.SelectedIndex = index;
            }
        }

        public static void CheckRadioButton(RadioButton control, bool isChecked)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { CheckRadioButton(control, isChecked); });
            else
            {
                control.Checked = isChecked;
            }
        }

        public static void CheckCheckBoxButton(CheckBox control, bool isChecked)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { CheckCheckBoxButton(control, isChecked); });
            else
            {
                control.Checked = isChecked;
            }
        }

        public static void ClearComboBoxItem(ComboBox control)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { ClearComboBoxItem(control); });
            else
            {
                control.DataSource = null;
                control.Items.Clear();
            }
        }

        public static void DisposeUserControl(UserControl userControl)
        {
            if (userControl == null) return;
            if (userControl.InvokeRequired)
                userControl.Invoke((MethodInvoker)delegate { DisposeUserControl(userControl); });
            else
            {
                userControl.Dispose();
            }
        }

        public static DialogResult MessageBoxShow(Form form, string message,string header, MessageBoxButtons button, MessageBoxIcon icon)
        {
            DialogResult result = DialogResult.OK;
            try
            {
                if (form.InvokeRequired)
                    form.Invoke((MethodInvoker)delegate { result = MessageBoxShow(form, message,header, button, icon); });

                else return MessageBox.Show(form, message, header, button, icon);
            }
            catch (ObjectDisposedException e)
            {
                Console.WriteLine(e.Message);
            }
            return result;
        }

        #region Setter

        public static void SetCheckBoxCheck(CheckBox checkbox, bool isChecked)
        {
            if (checkbox.InvokeRequired)
                checkbox.Invoke((MethodInvoker)delegate { SetCheckBoxCheck(checkbox, isChecked); });
            else
            {
                checkbox.Checked = isChecked;
            }
        }

        public static void SetNumericUpDownValues(NumericUpDown numericUpDown,int min, int max, int value)
        {
            if (numericUpDown.InvokeRequired)
                numericUpDown.Invoke(
                    (MethodInvoker) delegate { SetNumericUpDownValues(numericUpDown, min, max, value); });
            else
            {
                numericUpDown.Minimum = min;
                numericUpDown.Maximum = max;
                numericUpDown.Value = value;
            }
        }
        public static void SetControlForeColor(Control control, Color color)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetControlForeColor(control, color); });
            else
                control.ForeColor = color;
        }

        public static void SetControlBackColor(Control control, Color color)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetControlBackColor(control, color); });
            else
            {
                control.BackColor = color;
            }
        }

        public static void SetControlText(Control control, string text)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetControlText(control, text); });
            else
            {
                control.Text = text;
            }
        }

        public static void SetControlImage(Control control, Image bitmap)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetControlImage(control, bitmap); });
            else
            {
                control.BackgroundImage = bitmap;
                control.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        public static void SetComboBoxSelectedIndex(ComboBox control, int index)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetComboBoxSelectedIndex(control, index); });
            else
            {
                control.SelectedIndex = index;
            }
        }

        public static void SetNumericUpDown(NumericUpDown control, decimal val)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetNumericUpDown(control, val); });
            else
            {
                if (val > control.Maximum) val = 0;
                control.Value = val;
            }
        }

        public static void SetControlVisible(Control control, bool isVisible)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetControlVisible(control, isVisible); });
            else
            {
                control.Visible = isVisible;
            }
        }

        public static void SetInformationText(TextBox control, string text)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetInformationText(control, text); });
            else
            {
                control.AppendText(Environment.NewLine + "[ " + DateTime.Now + " ]   " + text);
            }
        }

        public static void SetEnableControl(Control control, bool enable)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { SetEnableControl(control, enable); });
            else
            {
                control.Enabled = enable;
            }
        }

        public static void SetProgressBarMarqueeAnimationSpeed(ProgressBar progressbar, int value)
        {
            if (progressbar.InvokeRequired)
                progressbar.Invoke((MethodInvoker)
                    (() => SetProgressBarMarqueeAnimationSpeed(progressbar, value)));
            else
            {
                progressbar.MarqueeAnimationSpeed = value;
            }
        }

        public static void SetLog(string text)
        {
            string filePath =
                Assembly.GetExecutingAssembly().Location.Replace("PortableLib.dll", "") +
                "LogFile.txt";
            // Create a writer and open the file:
            StreamWriter log = !File.Exists(filePath) ? new StreamWriter(filePath) : File.AppendText(filePath);
            // Write to the file:
            log.WriteLine(DateTime.Now + "    " + text);
            // Close the stream:
            log.Close();
        }
        #endregion

        #region Getter
        public static int GetComboBoxSelectedIndex(ComboBox control)
        {
            int returnVal = -1;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { returnVal = GetComboBoxSelectedIndex(control); });
            else
            {
                return control.SelectedIndex;
            }
            return returnVal;
        }

        public static decimal GetNumericUpDown(NumericUpDown control)
        {
            decimal returnVal = 0;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { returnVal = GetNumericUpDown(control); });
            else
            {
                return control.Value;
            }
            return returnVal;
        }

        public static string GetControlText(Control control)
        {
            string returnValue = null;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { returnValue = GetControlText(control); });
            else
            {
                return control.Text;
            }
            return returnValue;
        }

        public static bool GetCheckBoxCheck(CheckBox checkbox)
        {
            bool returnValue = false;
            if (checkbox.InvokeRequired)
                checkbox.Invoke((MethodInvoker)delegate { returnValue = GetCheckBoxCheck(checkbox); });
            else
            {
                return checkbox.Checked;
            }
            return returnValue;
        }

        public static bool GetEnableControl(Control control)
        {
            bool returnValue = false;
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { returnValue = GetEnableControl(control); });
            else
            {
                return control.Enabled;
            }
            return returnValue;
        }
        #endregion

        #region Reset
        public static void ResetButtonDefaultColor(ButtonBase control)
        {
            if (control.InvokeRequired)
                control.Invoke((MethodInvoker)delegate { ResetButtonDefaultColor(control); });
            else
            {
                control.BackColor = default(Color);
                control.UseVisualStyleBackColor = true;
            }
        }

        public static void ResetPanelDefaultColor(Panel panel)
        {
            if (panel.InvokeRequired)
                panel.Invoke((MethodInvoker)delegate { ResetPanelDefaultColor(panel); });
            else
            {
                panel.BackColor = default(Color);
            }
        }
        #endregion
    }
}
