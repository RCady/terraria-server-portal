using System.Linq;
using System.Net;
using System.Net.Sockets;
using Terraria;
using Terraria.ModLoader;
using Terraria.Net;
using Terraria.Net.Sockets;
using Terraria.ID;
using Terraria.GameContent.NetModules;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing;
using System;
using System.Collections.Generic;
using Ionic.Zlib;

namespace TestMod
{
    internal class WarpCommand : ModCommand
    {
        public override string Command => "warp";
        public override CommandType Type => CommandType.Server;

        public List<WarpTarget> warpTargets = new List<WarpTarget>();

        private void setWarpTargets()
        {
            string[] lines = System.IO.File.ReadAllLines(Path.Combine(System.AppContext.BaseDirectory, "warps.txt"));
            foreach (string line in lines)
            {
                string[] items = line.Split(" ");
                WarpTarget target = new WarpTarget(items[0], items[1], int.Parse(items[2]));
                warpTargets.Add(target);
            }
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            setWarpTargets();

            if (args.Length > 0)
            {
                if (args[0].ToLower() == "list")
                {
                    string[] lines = System.IO.File.ReadAllLines(Path.Combine(System.AppContext.BaseDirectory, "warps.txt"));
                    foreach (string line in lines)
                    {
                        caller.Reply(line, Colors.RarityYellow);
                    }
                } else
                {
                    string warpName = string.Join(" ", args);

                    caller.Reply(warpTargets[0].Name);

                    if (warpTargetExists(warpName.Trim()))
                    {
                        WarpTarget warpTarget = getWarpTarget(warpName.Trim());
                        SendWarpToPlayer(caller.Player, warpTarget);
                    }
                }
            }
        }

        private void SendWarpToPlayer(Player player, WarpTarget target)
        {
            ModPacket warpPacket = Mod.GetPacket();
            warpPacket.Write((byte) 69);
            warpPacket.Write(target.Address.Address.GetAddressBytes().Length);
            warpPacket.Write(target.Address.Address.GetAddressBytes());
            warpPacket.Write(target.Address.Port);
            warpPacket.Send();
        }

        private Boolean warpTargetExists(string warpName)
        {
            foreach (WarpTarget target in warpTargets)
            {
                if (target.Name.ToLower() == warpName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        private WarpTarget getWarpTarget(string warpName)
        {
            return warpTargets.Find(target => target.Name.ToLower() == warpName.ToLower());
        }
    }
}
