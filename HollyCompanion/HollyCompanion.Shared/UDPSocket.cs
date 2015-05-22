using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.System.Threading;
using Windows.Storage.Streams;

namespace SocketComm
{
    public class UDPSocket
    {
        public event EventHandler<DataReceiveEventArgs> MessageReceived;
        private int m_udpPort;
        private DatagramSocket m_socket;
        private DataWriter m_writer = null;
        private HostName m_host;
        /// <summary>
        /// 
        /// </summary>
        public UDPSocket(HostName host,int port) 
        {
            m_udpPort = port;
            m_socket = new DatagramSocket();
            m_socket.MessageReceived += m_socket_MessageReceived;
            m_host = host;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void m_socket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {
            DataReader reader = args.GetDataReader();
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
        /// <summary>
        public async void SendBytes(byte[] datas)
        {
            try
            {
                if (m_writer == null) 
                {
                   m_writer = new DataWriter(await m_socket.GetOutputStreamAsync(m_host, m_udpPort.ToString()));
                }
                m_writer.WriteUInt32((UInt32)datas.Length);
                m_writer.WriteBytes(datas);
                await m_writer.StoreAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        public void SendString(string strMsg, Encoding encode)
        {
            if (encode == null)
            {
                encode = Encoding.UTF8;
            }
            byte[] datas = encode.GetBytes(strMsg);
            SendBytes(datas);
        }
        /// <summary>
        /// 
        /// </summary>
        public void Close() 
        {
            if (m_socket!=null) 
            {
                m_socket.Dispose();
            }
            if (m_writer != null) 
            {
                m_writer.Dispose();
            }
        }


    }
}
