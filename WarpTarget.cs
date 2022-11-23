using System.Net;
using Terraria.Net;

namespace TestMod
{
    public struct WarpTarget
    {
        public WarpTarget(string name, TcpAddress address)
        {
            Name = name;
            Address = address;
        }

        public WarpTarget(string name, string ip, int port)
        {
            Name = name;
            IPAddress ipAddress = IPAddress.Parse(ip);
            Address = new TcpAddress(ipAddress, port);
        }

        public string Name;
        public TcpAddress Address;
    }
}
