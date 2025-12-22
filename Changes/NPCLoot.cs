using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Items;
using YHTMod.Items.ArcaneMissile;


namespace YHTMod.Changes;

public class NpcLoot : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        var id = npc.type;

        if (NPCID.Sets.CountsAsCritter[id])
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MithrilPebbleOfPigSmiting>(), 400));
        }

        switch (id)
        {
            case NPCID.Plantera:
                npcLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 1, 60, 80));
                break;
            case NPCID.Tim:
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ArcaneMissile>()));
                break;
        }
    }
}