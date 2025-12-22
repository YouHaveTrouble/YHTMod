using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Items;

public class MithrilPebbleOfPigSmiting : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.width = 8;
        Item.height = 8;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2f;
        Item.value = 100;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.crit = 2;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shootSpeed = 20f;
        Item.shoot = ModContent.ProjectileType<MithrilPebbleOfPigSmitingProjectile>();
    }
}