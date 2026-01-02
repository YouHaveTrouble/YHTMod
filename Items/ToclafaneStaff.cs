using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YHTMod.Buffs;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Items;

public class ToclafaneStaff : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] =
            true; // This lets the player target anywhere on the whole screen while using a controller.
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults()
    {
        Item.damage = 30;
        Item.knockBack = 3f;
        Item.mana = 10;
        Item.width = 32;
        Item.height = 32;
        Item.useTime = 36;
        Item.useAnimation = 36;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.value = Item.buyPrice(0, 30, 0, 0);
        Item.rare = ItemRarityID.Cyan;
        Item.UseSound = SoundID.Item44;
        Item.noMelee = true;
        Item.DamageType = DamageClass.Summon;
        Item.buffType = ModContent.BuffType<ToclafaneMinionBuff>();
        Item.shoot = ModContent.ProjectileType<ToclafaneMinion>();
    }

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity,
        int type, int damage, float knockback)
    {
        player.AddBuff(Item.buffType, 2);
        position = Main.MouseWorld;
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }

    public override void AddRecipes()
    {
        var watchGroup = new RecipeGroup(() => Language.GetTextValue("Mods.YHTMod.Recipes.AnyWatch"),
            ItemID.GoldWatch,
            ItemID.SilverWatch,
            ItemID.TinWatch,
            ItemID.CopperWatch,
            ItemID.PlatinumWatch,
            ItemID.TungstenWatch
        );
        RecipeGroup.RegisterGroup("YHTMod:Watches", watchGroup);

        var tier2BarGroup = new RecipeGroup(() => Language.GetTextValue("Mods.YHTMod.Recipes.Tier2Bars"),
            ItemID.MythrilBar,
            ItemID.OrichalcumBar
        );
        RecipeGroup.RegisterGroup("YHTMod:Tier2Bars", tier2BarGroup);

        var recipe = CreateRecipe();

        recipe.AddRecipeGroup(watchGroup);
        recipe.AddIngredient(ItemID.GuideVoodooDoll);
        recipe.AddTile(TileID.MythrilAnvil);

        // Calamity delays when you obtain hallowed bars, so use alternate recipe if Calamity is installed
        if (ModLoader.HasMod("CalamityMod"))
        {
            recipe.AddRecipeGroup(tier2BarGroup, 35);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
        }
        else
        {
            recipe.AddIngredient(ItemID.HallowedBar, 15);
        }

        recipe.Register();
    }
}