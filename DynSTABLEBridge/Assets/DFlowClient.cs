using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class DFlowClient : MonoBehaviour {

	/// <summary>
	/// 	<para>
	/// 		The IP address of the D-Flow server.
	/// 	</para>
	/// 	<para>
	/// 		Default: 127.0.0.1 (localhost)
	/// 	</para>
	/// </summary>
	/// <remarks>
	/// 
	/// </remarks>
	public string IPAddress = "127.0.0.1";

	/// <summary>
	/// The Starting Port of the D-Flow server.
	/// </summary>
	public int port = 3910;

	/// <summary>
	/// Data structure to hold the packages that D-Flow sends
	/// </summary>
	public struct DFlowPackage {

		/// <summary>
		/// Enum to describe the type of the package.
		/// </summary>
		public enum PackageType {
			CLIENT_INIT = 0,
			SERVER_INIT = 1,
			UPDATE = 2
		};

		/// <summary>
		/// The type of the package
		/// </summary>
		public PackageType packageType;

		/// <summary>
		/// The number of inputs
		/// </summary>
		public int numberInputs;
        /// <summary>
        /// The number of outputs
        /// </summary>
		public int numberOutputs;
        /// <summary>
        /// And index that identifies this client to the D-Flow server. It is assigned when establishing a connection to the D-Flow server.
        /// </summary>
		public int clientIndex;
        /// <summary>
        /// A name that the client can register to the server.
        /// </summary>
		public string clientName;
        /// <summary>
        /// The floating point values inputted into the networking module in D-Flow.
        /// </summary>
		public float[] data;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DFlowClient.DFlowPackage"/> struct from a byte buffer.
        /// The buffer must be formatted in the way specified by the D-Flow Networking Documentation.
        /// </summary>
        /// <param name="buffer">The buffer to be constructed from. Must be of length 1296.</param>
        public DFlowPackage(byte[] buffer) {
            if (buffer.Length == 1296) {
                
                packageType = (PackageType)System.BitConverter.ToInt32(buffer, 0);
                numberInputs = System.BitConverter.ToInt32(buffer, 4);
                numberOutputs = System.BitConverter.ToInt32(buffer, 8);
                clientIndex = System.BitConverter.ToInt32(buffer, 12);
                clientName = System.Text.Encoding.UTF8.GetString(buffer, 16, 256);

                data = new float[256];

                for (int i = 0; i < 256; i++) {
                    if (System.BitConverter.IsLittleEndian){
                        System.Array.Reverse(buffer, 272 + i * 4, sizeof(float));
                    }
                    data[i] = System.BitConverter.ToSingle(buffer, 272 + i * 4);
                }

            } else {
                //TODO: Handle the case where the buffer isn't of the right length
                //Right now just fill it with empty values

                packageType = PackageType.CLIENT_INIT;
                numberInputs = 0;
                numberOutputs = 0;
                clientIndex = 0;
                clientName = "";
                data = new float[256];
                for (int i = 0; i < 256; i++)
                {
                    data[i] = 0.0f;
                }
            }
        }

        //TODO: Implement this
        //public byte[] ToBuffer() {
        //    byte[] buffer = new byte[1296];

        //    System.BitConverter.to

        //    return buffer;
        //}
	};

    /// <summary>
    /// Enum to describe the current connection status to the D-Flow server
    /// </summary>
    public enum DFlowConnectionStatus
    {
        /// <summary>
        /// Not connected yet
        /// </summary>
        DISCONNECTED = 0,
        /// <summary>
        /// A request to the initial port <paramref name="port"/> has been made. No clientIndex yet.
        /// </summary>
        ESTABLISHING = 1,
        /// <summary>
        /// The connection is live and sending values.
        /// </summary>
        LIVE = 2,
        /// <summary>
        /// No updated values for longer than timeoutInterval.
        /// </summary>
        TIMEOUT = 3,
        /// <summary>
        /// Unknown error.
        /// </summary>
        ERROR = 4
    }

    public int clientIndex;
    //TODO: Properly name these properties, maybe to server/client channels
    private int numberOfOutputs = 0;
    private int numberOfInputs = 0;

    public Socket establishingSocket;
    public NetworkStream establishingStream;
    public byte[] establishingPackageBuffer = new byte[1296];

    public Socket clientSocket;
    public NetworkStream clientStream;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	/// <summary>
    /// Conntect to the D-Flow server specified by <paramref name="IPAddress"/> and <paramref name="port"/>
    /// </summary>
	public void Connect() {
        establishingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        establishingSocket.BeginConnect(IPAddress, port, new System.AsyncCallback(EstablishingConnectCallback), this);
	}

    /// <summary>
    /// Disconnect any connections.
    /// </summary>
    public void Disconnect() {
        if((establishingSocket != null) && (establishingSocket.Connected)) {
            //TODO: Disconnect the establishing socket
        }
    }

    private static void EstablishingConnectCallback(System.IAsyncResult asyncResult) {

        DFlowClient client = (DFlowClient)asyncResult.AsyncState;

        if (client.establishingSocket.Connected) {
            Debug.Log("Connection to D-Flow server established.");

            client.establishingStream = new NetworkStream(client.establishingSocket);

            if (client.establishingStream.CanRead) {
                client.establishingStream.BeginRead(client.establishingPackageBuffer, 0, 1296, new System.AsyncCallback(EstablishingReadCallback), client);
            }
        } else {
            Debug.Log("Connection to D-Flow server couldn't be established.");
        }
    }

    private static void EstablishingReadCallback(System.IAsyncResult asyncResult) {

        DFlowClient client = (DFlowClient)asyncResult.AsyncState;

        DFlowPackage package = new DFlowPackage(client.establishingPackageBuffer);

        if (package.packageType == DFlowPackage.PackageType.SERVER_INIT) {
            client.clientIndex = package.clientIndex;
            client.numberOfInputs = package.numberInputs;
            client.numberOfOutputs = package.numberOutputs;

            Debug.Log("Successfully created a D-Flow session with client index " + client.clientIndex);

            client.establishingStream.Close();
            client.establishingSocket.BeginDisconnect(false, new System.AsyncCallback(EstablishingDisconnectCallback), client);
        }
    }

    private static void EstablishingDisconnectCallback(System.IAsyncResult asyncResult) {

        DFlowClient client = (DFlowClient)asyncResult.AsyncState;

        //Nothing to do here really.

    }
}
