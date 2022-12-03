using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Buffs;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Items; 

public class ToclafaneStaff : ModItem {
    
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Toclafane Staff");
        Tooltip.SetDefault("Summons a toclafane to remove population for you");
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
    }
    
    public override void SetDefaults() {
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

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
        player.AddBuff(Item.buffType, 2);
        position = Main.MouseWorld;
        return base.Shoot(player, source, position, velocity, type, damage, knockback);
    }
    
}