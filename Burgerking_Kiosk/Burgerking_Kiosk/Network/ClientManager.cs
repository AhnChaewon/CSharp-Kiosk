using Burgerking_Kiosk.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Burgerking_Kiosk.Network
{
    class ClientManager
    {

        public class StateObject
        {
            // Client socket.  
            public Socket workSocket = null;
            // Size of receive buffer.  
            public const int BufferSize = 4096;
            // Receive buffer.  
            public byte[] buffer = new byte[BufferSize];
        }

        private static AsyncCallback receiveCallback;
        private static AsyncCallback sendCallback;

        static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static IPEndPoint end = new IPEndPoint(IPAddress.Parse("10.80.162.152"), 80);

        public static void start()
        {
            receiveCallback = new AsyncCallback(handleDataReceive);
            sendCallback = new AsyncCallback(handleDataSend);
            try
            {
                client.Connect(end);
                StateObject ao = new StateObject();
                ao.workSocket = client;

                client.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, receiveCallback, ao);

            }
            catch (Exception ex)
            {
                MessageBox.Show("서버와 연결 중 오류가 발생하였습니다.");
                Console.WriteLine(ex);
            }
        }

        public static void close()
        {
            try
            {
                client.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }

        public static void sendMessage(String message, int type)
        {
            StateObject ao = new StateObject();
            ao.buffer = sendMsg(type, message);
            ao.workSocket = client;

            // 전송 시작!
            try
            {
                client.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, sendCallback, ao);
            }
            catch (Exception ex)
            {
                MessageBox.Show("메세지 전송 중 오류가 발생하였습니다!");
                Console.WriteLine("전송 중 오류 발생!\n메세지: {0}", ex.Message);
            }
        }

        private static void handleDataReceive(IAsyncResult ar)
        {
            StateObject ao = (StateObject)ar.AsyncState;
            int recvBytes;

            try
            {
                recvBytes = ao.workSocket.EndReceive(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            if (recvBytes > 0)
            {
                Byte[] msgByte = new Byte[recvBytes];
                Array.Copy(ao.buffer, msgByte, recvBytes);
                String msg = Encoding.UTF8.GetString(msgByte);

                if (!msg.Equals("200"))
                {
                    MessageBox.Show(msg);
                }

                
                // 받은 메세지를 출력
                Console.WriteLine("메세지 받음: {0}", Encoding.UTF8.GetString(msgByte));
            }

            try
            {
                ao.workSocket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, receiveCallback, ao);
            }
            catch (Exception ex)
            {
                MessageBox.Show("수신 대기 중 오류가 발생하였습니다!");
                Console.WriteLine("자료 수신 대기 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }
        }
        private static void handleDataSend(IAsyncResult ar)
        {

            StateObject ao = (StateObject)ar.AsyncState;

            int sentBytes;

            try
            {
                sentBytes = ao.workSocket.EndSend(ar);
            }
            catch (Exception ex)
            {
                MessageBox.Show("메세지 전송 중 오류가 발생하였습니다!");
                Console.WriteLine("자료 송신 도중 오류 발생! 메세지: {0}", ex.Message);
                return;
            }

            if (sentBytes > 0)
            {
                Byte[] msgByte = new Byte[sentBytes];
                Array.Copy(ao.buffer, msgByte, sentBytes);

                Console.WriteLine("메세지 보냄: {0}", Encoding.UTF8.GetString(msgByte));
            }
        }


        public static byte[] sendMsg(int type, String str)
        {
            var json = new JObject();
            json.Add("MSGType", type);
            json.Add("Id", 2111);
            json.Add("Content", str);
            json.Add("ShopName", "");
            json.Add("OrderNumber", "");
            json.Add("Group", false);
            json.Add("Menus", null);

            String msg = JsonConvert.SerializeObject(json);
            byte[] b = Encoding.UTF8.GetBytes(msg);
            return b;

        }

    }
}
