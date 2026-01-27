using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YHTMod.Changes;

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
            int bonus = 20;
            if (ModLoader.HasMod("CalamityMod")) {
                bonus = 10;
            }
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionKingSlime",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.KingSlime", bonus)));
        }

        if (ModLoader.HasMod("CalamityMod") && player.SummonerAmbitions.Contains("desert_scourge")) {
            int id = CalamityHelper.GetBossIconId("desert_scourge");
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionDesertScourge",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.DesertScourge", "[i:" + id + "]")));
        }

        if (player.SummonerAmbitions.Contains("eye_of_cthulhu")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionEyeOfCthulhu",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.EyeOfCthulhu")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.SummonerAmbitions.Contains("crabulon")) {
            int id = CalamityHelper.GetBossIconId("crabulon");
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionCrabulon",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Crabulon", "[i:" + id + "]")));
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

        if (ModLoader.HasMod("CalamityMod") && player.SummonerAmbitions.Contains("perforators")) {
            int id = CalamityHelper.GetBossIconId("perforators");
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionPerforators",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Perforators", "[i:" + id + "]")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.SummonerAmbitions.Contains("hive_mind")) {
            int id = CalamityHelper.GetBossIconId("hive_mind");
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionHiveMind",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.HiveMind", "[i:" + id + "]")));
        }

        if (player.SummonerAmbitions.Contains("queen_bee")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionQueenBee",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.QueenBee")));
        }

        if (player.SummonerAmbitions.Contains("skeletron")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionSkeletron",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.Skeletron")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.SummonerAmbitions.Contains("slime_god")) {
            int id = CalamityHelper.GetBossIconId("slime_god");
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionSlimeGod",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.SlimeGod", "[i:" + id + "]")));
        }

        if (player.SummonerAmbitions.Contains("wall_of_flesh")) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionWallOfFlesh",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.WallOfFlesh")));
        }

        if (IsPreHardmodeRealized(player.Player)) {
            tooltips.Add(new TooltipLine(Mod, "SummonerAmbitionPreHardmodeRealized",
                Language.GetTextValue("Mods.YHTMod.Items.SummonersAmbition.PreHardmodeRealized")));
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

    private static bool IsPreHardmodeRealized(Player player) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        
        HashSet<string> bossesToCheck = [
            "king_slime",
            "eye_of_cthulhu",
            "deerclops",
            "queen_bee",
            "skeletron",
            "wall_of_flesh"
        ];

        if (!modPlayer.SummonerAmbitions.Contains("eater_of_worlds") && !modPlayer.SummonerAmbitions.Contains("brain_of_cthulhu")) {
            return false;
        }

        if (ModLoader.HasMod("CalamityMod")) {
            bossesToCheck.Add("desert_scourge");
            bossesToCheck.Add("crabulon");
            bossesToCheck.Add("slime_god");

            if (!modPlayer.SummonerAmbitions.Contains("perforators") && !modPlayer.SummonerAmbitions.Contains("hive_mind")) {
                return false;
            }
        }

        return bossesToCheck.All(boss => modPlayer.SummonerAmbitions.Contains(boss));
    }
}
