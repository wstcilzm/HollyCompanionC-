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
    /// <summary>
    /// 
    /// </summary>
    public class UDPBroadCast:UDPSocket
    {
        private static readonly string BROADCASTHN = "255.255.255.255";
        /// <summary>
        /// 
        /// </summary>
        public UDPBroadCast(int udpport=45555):base(new HostName(BROADCASTHN),udpport)
        {

        }
    }
}
