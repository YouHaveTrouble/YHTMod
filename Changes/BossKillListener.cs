using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Changes;

public class BossKillListener : GlobalNPC
{
    public override void OnKill(NPC npc)
    {
        switch (npc.type)
        {
            case NPCID.KingSlime:
                HandleBossKill(npc, "king_slime");
                break;
            case NPCID.EyeofCthulhu:
                HandleBossKill(npc, "eye_of_cthulhu");
                break;
            case NPCID.EaterofWorldsHead:
                HandleBossKill(npc, "eater_of_worlds");
                break;
            case NPCID.BrainofCthulhu:
                HandleBossKill(npc, "brain_of_cthulhu");
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

    private static void HandleBossKill(NPC npc, string bossKey)
    {
        foreach (var player in Main.ActivePlayers)
        {
            var modPlayer = player.GetModPlayer<YhtPlayer>();
            if (!npc.playerInteraction[player.whoAmI]) continue;
            if (modPlayer.KilledBosses.Add(bossKey))
            {
                ChatHelper.SendChatMessageToClient(
                    NetworkText.FromLiteral("Your Ambition's potential grows stronger!"), 
                    Color.MediumPurple,
                    player.whoAmI
                );
            }
        }
    }
}
