using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Items.ArcaneMissle; 

public class ArcaneMissleBehavior : GlobalNPC {
    
    public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit) {
        if (!crit) {
            base.OnHitByProjectile(npc, projectile, damage, knockback, false);
            return;
        }

        if (Main.netMode == NetmodeID.Server) {
            return;
        }

        Player player = Main.LocalPlayer;
        
        if (player.GetModPlayer<YhtPlayer>().arcaneMissle != 0 && projectile.DamageType == DamageClass.Magic) {
            // player just crit with magic weapon while having arcane missle accessory
            Projectile proj = Projectile.NewProjectileDirect(
                projectile.GetSource_FromThis("arcaneMissle"),
                Main.LocalPlayer.position,
                npc.position.DirectionFrom(Main.LocalPlayer.position),
                ProjectileID.MagicMissile,
                player.GetModPlayer<YhtPlayer>().arcaneMissle,
                0,
                Main.LocalPlayer.whoAmI
            );
            proj.friendly = true;
            proj.hostile = false;
            proj.timeLeft = 300;
            proj.maxPenetrate = 1;
            proj.tileCollide = false;
            proj.DamageType = DamageClass.Magic;
            proj.aiStyle = ProjAIStyleID.MagicMissile;
            
            // Prevent crits for the missles
            proj.CritChance = Int32.MinValue;
        }

        base.OnHitByProjectile(npc, projectile, damage, knockback, true);
    }
}