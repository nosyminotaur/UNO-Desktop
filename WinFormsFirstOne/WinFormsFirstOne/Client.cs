using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsFirstOne
{
	class Client
	{
		public IPAddress ipAddress { get; set; }
		public int port { get; set; }
		private TcpClient ClientSocket { get; set; }
		private NetworkStream ServerStream;

		public Client(IPAddress ipAddress, int port)
		{
			this.ipAddress = ipAddress;
			this.port = port;
		}

		public bool Start()
		{
			this.ClientSocket = new TcpClient();
			bool flag = true;
			try
			{
				ClientSocket.Connect(ipAddress, port);
			}
			catch (Exception e)
			{
				flag = false;
				Debug.WriteLine(e.ToString());
			}
			return flag;
		}

		public bool SetServerStream()
		{
			bool flag = true;
			try
			{
				ServerStream = ClientSocket.GetStream();
			}
			catch (Exception e)
			{
				flag = false;
				Debug.WriteLine(e.ToString());
			}
			return flag;
		}

		public bool SendData(byte[] data)
		{
			bool flag = true;
			try
			{
				ServerStream.Write(data, 0, data.Length);
				ServerStream.Flush();
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
				flag = false;
			}
			return flag;
		}

		public string ReceiveData()
		{
			string ReturnData = null;
			try
			{
				byte[] InStream = new byte[4096];
				int InStreamSize = ServerStream.Read(InStream, 0, InStream.Length);
				ReturnData = System.Text.Encoding.ASCII.GetString(InStream, 0, InStreamSize);
			}
			catch (Exception e)
			{
				Debug.WriteLine(e.ToString());
			}
			return ReturnData;
		}

		public void Close()
		{
			ClientSocket.Close();
		}
	}
}
