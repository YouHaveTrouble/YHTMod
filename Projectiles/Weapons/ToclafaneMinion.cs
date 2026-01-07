using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Buffs;

namespace YHTMod.Projectiles.Weapons;

public class ToclafaneMinion : ModProjectile {
    private int _shootCooldown = 0;
    private AttackMode _attackMode = AttackMode.Ranged;

    public override void SetStaticDefaults() {
        // Sets the amount of frames this minion has on its spritesheet
        Main.projFrames[Projectile.type] = 8;
        // This is necessary for right-click targeting
        ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;

        Main.projPet[Projectile.type] = true;
        ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
    }

    public sealed override void SetDefaults() {
        Projectile.width = 32;
        Projectile.height = 32;
        Projectile.scale = 0.65f;

        Projectile.tileCollide = false;

        Projectile.friendly = true;
        Projectile.minion = true;
        Projectile.minionSlots = 1f;
        Projectile.penetrate = -1;
    }

    public override bool? CanCutTiles() {
        return false;
    }

    public override bool MinionContactDamage() {
        return true;
    }

    public override void AI() {
        Player player = Main.player[Projectile.owner];

        #region Active check

        if (player.dead || !player.active) {
            player.ClearBuff(ModContent.BuffType<ToclafaneMinionBuff>());
        }

        if (player.HasBuff(ModContent.BuffType<ToclafaneMinionBuff>())) {
            Projectile.timeLeft = 2;
        }

        #endregion

        #region General behavior

        if (_shootCooldown > 0) {
            _shootCooldown = _shootCooldown - 1;
        }

        _attackMode = AttackMode.Ranged;

        Vector2 idlePosition = player.Center;
        idlePosition.Y -= 48f;

        float minionPositionOffsetX = (10 + Projectile.minionPos * 40) * -player.direction;
        idlePosition.X += minionPositionOffsetX;

        // All of this code below this line is adapted from Spazmamini code (ID 388, aiStyle 66)

        // Teleport to player if distance is too big
        Vector2 vectorToIdlePosition = idlePosition - Projectile.Center;
        float distanceToIdlePosition = vectorToIdlePosition.Length();
        if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f) {
            // Whenever you deal with non-regular events that change the behavior or position drastically, make sure to only run the code on the owner of the projectile,
            // and then set netUpdate to true
            Projectile.position = idlePosition;
            Projectile.velocity *= 0.1f;
            Projectile.netUpdate = true;
        }

        // If your minion is flying, you want to do this independently of any conditions
        const float overlapVelocity = 0.04f;
        for (int i = 0; i < Main.maxProjectiles; i++) {
            // Fix overlap with other minions
            Projectile other = Main.projectile[i];
            if (i == Projectile.whoAmI || !other.active || other.owner != Projectile.owner ||
                !(Math.Abs(Projectile.position.X - other.position.X) +
                    Math.Abs(Projectile.position.Y - other.position.Y) < Projectile.width)) continue;
            if (Projectile.position.X < other.position.X) Projectile.velocity.X -= overlapVelocity;
            else Projectile.velocity.X += overlapVelocity;
            if (Projectile.position.Y < other.position.Y) Projectile.velocity.Y -= overlapVelocity;
            else Projectile.velocity.Y += overlapVelocity;
        }

        #endregion

        #region Find target

        // Starting search distance
        float distanceFromTarget = 700f;
        Vector2 targetCenter = Projectile.position;
        bool foundTarget = false;

        // This code is required if your minion weapon has the targeting feature
        if (player.HasMinionAttackTargetNPC) {
            NPC npc = Main.npc[player.MinionAttackTargetNPC];
            float between = Vector2.Distance(npc.Center, Projectile.Center);
            // Reasonable distance away so it doesn't target across multiple screens
            if (between < 2000f) {
                distanceFromTarget = between;
                targetCenter = npc.Center;
                foundTarget = true;
            }
        }

