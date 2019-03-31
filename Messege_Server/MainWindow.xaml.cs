using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Messege_Server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread myThread ;
        bool Server_start = false;
        Start_Stop SS = new Start_Stop();
        Work_DB Db = new Work_DB();



        public MainWindow()
        {
            InitializeComponent();
        
        }

            private void Server_start_stop_Click(object sender, RoutedEventArgs e)
        {
            if (myThread == null)
                myThread = new Thread(Count);

            if (!Server_start)
            {
                SS.X("Start_Server");
                myThread.Start();
                Server_start = !Server_start;
                Server_start_stop.Content = "Stop";
                myThread.Priority = ThreadPriority.Highest;

            }
            else
            {
                Server_start_stop.IsEnabled = false;
                SS.X("Stop_Server");
                Server_start = !Server_start;
                Server_start_stop.Content = "Start";
                myThread = null;
            }           

        }
        public class Start_Stop{
            private string x;
            public void X(string X) { this.x = X; }
            public string X( ) { return this.x; }

    }
        public  void Count( )
        {

            IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
            UdpClient client = new UdpClient();
            client.ExclusiveAddressUse = false;
            IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 1);

            client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            client.ExclusiveAddressUse = false;

            client.Client.Bind(localEp);

            client.JoinMulticastGroup(multicastaddress);


            string formatted_data;

            while (true)
            {
                
                Byte[] data = client.Receive(ref localEp);
                formatted_data = Encoding.UTF8.GetString(data);
                Dispatcher.BeginInvoke((Action)(() => this.TextB1.Text += formatted_data + "\r\n"));


            }


            //const int PORT_NO = 27015;
            //const string SERVER_IP = "127.0.0.1";
            //string BekLine = null;
            //IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            //TcpListener listener = new TcpListener(localAdd, PORT_NO);
            //while (true)
            //{

            //    if (SS.X() == "Stop_Server")
            //    {
            //        Dispatcher.BeginInvoke((Action)(() => Server_start_stop.IsEnabled = true));
            //        break;
            //    }
            //    listener.Start();
            //    Dispatcher.BeginInvoke((Action)(() => this.TextB1.Text += "listener\r\n"));
            //    TcpClient client = listener.AcceptTcpClient();
            //    NetworkStream nwStream = client.GetStream();

            //    byte[] buffer = new byte[client.ReceiveBufferSize];
            //    int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);
            //    string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //   // Console.WriteLine("Received : " + dataReceived);
            //    Dispatcher.BeginInvoke((Action)(() => this.TextB1.Text += "Received:"+dataReceived+"\r\n"));

            //    BekLine = "  ------- ";
            //    //BekLine += Db.print_users();
            //    //MessageBox.Show(Db.print_users());

            //    Dispatcher.BeginInvoke((Action)(() => this.TextB1.Text += "Sending back :" + BekLine +"\r\n"));

            //    //Console.WriteLine("Sending back : " + BekLine);

            //    byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(BekLine);

            //    nwStream.Write(bytesToSend, 0, bytesToSend.Length);
            //    client.Close();
            //    listener.Stop();
            //    // Console.ReadLine();
            //}

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Test_B_Click(object sender, RoutedEventArgs e)
        {
            TextB1.Text += "Table Users \r\n";
            TextB1.Text += Db.print_users();
            Db.add_users("log","pass","name","tel",2);
            TextB1.Text += "ADD Users \r\n";
            TextB1.Text += Db.print_users();
            Db.update_users(2,"XX","","","",-1);
            Db.update_users(2,"","DD","","",-1);
            Db.update_users(2,"","","FF","",-1);
            Db.update_users(2,"","","","TT",1);
            TextB1.Text += "Upd Users \r\n";
            TextB1.Text += Db.print_users();
            Db.del_users( 2);
            TextB1.Text += "Del Users \r\n";
            TextB1.Text += Db.print_users();
            TextB1.Text += " \r\n";

        }
    }
}
