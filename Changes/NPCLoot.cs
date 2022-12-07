
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Items;


namespace YHTMod.Changes;

public class NpcLoot : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot) {
        int id = npc.type;
        if (NPCID.Sets.CountsAsCritter[id]) {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MithrilPebbleOfPigSmiting>(), 400, 1, 1));
        }
    }
}