        if (!foundTarget) {
            for (int i = 0; i < Main.maxNPCs; i++) {
                NPC npc = Main.npc[i];
                if (!npc.CanBeChasedBy()) continue;
                float between = Vector2.Distance(npc.Center, Projectile.Center);
                bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                bool inRange = between < distanceFromTarget;
                bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width,
                    Projectile.height, npc.position, npc.width, npc.height);
                // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterward
                // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                bool closeThroughWall = between < 100f;
                if (((!closest || !inRange) && foundTarget) || (!lineOfSight && !closeThroughWall)) continue;
                distanceFromTarget = between;
                targetCenter = npc.Center;
                foundTarget = true;
            }
        }

        Projectile.friendly = foundTarget;

        #endregion

        #region Movement

        // Default movement parameters (here for attacking)
        float speed = 8f;
        float inertia = 20f;

        if (foundTarget) {
            Vector2 direction = targetCenter - Projectile.Center;
            direction.Normalize();
            if (distanceFromTarget > 40f) {
                // The immediate range around the target (so it doesn't latch onto it when close)
                direction *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia;
            }

            switch (distanceFromTarget) {
                case <= 120f:
                    _attackMode = AttackMode.Melee;
                    break;
                case > 120f when _shootCooldown == 0: {
                    _shootCooldown = 60; // 1 second between shots
                    Projectile laser = Projectile.NewProjectileDirect(player.GetSource_FromThis(), Projectile.Center,
                        direction, ProjectileID.DeathLaser, 30, Projectile.knockBack, Projectile.owner);
                    laser.friendly = true;
                    laser.hostile = false;
                    laser.penetrate = 5;
                    _attackMode = AttackMode.Ranged;
                    break;
                }
            }
        }
        else {
            // Minion doesn't have a target: return to player and idle
            if (distanceToIdlePosition > 600f) {
                // Speed up the minion if it's away from the player
                speed = 12f;
                inertia = 60f;
            }
            else {
                // Slow down the minion if closer to the player
                speed = 4f;
                inertia = 80f;
            }

            if (distanceToIdlePosition > 20f) {
                // The immediate range around the player (when it passively floats about)

                // This is a simple movement formula using the two parameters and its desired direction to create a "homing" movement
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                Projectile.velocity = (Projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (Projectile.velocity == Vector2.Zero) {
                // If there is a case where it's not moving at all, give it a little "poke"
                Projectile.velocity.X = -0.15f;
                Projectile.velocity.Y = -0.05f;
            }
        }

        #endregion

        #region Animation and visuals

        // So it will lean slightly towards the direction it's moving
        Projectile.rotation = Projectile.velocity.X * 0.05f;

        const int frameSpeed = 8;
        Projectile.frameCounter++;
        if (Projectile.frameCounter >= frameSpeed) {
            Projectile.frameCounter = 0;
            switch (_attackMode) {
                case AttackMode.Melee:
                    switch (Projectile.frame) {
                        case 0:
                        case 1:
                        case 2:
                            Projectile.frame++;
                            break;
                        case 3:
                            Projectile.frame = 0;
                            break;
                        case 4:
                            Projectile.frame = 1;
                            break;
                        case 5:
                            Projectile.frame = 2;
                            break;
                        case 6:
                            Projectile.frame = 3;
                            break;
                        case 7:
                            Projectile.frame = 4;
                            break;
                    }

                    break;
                case AttackMode.Ranged:
                    switch (Projectile.frame) {
                        case 0:
                            Projectile.frame = 5;
                            break;
                        case 1:
                            Projectile.frame = 6;
                            break;
                        case 2:
                            Projectile.frame = 7;
                            break;
                        case 3:
                            Projectile.frame = 4;
                            break;
                        case 4:
                        case 5:
                        case 6:
                            Projectile.frame++;
                            break;
                        case 7:
                            Projectile.frame = 4;
                            break;
                    }

                    break;
            }
        }

        Lighting.AddLight(Projectile.Center, Color.White.ToVector3() * 0.78f);

        #endregion
    }

    private enum AttackMode {
        Melee,
        Ranged,
    }
}
