using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Items;

public class SummonersAmbition : ModItem {
    public override void SetStaticDefaults() {
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override LocalizedText Tooltip => Language.GetText("");

    public override void SetDefaults() {
        Item.width = 32;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.White;
        Item.noMelee = true;
        Item.noUseGraphic = true;
        ItemID.Sets.ShimmerTransformToItem[Type] = 0;
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        modPlayer.SummonerAmbition = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips) {
        YhtPlayer player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
        tooltips.Add(new TooltipLine(Mod, "SummonerAmbition",
            Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Tooltip",
                player.GetSummonersAmbitionMinionBonus())));

        if (player.SummonerAmbitions.Contains("king_slime")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionKingSlime",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.KingSlime")));
        }

        if (player.SummonerAmbitions.Contains("eye_of_cthulhu")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionEyeOfCthulhu",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.EyeOfCthulhu")));
        }

        if (player.SummonerAmbitions.Contains("deerclops")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionDeerclops",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Deerclops")));
        }

        if (player.SummonerAmbitions.Contains("eater_of_worlds")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionEaterOfWorlds",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.EaterOfWorlds")));
        }

        if (player.SummonerAmbitions.Contains("brain_of_cthulhu")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionBrainOfCthulhu",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.BrainOfCthulhu")));
        }

        if (player.SummonerAmbitions.Contains("queen_bee")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionQueenBee",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.QueenBee")));
        }

        if (player.SummonerAmbitions.Contains("skeletron")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionSkeletron",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Skeletron")));
        }

        if (player.SummonerAmbitions.Contains("wall_of_flesh")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionWallOfFlesh",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.WallOfFlesh")));
        }
    }

    public override void AddRecipes() {
        CreateRecipe()
            .AddIngredient(ItemID.Rope, 5)
            .AddRecipeGroup(RecipeGroupID.Squirrels)
            .AddIngredient(ItemID.Acorn, 25)
            .AddTile(TileID.WorkBenches)
            .Register();
    }

    public static bool IsPreHardmodeRealized(Player player) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        return modPlayer.SummonerAmbitions.Contains("king_slime")
               && modPlayer.SummonerAmbitions.Contains("eye_of_cthulhu")
               && modPlayer.SummonerAmbitions.Contains("deerclops")
               && (modPlayer.SummonerAmbitions.Contains("eater_of_worlds") || modPlayer.SummonerAmbitions.Contains("brain_of_cthulhu"))
               && modPlayer.SummonerAmbitions.Contains("queen_bee")
               && modPlayer.SummonerAmbitions.Contains("skeletron")
               && modPlayer.SummonerAmbitions.Contains("wall_of_flesh");
    }
}
