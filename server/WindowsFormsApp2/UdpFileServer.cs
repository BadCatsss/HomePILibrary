using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;
using System.Diagnostics;
using System.Threading;

namespace WindowsFormsApp2
{
    public class UdpFileServer
    {
        // Информация о файле (требуется для получателя)
        [Serializable]
        public class FileDetails
        {
            public string FILETYPE = "";
            public long FILESIZE = 0;
        }

        public static FileDetails fileDet = new FileDetails();

        // Поля, связанные с UdpClient
        public static IPAddress remoteIPAddress;
        public const int remotePort = 5002;
        public static UdpClient sender = new UdpClient();
        public static IPEndPoint endPoint;

        // Filestream object
        public static FileStream fs;
    }
}
