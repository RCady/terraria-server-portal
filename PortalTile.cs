using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TestMod
{
    internal class PortalTile : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileFrameImportant[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 18 };
            TileObjectData.addTile(Type);
            AddMapEntry(new Color(200, 200, 200));
        }

        public override bool RightClick(int i, int j)
        {
            NetMessage.

            return true;
        }
    }
}
