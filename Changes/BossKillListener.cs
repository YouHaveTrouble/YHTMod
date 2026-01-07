using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Changes;

public class BossKillListener : GlobalNPC {
    public override void OnKill(NPC npc) {
        switch (npc.type) {
            case NPCID.KingSlime:
                HandleBossKill(npc, "king_slime");
                break;
            case NPCID.EyeofCthulhu:
                HandleBossKill(npc, "eye_of_cthulhu");
                break;
            case NPCID.EaterofWorldsHead:
            case NPCID.EaterofWorldsBody:
            case NPCID.EaterofWorldsTail:
                int foundEaterSegments = 0;
                foreach (NPC activeNpC in Main.ActiveNPCs) {
                    if (activeNpC.friendly || activeNpC.townNPC) continue;
                    if (activeNpC.type is not NPCID.EaterofWorldsBody
                        and not NPCID.EaterofWorldsHead
                        and not NPCID.EaterofWorldsTail
                       ) continue;
                    if (++foundEaterSegments > 1) break;
                }

                HandleBossKill(npc, "eater_of_worlds");
                break;
            case NPCID.BrainofCthulhu:
                HandleBossKill(npc, "brain_of_cthulhu");
                break;
            case NPCID.Deerclops:
                HandleBossKill(npc, "deerclops");
                break;
            case NPCID.QueenBee:
                HandleBossKill(npc, "queen_bee");
                break;
            case NPCID.SkeletronHead:
                HandleBossKill(npc, "skeletron");
                break;
            case NPCID.WallofFlesh:
                HandleBossKill(npc, "wall_of_flesh");
                break;
        }

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
            }
        }
    }
}
