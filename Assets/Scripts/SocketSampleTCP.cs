using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;

namespace SocketTest
{
	/// <summary>
	/// .
	/// </summary>
	public class SocketSampleTCP : MonoBehaviour
	{
		[SerializeField]
		private string address;

		[SerializeField]
		private int port;

		private Socket listener;

		private Socket socket;

		private bool isConnected = false;

		void OnGUI()
		{
			if( GUILayout.Button( "Run Server" ) )
			{
				this.StartListener( this.port );
			}
		}

		/// <summary>
		/// TCPの待ち受けを開始する.
		/// </summary>
		/// <param name="port">Port.</param>
		private void StartListener( int port )
		{
			this.listener = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			this.listener.Bind( new IPEndPoint( IPAddress.Any, port ) );
			this.listener.Listen( 1 );
			Debug.Log( "Run Server" );
		}

		private void AcceptClient()
		{
			if( this.listener != null && this.listener.Poll( 0, SelectMode.SelectRead ) )
			{
				this.socket = this.listener.Accept();
				this.isConnected = true;
			}
		}

		private void ClientProcess()
		{
			this.socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			this.socket.NoDelay = true;
			this.socket.SendBufferSize = 0;
			this.socket.Connect( this.address, this.port );
		}
	}
}