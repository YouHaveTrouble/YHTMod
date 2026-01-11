
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Buffs;

public class ShroomGlowDebuff : ModBuff {
    public override void SetStaticDefaults() {
        Main.debuff[Type] = true;
    }

    public override void Update(NPC npc, ref int buffIndex) {
        if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.SinglePlayer) { // client-only
            Lighting.AddLight(npc.Center, 0.1f, 0.3f, 0.6f);
        }
    }
}
