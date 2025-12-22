using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Items;

public class CopperSwordOnAStick : ModItem {
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.DamageType = DamageClass.Melee;
        Item.damage = 3;
        Item.width = 80;
        Item.height = 80;
        Item.useTime = 7;
        Item.useAnimation = 7;
        Item.useStyle = ItemUseStyleID.Rapier;
        Item.knockBack = 2f;
        Item.value = 100;
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.crit = 1;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shootSpeed = 1f;
        Item.shoot = ModContent.ProjectileType<CopperSwordOnAStickProjectile>();
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.Wood, 5)
            .AddIngredient(ItemID.Rope, 5)
            .AddIngredient(ItemID.CopperShortsword)
            .Register();
    }
}
