using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Projectiles.Weapons;

class MithrilPebbleOfPigSmitingProjectile : ModProjectile {
    public override void SetDefaults() {
        Projectile.width = 8;
        Projectile.height = 8;
        Projectile.friendly = true;
        Projectile.penetrate = 1;
        Projectile.tileCollide = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 300;
        Projectile.aiStyle = ProjAIStyleID.ThrownProjectile;
        Projectile.light = 0.3f;
    }

    public override void AI() {
        base.AI();
        DrawOriginOffsetX = 0;
        DrawOffsetX = 0;
        int dust = Dust.NewDust(Projectile.Center, 1, 1, DustID.Mythril, 0f, 0f, 0, default(Color), 1f);
        Main.dust[dust].noGravity = true;
        Main.dust[dust].velocity *= 0.3f;
    }
}
