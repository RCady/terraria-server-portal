using Terraria;
using Terraria.ModLoader;
using Terraria.Net;
using System.Net;
using System.IO;
using Terraria.ID;
using System.Linq;
using System.Threading;

namespace TestMod
{
	public class TestMod : Mod
	{
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            base.HandlePacket(reader, whoAmI);
            byte msgType = reader.ReadByte();

            switch (msgType) {
                case 69:
                    if (Main.netMode == NetmodeID.MultiplayerClient)
                    {
                        int length = reader.ReadInt32();
                        IPAddress ip = new IPAddress(reader.ReadBytes(length));
                        int port = reader.ReadInt32();

                        TcpAddress server = new TcpAddress(ip, port);
                        Main.NewText(string.Concat("Connecting to server: ", server.Address.ToString(), ":", server.Port.ToString()));

                        // Need to gracefully handle disconnection and then reconnect
                        if (Netplay.Connection.Socket.IsConnected())
                        {
                            Netplay.Disconnect = true;
                            Netplay.Connection.Socket.Close();

                            Thread.Sleep(200);

                            // Connect to the new server
                            Netplay.ListenPort = port;
                            Netplay.SetRemoteIP(ip.ToString());
                            Netplay.StartTcpClient();
                        }
                    }

                    break;
                default:
                    Logger.WarnFormat("TestMod: Unknown Message type: {0}", msgType);
                    break;
            }
        }

        private void connectToServer()
        {

        }
    }
}