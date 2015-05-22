using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace SocketComm
{
    /// <summary>
    /// 
    /// </summary>
    public class DataReceiveEventArgs:EventArgs
    {
        public byte[] datas;
        public DatagramSocket socket;
    }
}
