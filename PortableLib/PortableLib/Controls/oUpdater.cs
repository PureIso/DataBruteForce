using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PortableLib.Controls
{
    public partial class oUpdater : UserControl
    {
        private string _xmlFilePath;
        private string _lastestVersionNodeName;
        private string _downloadNodeName;
        private string _exeName;
        private Form _parent;
        public oUpdater(Form parent,string xmlFilePath,string lastestVersionNodeName, string downloadNodeName,string exeName)
        {
            InitializeComponent();
            _xmlFilePath = xmlFilePath;                         //"http://www.olaegbe.com/athleticsDatabase.xml"
            _lastestVersionNodeName = lastestVersionNodeName;   //"latestVersion"
            _downloadNodeName = downloadNodeName;               //"latestDownload"
            _parent = parent;
            _exeName = exeName;
        }

        private void oUpdater_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(UpdateWork);
        }

        private void UpdateWork(object obj)
        {
            try
            {
                //progress bar continues progress
                oDelegateFunctions.SetProgressBarMarqueeAnimationSpeed(progressBar,30);
                //xml location
                var xmlReader = XmlReader.Create(_xmlFilePath);
                //read and stop at lateVersion element
                xmlReader.Read();
                xmlReader.ReadToFollowing(_lastestVersionNodeName);
                //Read the contents of latest version to xmlVersion string
                var xmlVersion = xmlReader.ReadString();
                xmlReader.ReadToFollowing(_downloadNodeName);
                var xmlDownloadLink = xmlReader.ReadString();
                xmlReader.Close();

                //comparing version
                if (String.CompareOrdinal(xmlVersion, Application.ProductVersion) > 0)
                {
                    if (oDelegateFunctions.MessageBoxShow(_parent, @"Update found! " + Environment.NewLine + @"New Version: " + xmlVersion +
                        Environment.NewLine + @"Would you like to download?", @"Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        var webclient = new WebClient();
                        //=========================================
                        //Rename the current exe to temp
                        var newPath = Path.Combine(Application.StartupPath, "temp");
                        if (File.Exists(newPath)) File.Delete(newPath);
                        File.Move(Application.ExecutablePath, newPath);
                        //==========================================
                        //Download file
                        webclient.DownloadFileAsync(new Uri(xmlDownloadLink), _exeName);
                        webclient.DownloadProgressChanged += (s, ev) =>
                        {
                            oDelegateFunctions.SetControlText(statusLabel,"Updating... " + ev.ProgressPercentage + "% Complete");
                        };
                        webclient.DownloadFileCompleted += (s, ev) =>
                        {
                            webclient.Dispose();
                            oDelegateFunctions.MessageBoxShow(_parent,
                            @"File Downloaded!", @"Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //Create a new process containing the information for our updated version.
                            var proccess = new System.Diagnostics.Process
                            {
                                //Start the new updated process
                                StartInfo = new System.Diagnostics.ProcessStartInfo(Application.ExecutablePath)
                            };
                            //We then start the process.
                            proccess.Start();
                            //And kill the current.
                            System.Diagnostics.Process.GetCurrentProcess().Kill();
                        };
                    }
                }
                else
                {
                    oDelegateFunctions.MessageBoxShow(_parent,@"No Update Found", @"Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _parent.Invoke((MethodInvoker) (delegate
                    {
                        _parent.Close();
                    }));
                }
            }
            catch (Exception ex)
            {
                oDelegateFunctions.MessageBoxShow(_parent,ex.Message, @"Update", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _parent.Invoke((MethodInvoker)(delegate
                {
                    _parent.Close();
                }));
            }
        }
    }
}
