using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace chat
{
    public class UdpCommunicator : IDisposable
    {
        private UdpClient udpClient;
        private IPAddress groupAddress;
        private int port;

        public UdpCommunicator(string ip, int port)
        {
            this.groupAddress = IPAddress.Parse(ip);
            this.port = port;
            udpClient = new UdpClient();


            udpClient.JoinMulticastGroup(groupAddress);
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, port));
        }


        public void SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            IPEndPoint endPoint = new IPEndPoint(groupAddress, port);
            udpClient.Send(buffer, buffer.Length, endPoint);
        }


        public string ReceiveMessage()
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = udpClient.Receive(ref remoteEndPoint);
            return Encoding.UTF8.GetString(buffer);
        }


        public void LeaveGroup(string username)
        {
            string exitMessage = $"{username} left the room";
            SendMessage(exitMessage);
            udpClient.DropMulticastGroup(groupAddress);
        }


        public void Dispose()
        {
            if (udpClient != null)
            {
                udpClient.Close();
                udpClient = null;
            }
        }
    }
}