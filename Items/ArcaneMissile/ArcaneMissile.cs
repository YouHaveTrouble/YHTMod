using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Items.ArcaneMissile;

public class ArcaneMissile : ModItem {
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.width = 32;
        Item.height = 32;
        Item.accessory = true;
        Item.damage = 10;
        Item.rare = ItemRarityID.LightRed;
        Item.DamageType = DamageClass.Magic;
        Item.noMelee = true;
        Item.noUseGraphic = true;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {
        player.GetModPlayer<YhtPlayer>().ArcaneMissile = Item.damage;

        base.UpdateAccessory(player, hideVisual);
    }
}
