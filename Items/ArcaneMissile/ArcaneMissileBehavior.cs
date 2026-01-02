using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Items.ArcaneMissile;

public class ArcaneMissileBehavior : GlobalNPC
{
    public override void OnHitByProjectile(NPC npc, Projectile projectile, NPC.HitInfo hitInfo, int damage)
    {
        if (!hitInfo.Crit)
        {
            base.OnHitByProjectile(npc, projectile, hitInfo, damage);
            return;
        }

        if (Main.netMode == NetmodeID.Server)
        {
            return;
        }

        var player = Main.LocalPlayer;

        if (player.GetModPlayer<YhtPlayer>().ArcaneMissile != 0 && projectile.DamageType == DamageClass.Magic)
        {
            // player just crit with magic weapon while having arcane missile accessory
            var proj = Projectile.NewProjectileDirect(
                projectile.GetSource_FromThis("arcaneMissile"),
                Main.LocalPlayer.position,
                npc.position.DirectionFrom(Main.LocalPlayer.position),
                ProjectileID.MagicMissile,
                player.GetModPlayer<YhtPlayer>().ArcaneMissile,
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

            // Prevent crits for the missiles
            proj.CritChance = int.MinValue;
        }

        base.OnHitByProjectile(npc, projectile, hitInfo, damage);
    }
}