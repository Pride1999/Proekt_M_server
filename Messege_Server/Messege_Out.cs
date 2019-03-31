using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Messege_Server
{
    class Messege_Out
    {
        private static UdpClient udpclient;
        private static IPAddress multicastaddress;
        private static IPEndPoint remoteep;
        public  void Messeges(string messege, int id)
        {
            Chat( id);
            try
            {
                Byte[] buffer = Encoding.UTF8.GetBytes(messege);

                Byte[] data = Encoding.UTF8.GetBytes(messege);

                udpclient.Send(data, data.Length, remoteep);
            }
            catch (Exception ex)
            {
               //TextB1.Text += "Not Connect To Client!\r\n";
            }
        }
        public static void Chat(int id)
        {
            multicastaddress = IPAddress.Parse("239.0.0.222");
            udpclient = new UdpClient();
            udpclient.JoinMulticastGroup(multicastaddress);
            remoteep = new IPEndPoint(multicastaddress, id);
        }
 
    }
}
