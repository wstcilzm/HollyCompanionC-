using System;
using System.Collections.Generic;
using System.Text;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using HollyCompanion.Utility;
using System.IO;
using Windows.UI.Popups;
using SocketComm;

namespace HollyCompanion
{
    public class HttpConnection
    {
        private StreamSocket streamSocket = null;
        private static UDPSocketListener listener = null;
        private static string strServcieIp = null;
        private static bool IsListener = false;

        public HttpConnection()
        {
            try
            {
                if (listener == null)
                {
                    listener = new UDPSocketListener(9000);
                    listener.MessageReceived += listener_MessageReceived;
                }
            }
            catch(Exception ex)
            {

            }
        }

        //public async void HttpInitiallize()
        //{
        //    streamSocket = new StreamSocket();
        //    try
        //    {
        //        await streamSocket.ConnectAsync(new HostName(GetServiceHostIp()), GetPort());
        //    }
        //    catch(Exception ex)
        //    {

        //    }
          
        //}

        public async Task<uint> WriteRequest(string requestData)
        {
            using (var writer = new DataWriter(streamSocket.OutputStream))
            {
                writer.WriteUInt32(writer.MeasureString(requestData));
                writer.WriteString(requestData);
                return await writer.StoreAsync();           
            }         
        }

        private System.Threading.ManualResetEvent m_manEvent = new System.Threading.ManualResetEvent(false);

        public async Task<string> GetResponse(string requestData)
        {
            if (strServcieIp == null) 
            {
                m_manEvent.WaitOne();
            }
            string result = string.Empty;
            try
            {
                if (streamSocket == null)
                {
                    streamSocket = new StreamSocket();
                    HostName hostName = new HostName(GetIP(strServcieIp));
                    await streamSocket.ConnectAsync(hostName, GetPort(strServcieIp));
                }
                if (HttpConnection.IsListener == false)
                {
                    listener.Close();
                    HttpConnection.IsListener = true;
                }

                var writer = new DataWriter(streamSocket.OutputStream);
                writer.WriteUInt32(writer.MeasureString(requestData));
                writer.WriteString(requestData);
                await writer.StoreAsync();
                writer.DetachStream();

                var reader = new DataReader(streamSocket.InputStream);
                await reader.LoadAsync(sizeof(UInt32));
                UInt32 strLength = reader.ReadUInt32();
                await reader.LoadAsync(strLength);
                result = reader.ReadString(strLength);
                reader.DetachStream();
            }
            catch (Exception ex)
            {

            }      
            return result;
        }

        
        

        private void listener_MessageReceived(object sender, DataReceiveEventArgs e)
        {
            strServcieIp = Encoding.UTF8.GetString(e.datas, 0, e.datas.Length);
            m_manEvent.Set();
            //listener.SendToRemote("STOPBROAD", null);
        }

        public string GetPort(string str)
        {
            try
            {
                return str.Substring(str.IndexOf(':') + 1, str.Length - str.IndexOf(':')-1);
            }
            catch(Exception ex)
            {
                return string.Empty;
            }
        }

        public string GetIP(string str)
        {
            return str.Substring(0, str.IndexOf(':'));
        }

    }
}
