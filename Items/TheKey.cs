using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace YHTMod.Items; 

public class TheKey : ModItem {
    
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("The Key");
        Tooltip.SetDefault("Opens all locks.");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

}