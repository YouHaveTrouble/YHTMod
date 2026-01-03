using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Items;

public class SummonersAmbition : ModItem
{
    public override void SetStaticDefaults()
    {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override LocalizedText Tooltip => Language.GetText("");

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.White;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        ItemID.Sets.ShimmerTransformToItem[Type] = 0;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
    {
        var modPlayer = player.GetModPlayer<YhtPlayer>();
        modPlayer.SummonerAmbition = true;
        modPlayer.HasAmbition = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips)
    {
        var player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
        tooltips.Add(new TooltipLine(Mod, "SummonerAmbition", Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Tooltip", player.GetSummonersAmbitionMinionBonus())));

        if (player.KilledBosses.Contains("king_slime"))
        {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionKingSlime", Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.KingSlime")));
        }

    }

    public override void AddRecipes()
    {
        CreateRecipe()
            .AddIngredient(ItemID.Rope, 5)
            .AddIngredient(ItemID.Squirrel)
            .AddIngredient(ItemID.Acorn, 25)
            .AddTile(TileID.WorkBenches)
            .Register();

        #region Upgrades

        CreateRecipe()
            .AddIngredient(this)
            .AddTile(TileID.DemonAltar)
            .AddIngredient(ItemID.Gel, 250)
            .AddCondition(new Condition(
                Language.GetText("Mods.YHTMod.Recipes.Ambitions.KingSlimeDead"),
                () =>
                {
                    var player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
                    return player.KilledBosses.Contains("king_slime") && !player.SummonerAmbitionCrafts.Contains("king_slime");
                })
            )
            .AddOnCraftCallback((recipe, item, consumed, destination) =>
            {
                Main.LocalPlayer.GetModPlayer<YhtPlayer>().SummonerAmbitionCrafts.Add("king_slime");
            })
            .Register();
        
        CreateRecipe()
            .AddIngredient(this)
            .AddTile(TileID.DemonAltar)
            .AddIngredient(ItemID.Lens, 100)
            .AddCondition(new Condition(Language.GetText(
                    "Mods.YHTMod.Recipes.Ambitions.EyeOfCthulhuDead"),
                () =>
                {
                    var player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
                    return player.KilledBosses.Contains("eye_of_cthulhu") && !player.SummonerAmbitionCrafts.Contains("eye_of_cthulhu");
                })
            )
            .AddOnCraftCallback((recipe, item, consumed, destination) =>
            {
                Main.LocalPlayer.GetModPlayer<YhtPlayer>().SummonerAmbitionCrafts.Add("eye_of_cthulhu");
            })
            .Register();
        
        CreateRecipe()
            .AddIngredient(this)
            .AddTile(TileID.DemonAltar)
            .AddIngredient(ItemID.Bone, 250)
            .AddCondition(new Condition(Language.GetText(
                    "Mods.YHTMod.Recipes.Ambitions.SkeletronDead"),
                () => {
                    var player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
                    return player.KilledBosses.Contains("skeletron") && !player.SummonerAmbitionCrafts.Contains("skeletron");
                })
            )
            .AddOnCraftCallback((recipe, item, consumed, destination) =>
            {
                Main.LocalPlayer.GetModPlayer<YhtPlayer>().SummonerAmbitionCrafts.Add("skeletron");
            })
            .Register();

        #endregion
    }
}
