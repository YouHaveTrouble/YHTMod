using Terraria;
using Terraria.ModLoader;

namespace YHTMod.Items.ArcaneMissle; 

public class ArcaneMissleBehavior : GlobalNPC {
    
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) {
        if (!crit) {
            base.OnHitByProjectile(npc, projectile, damage, knockback, false);
            return;
        }
        Player player = Main.LocalPlayer;
        
        if (player.GetModPlayer<YhtPlayer>().arcaneMissle && projectile.DamageType == DamageClass.Magic) {
            // player just crit with magic weapon while having arcane missle accessory equipped
        }

        base.OnHitByProjectile(npc, projectile, damage, knockback, true);
    }
}