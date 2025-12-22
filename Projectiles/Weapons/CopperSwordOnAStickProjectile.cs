using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Projectiles.Weapons;

class CopperSwordOnAStickProjectile : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = 80;
        Projectile.height = 80;

        Projectile.friendly = true;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.DamageType = DamageClass.Melee;
        Projectile.ownerHitCheck = true;
        Projectile.extraUpdates = 1;
        Projectile.timeLeft = 300;
        Projectile.aiStyle = ProjAIStyleID.ShortSword;
    }

    public override void AI()
    {
        base.AI();
        float random = (float)(Random.Shared.NextDouble() / 5f);
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 -
                              MathHelper.PiOver4 * Projectile.spriteDirection;
        Projectile.rotation += random;
        DrawOriginOffsetX = 0;
        DrawOffsetX = 0;
    }
}