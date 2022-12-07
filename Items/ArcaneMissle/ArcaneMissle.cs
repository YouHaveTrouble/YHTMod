using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace YHTMod.Items.ArcaneMissle; 

public class ArcaneMissle : ModItem {

    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Arcane Missle");
        Tooltip.SetDefault("Magic damage crits have a chance to shoot a homing arcane missle");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.width = 64;
        Item.height = 64;
        Item.accessory = true;
        Item.damage = 10;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {

        player.GetModPlayer<YhtPlayer>().arcaneMissle = true;
        
        base.UpdateAccessory(player, hideVisual);
    }
}