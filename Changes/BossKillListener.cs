using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Changes;

[ExtendsFromMod("CalamityMod")]
public class BossKillListener : GlobalNPC {

    private static readonly Dictionary<int, string> BossIds = new();

    public override void Load() {
        BossIds.Add(NPCID.KingSlime, "king_slime");
        BossIds.Add(NPCID.EyeofCthulhu, "eye_of_cthulhu");
        BossIds.Add(NPCID.EaterofWorldsHead, "eater_of_worlds");
        BossIds.Add(NPCID.EaterofWorldsBody, "eater_of_worlds");
        BossIds.Add(NPCID.EaterofWorldsTail, "eater_of_worlds");
        BossIds.Add(NPCID.BrainofCthulhu, "brain_of_cthulhu");
        BossIds.Add(NPCID.Deerclops, "deerclops");
        BossIds.Add(NPCID.QueenBee, "queen_bee");
        BossIds.Add(NPCID.SkeletronHead, "skeletron");
        BossIds.Add(NPCID.WallofFlesh, "wall_of_flesh");
        
        Mod calamity = CalamityHelper.GetCalamityMod();
        if (calamity == null) return;
        if (calamity.TryFind("DesertScourgeHead", out ModNPC desertScourgeHead)) {
            BossIds.Add(desertScourgeHead.Type, "desert_scourge_head");
        }
        if (calamity.TryFind("Crabulon", out ModNPC crabulon)) {
            BossIds.Add(crabulon.Type, "crabulon");
        }
        if (calamity.TryFind("PerforatorHive", out ModNPC perforatorHive)) {
            BossIds.Add(perforatorHive.Type, "perforators");
        }
        if (calamity.TryFind("HiveMind", out ModNPC hiveMind)) {
            BossIds.Add(hiveMind.Type, "hive_mind");
        }
        if (calamity.TryFind("SlimeGodCore", out ModNPC slimeGodCore)) {
            BossIds.Add(slimeGodCore.Type, "slime_god");
        }
    }

    public override void OnKill(NPC npc) {
        string id = BossIds.GetValueOrDefault(npc.type, null);
        switch (id) {
            case null:
                base.OnKill(npc);
                return;
            case "eater_of_worlds": {
                int foundEaterSegments = 0;
                foreach (NPC activeNpC in Main.ActiveNPCs) {
                    if (activeNpC.friendly || activeNpC.townNPC) continue;
                    if (activeNpC.type is not NPCID.EaterofWorldsBody
                        and not NPCID.EaterofWorldsHead
                        and not NPCID.EaterofWorldsTail
                       ) continue;
                    if (++foundEaterSegments > 1) break;
                }
                if (foundEaterSegments > 1) {
                    base.OnKill(npc);
                    return;
                }
                break;
            }
        }

        HandleBossKill(npc, id);
        base.OnKill(npc);
    }

    private static void HandleBossKill(NPC npc, string bossKey) {
        foreach (Player player in Main.ActivePlayers) {
            YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
            if (!npc.playerInteraction[player.whoAmI]) continue;

            if (modPlayer.SummonerAmbition && modPlayer.SummonerAmbitions.Add(bossKey)) {
                ChatHelper.SendChatMessageToClient(
                    NetworkText.FromLiteral("Your Summoner Ambition's potential grows stronger!"),
                    Color.MediumPurple,
                    player.whoAmI
                );
            } else if (modPlayer.WarriorAmbition && modPlayer.WarriorAmbitions.Add(bossKey)) {
                ChatHelper.SendChatMessageToClient(
                    NetworkText.FromLiteral("Your Warrior Ambition's potential grows stronger!"),
                    Color.OrangeRed,
                    player.whoAmI
                );
            }
        }
    }
}
