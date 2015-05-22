using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;

namespace SocketComm
{
    /// <summary>
    /// 
    /// </summary>
    public class UDPSocketListener
    {
        private DatagramSocket m_socket = null;
        private int m_port;
        public event EventHandler<DataReceiveEventArgs> MessageReceived;
        private string m_remoteIp = "";
        private string m_remotePort = "";
        private DataWriter m_writer = null;
        private DatagramSocket m_SendRemoteSocket = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public UDPSocketListener(int port) 
        {
            m_socket = new DatagramSocket();
            m_socket.MessageReceived += m_socket_MessageReceived;
            m_port = port;
            BindToPort();
        }
        /// <summary>
        /// 
        /// </summary>
        private async void BindToPort() 
        {
            await m_socket.BindServiceNameAsync(m_port.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void m_socket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            DataReader reader = args.GetDataReader();
            if (m_remoteIp == "") 
            {
                m_remoteIp = args.RemoteAddress.ToString();
            }
            if (m_remotePort == "") 
            {
               m_remotePort=args.RemotePort;
            }
            while (true)
            {
                UInt32 dataLength = 0;
                if (reader.UnconsumedBufferLength >= sizeof(UInt32))
                {
                    dataLength = reader.ReadUInt32();
                }
                if (reader.UnconsumedBufferLength >= dataLength)
                {
                    byte[] datasReceive = new byte[dataLength];
                    reader.ReadBytes(datasReceive);
                    if (MessageReceived != null)
                    {
                        DataReceiveEventArgs eventArgs = new DataReceiveEventArgs();
                        eventArgs.datas = datasReceive;
                        eventArgs.socket = sender;
                        MessageReceived(this, eventArgs);
                    }
                }
                return;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datas"></param>
        public async void SendToRemote(byte[] datas) 
        {
            if (m_SendRemoteSocket == null) 
            {
                m_SendRemoteSocket = new DatagramSocket();
                if (m_remoteIp != "" && m_remotePort != "")
                {
                    await m_SendRemoteSocket.ConnectAsync(new HostName(m_remoteIp), m_remotePort);
                }
                else 
                {
                    throw new Exception("Not get the remorteIp or remotePort now!");
                }
                if (m_writer == null) 
                {
                    m_writer = new DataWriter(m_SendRemoteSocket.OutputStream);
                }
            }
            m_writer.WriteUInt32((UInt32)datas.Length);
            m_writer.WriteBytes(datas);
            await m_writer.StoreAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        /// <param name="encode"></param>
        public void SendToRemote(string strMsg, Encoding encode) 
        {
            if (encode == null) 
            {
                encode = Encoding.UTF8;
            }
            byte[] datas = encode.GetBytes(strMsg);
            SendToRemote(datas);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close() 
        {
            if (m_socket != null) 
            {
                m_socket.Dispose();
            }
            if (m_SendRemoteSocket != null) 
            {
                m_SendRemoteSocket.Dispose();
            }
        }
    }
}
