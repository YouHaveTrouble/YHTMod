using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Buffs;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace YHTMod.Changes;

public class SummonerOnHitEffects : GlobalProjectile {
    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone) {
        Player player = Main.player[projectile.owner];
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();
        
        // Player's own minions
        if (projectile.minion && Main.myPlayer == projectile.owner) {
            if (modPlayer.SummonerAmbition) {
                if (
                    modPlayer.SummonerAmbitions.Contains("deerclops")
                    && modPlayer.SummonerAmbitionDeerclopsCooldown == 0
                    && Main.rand.NextBool(10)
                ) {
                    modPlayer.SummonerAmbitionDeerclopsCooldown = 5 * 60;
                    Vector2 direction = new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
                    direction.Normalize();
                    direction *= Main.rand.NextFloat(4f, 8f);
                    Projectile.NewProjectile(
                        player.GetSource_OnHit(target),
                        target.Center,
                        direction,
                        ProjectileID.InsanityShadowFriendly,
                        projectile.damage / 2,
                        0f,
                        projectile.owner
                    );
                }

                if (modPlayer.SummonerAmbitions.Contains("queen_bee") && Main.rand.NextBool(4)) {
                    target.AddBuff(BuffID.Poisoned, 5 * 60);
                }
            }
        }

        // Whips
        if (ProjectileID.Sets.IsAWhip[projectile.type]) {
            if (modPlayer.SummonerAmbitions.Contains("eye_of_cthulhu")) {
                projectile.damage = (int)(projectile.damage * 1.1);
            }

            if (ModLoader.HasMod("CalamityMod")) {
                if (modPlayer.SummonerAmbitions.Contains("crabulon")) {
                    target.AddBuff(ModContent.BuffType<ShroomGlowDebuff>(), 5 * 60);
                }
            }
        }

        base.OnHitNPC(projectile, target, hit, damageDone);
    }
}
