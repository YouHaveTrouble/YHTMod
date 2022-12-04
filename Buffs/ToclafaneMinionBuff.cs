using Terraria;
using Terraria.ModLoader;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Buffs; 

public class ToclafaneMinionBuff : ModBuff {
    
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Summon Toclafane");
        Description.SetDefault("It came from a parallel world to fight for its Master.");
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void Update(Player player, ref int buffIndex) {
        if (player.ownedProjectileCounts[ModContent.ProjectileType<ToclafaneMinion>()] > 0) {
            player.buffTime[buffIndex] = 18000;
        } else {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
