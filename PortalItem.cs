using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TestMod
{
    internal class PortalItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("This is awesome");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.createTile = ModContent.TileType<PortalTile>();
            Item.width = 10;
            Item.height = 10;
            Item.scale = 0.25f;
            Item.value = 500;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DirtBlock);
            recipe.Register();
        }
    }
}
