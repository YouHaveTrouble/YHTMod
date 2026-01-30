using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YHTMod.Changes;

namespace YHTMod.Items;

public class WarriorsAmbition : ModItem {
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

    public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        return !modPlayer.hasAmbitionEquipped() && base.CanAccessoryBeEquippedWith(equippedItem, incomingItem, player);
    }

    public override void UpdateAccessory(Player player, bool hideVisual) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        modPlayer.WarriorAmbition = true;
    }

    public override void ModifyTooltips(List<TooltipLine> tooltips) {
        YhtPlayer player = Main.LocalPlayer.GetModPlayer<YhtPlayer>();
        tooltips.Add(new TooltipLine(Mod, "WarriorsAmbition",
            Language.GetTextValue("Mods.YHTMod.Items.WarriorAmbition.Tooltip")));

        if (player.WarriorAmbitions.Contains("king_slime")) {
            float bonus = 5f;
            if (ModLoader.HasMod("CalamityMod")) {
                bonus = 2.5f;
            }
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionKingSlime",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.KingSlime", bonus)));
        }

        if (ModLoader.HasMod("CalamityMod") && player.WarriorAmbitions.Contains("desert_scourge")) {
            int id = CalamityHelper.GetBossIconId("desert_scourge");
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionDesertScourge",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.DesertScourge", "[i:" + id + "]")));
        }

        if (player.WarriorAmbitions.Contains("eye_of_cthulhu")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionEyeOfCthulhu",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.EyeOfCthulhu")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.WarriorAmbitions.Contains("crabulon")) {
            int id = CalamityHelper.GetBossIconId("crabulon");
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionCrabulon",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.Crabulon", "[i:" + id + "]")));
        }

        if (player.WarriorAmbitions.Contains("deerclops")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionDeerclops",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.Deerclops")));
        }

        if (player.WarriorAmbitions.Contains("eater_of_worlds")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionEaterOfWorlds",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.EaterOfWorlds")));
        }

        if (player.WarriorAmbitions.Contains("brain_of_cthulhu")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionBrainOfCthulhu",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.BrainOfCthulhu")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.WarriorAmbitions.Contains("perforators")) {
            int id = CalamityHelper.GetBossIconId("perforators");
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionPerforators",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.Perforators", "[i:" + id + "]")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.WarriorAmbitions.Contains("hive_mind")) {
            int id = CalamityHelper.GetBossIconId("hive_mind");
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionHiveMind",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.HiveMind", "[i:" + id + "]")));
        }

        if (player.WarriorAmbitions.Contains("queen_bee")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionQueenBee",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.QueenBee")));
        }

        if (player.WarriorAmbitions.Contains("skeletron")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionSkeletron",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.Skeletron")));
        }

        if (player.WarriorAmbitions.Contains("wall_of_flesh")) {
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionWallOfFlesh",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.WallOfFlesh")));
        }

        if (ModLoader.HasMod("CalamityMod") && player.WarriorAmbitions.Contains("slime_god")) {
            int id = CalamityHelper.GetBossIconId("slime_god");
            tooltips.Add(new TooltipLine(Mod, "WarriorAmbitionSlimeGod",
                Language.GetTextValue("Mods.YHTMod.Items.WarriorsAmbition.SlimeGod", "[i:" + id + "]")));
        }
    }

    public override void AddRecipes() {
        
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

        if (!modPlayer.WarriorAmbitions.Contains("eater_of_worlds") && !modPlayer.WarriorAmbitions.Contains("brain_of_cthulhu")) {
            return false;
        }

        if (ModLoader.HasMod("CalamityMod")) {
            bossesToCheck.Add("desert_scourge");
            bossesToCheck.Add("crabulon");
            bossesToCheck.Add("slime_god");

            if (!modPlayer.WarriorAmbitions.Contains("perforators") && !modPlayer.WarriorAmbitions.Contains("hive_mind")) {
                return false;
            }
        }

        return bossesToCheck.All(boss => modPlayer.WarriorAmbitions.Contains(boss));
    }
}
