using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;

namespace PortableLib
{
    public class oFtp
    {
        private string password;
        private string userName;
        private string defaultUri;
        private string uri;
        private int bufferSize = 1024;

        public bool Passive = true;
        public bool Binary = true;
        public bool EnableSsl = false;
        public bool Hash = false;

        public oFtp(string uri, string userName, string password)
        {
            this.uri = uri;
            this.defaultUri = uri;
            this.userName = userName;
            this.password = password;
        }

        public bool ResetUri()
        {
            try
            {
                uri = defaultUri;
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string AppendFile(string source, string destination)
        {
            var request = createRequest(combine(uri, destination), WebRequestMethods.Ftp.AppendFile);
            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            return getStatusDescription(request);
        }

        public string ChangeWorkingDirectory(string path)
        {
            uri = combine(uri, path);

            return PrintWorkingDirectory();
        }

        public string DeleteFile(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.DeleteFile);
            return getStatusDescription(request);
        }

        public string DownloadFile(string source, string dest)
        {
            var request = createRequest(combine(uri, source), WebRequestMethods.Ftp.DownloadFile);
            byte[] buffer = new byte[bufferSize];

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(dest, FileMode.OpenOrCreate))
                    {
                        int readCount = stream.Read(buffer, 0, bufferSize);

                        while (readCount > 0)
                        {
                            if (Hash)
                                Console.Write("#");

                            fs.Write(buffer, 0, readCount);
                            readCount = stream.Read(buffer, 0, bufferSize);
                        }
                    }
                }

                return response.StatusDescription;
            }
        }

        public Bitmap StreamImage(string imageName)
        {
            var request = WebRequest.Create(combine(uri, imageName));
            request.Credentials = new NetworkCredential(userName, password);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                if (stream != null)
                {
                    Image img;
                    using (img = Image.FromStream(stream))
                    {
                    
                        //img.Save("foo.jpg", ImageFormat.Jpeg);
                        return new Bitmap(img);
                    }
                }
            }
            return null;
        }
        
        public DateTime GetDateTimestamp(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetDateTimestamp);
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.LastModified;
            }
        }

        public string GetFileSize(string fileName)
        {
            var request = createRequest(combine(uri, fileName), WebRequestMethods.Ftp.GetFileSize);
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return oDelegateFunctions.GetSize(response.ContentLength);
            }
        }

        public List<string> StreamText(string imageName)
        {
            List<string> stringbuilder = new List<string>();
            var request = WebRequest.Create(combine(uri, imageName));
            request.Credentials = new NetworkCredential(userName, password);

            using (var response = request.GetResponse())
            using (var stream = response.GetResponseStream())
            {
                if (stream == null) return stringbuilder;
                using (StreamReader file = new StreamReader(stream))
                {
                    while (stream.CanRead)
                    {
                        stringbuilder.Add(file.ReadLine());
                    }
                }
                return stringbuilder;
            }
        }

        public string[] ListDirectory()
        {
            var list = new List<string>();
            try
            {
                var request = createRequest(WebRequestMethods.Ftp.ListDirectory);
                using (FtpWebResponse  response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        if(stream != null)
                            using (var reader = new StreamReader(stream))
                            {
                                while (!reader.EndOfStream)
                                {
                                    list.Add(reader.ReadLine());
                                }
                            }
                    }
                }
            }
            catch{}
            

            return list.ToArray();
        }

        public string[] ListDirectoryDetails()
        {
            var list = new List<string>();
            var request = createRequest(WebRequestMethods.Ftp.ListDirectoryDetails);

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(stream, true))
                    {
                        while (!reader.EndOfStream)
                        {
                            list.Add(reader.ReadLine());
                        }
                    }
                }
            }

            return list.ToArray();
        }

        public string MakeDirectory(string directoryName)
        {
            var dir = combine(uri, directoryName);


            var request = createRequest(dir, WebRequestMethods.Ftp.MakeDirectory);
            request.Credentials = new NetworkCredential(userName, password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }


            return getStatusDescription(request);
        }

        public string PrintWorkingDirectory()
        {
            var request = createRequest(WebRequestMethods.Ftp.PrintWorkingDirectory);

            return getStatusDescription(request);
        }

        public string RemoveDirectory(string directoryName)
        {
            var request = createRequest(combine(uri, directoryName), WebRequestMethods.Ftp.RemoveDirectory);
            return getStatusDescription(request);
        }

        public string Rename(string currentName, string newName)
        {
            var request = createRequest(combine(uri, currentName), WebRequestMethods.Ftp.Rename);
            request.RenameTo = newName;

            return getStatusDescription(request);
        }

        public string UploadFile(string source)
        {
            var b = Path.GetFileName(source);
            var request = createRequest(combine(uri, Path.GetFileName(source)), WebRequestMethods.Ftp.UploadFile);
            //Force clean up
            GC.Collect();

            using (var stream = request.GetRequestStream())
            {
                FileStream fs = File.OpenRead(source);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
                
                stream.Write(buffer, 0, buffer.Length);
                stream.Close();
            }
            return getStatusDescription(request);
        }

        public string UploadFile(string source, string destination)
        {
            var request = createRequest(combine(uri, destination), WebRequestMethods.Ftp.UploadFile);
            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            return getStatusDescription(request);
        }

        public bool MakeDirectoryLongUri(string Path)
        {          
            try
            {
                string buildUp ="";
                var dirs = Path.Split(new[] { "/"}, StringSplitOptions.None);
                foreach (var dir in dirs)
                {
                    if (dir == "" || dir == " ") continue;
                    var k = uri;                //Current directory
                    var s = ListDirectory();    //List all folders in current dir
                    if (s.Contains(dir))
                    {
                        buildUp += dir + "/";
                        ResetUri();
                        ChangeWorkingDirectory(buildUp);
                    }
                    else
                    {
                        MakeDirectory(dir);
                        buildUp += dir + "/";
                        ResetUri();
                        ChangeWorkingDirectory(buildUp);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string UploadFileWithUniqueName(string source)
        {
            var request = createRequest(WebRequestMethods.Ftp.UploadFileWithUniqueName);
            using (var stream = request.GetRequestStream())
            {
                using (var fileStream = System.IO.File.Open(source, FileMode.Open))
                {
                    int num;

                    byte[] buffer = new byte[bufferSize];

                    while ((num = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (Hash)
                            Console.Write("#");

                        stream.Write(buffer, 0, num);
                    }
                }
            }

            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return Path.GetFileName(response.ResponseUri.ToString());
            }
        }

        private FtpWebRequest createRequest(string method)
        {
            return createRequest(uri, method);
        }

        private FtpWebRequest createRequest(string uri, string method)
        {
            var r = (FtpWebRequest)WebRequest.Create(uri);

            r.Credentials = new NetworkCredential(userName, password);
            r.Method = method;
            r.UseBinary = Binary;
            r.EnableSsl = EnableSsl;
            r.UsePassive = Passive;

            return r;
        }

        private string getStatusDescription(FtpWebRequest request)
        {
            using (var response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
            }
        }

        private string combine(string path1, string path2)
        {
            return Path.Combine(path1, path2).Replace("\\", "/");
        }
    }
}
