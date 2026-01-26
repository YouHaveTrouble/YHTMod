using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Buffs;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace YHTMod.Changes;

public class SummonerOnHitEffects : GlobalProjectile {
    public override void OnSpawn(Projectile projectile, IEntitySource source) {
        if (ModLoader.HasMod("CalamityMod") && projectile.type == ModContent.ProjectileType<CalamityMod.Projectiles.Boss.ShaderainHostile>()) {
            if (source is EntitySource_Parent { Entity: Projectile parentProj }) {
                int shadeType = ModContent.ProjectileType<CalamityMod.Projectiles.Boss.ShadeNimbusHostile>();
                if (parentProj.type == shadeType && parentProj.friendly && !parentProj.hostile) {
                    projectile.friendly = true;
                    projectile.hostile = false;
                    projectile.DamageType = DamageClass.Summon;
                    projectile.damage = parentProj.damage;
                }
            }
        }

        base.OnSpawn(projectile, source);
    }

    public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone) {
        Player player = Main.player[projectile.owner];
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();

        // Player's own minions
        if (projectile.minion && Main.myPlayer == projectile.owner) {
            if (modPlayer.SummonerAmbition) {
                HandleDeerclopsEffect(modPlayer, target, projectile);
                HandleQueenBeeEffect(modPlayer, target);
                HandlePerforatorsEffect(modPlayer, target, projectile);
                HandleHiveMindEffect(modPlayer, target, projectile);
            }
        }

        // Whips
        if (modPlayer.SummonerAmbition && ProjectileID.Sets.IsAWhip[projectile.type]) {
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

    private static void HandleDeerclopsEffect(YhtPlayer modPlayer, NPC target, Projectile projectile) {
        Player player = modPlayer.Player;
        if (!modPlayer.SummonerAmbitions.Contains("deerclops")) return;
        if (modPlayer.SummonerAmbitionDeerclopsCooldown != 0) return;
        if (!Main.rand.NextBool(10)) return;

        modPlayer.SummonerAmbitionDeerclopsCooldown = 5 * 60;
        Vector2 direction = new(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
        direction.Normalize();
        direction *= Main.rand.NextFloat(4f, 8f);
        Projectile.NewProjectile(
            player.GetSource_OnHit(target),
            target.Center,
            direction,
            ProjectileID.InsanityShadowFriendly,
            (int) (projectile.damage * 0.5),
            0f,
            projectile.owner
        );
    }
    
    private static void HandleQueenBeeEffect(YhtPlayer modPlayer, NPC target) {
        if (!modPlayer.SummonerAmbitions.Contains("queen_bee")) return;
        if (!Main.rand.NextBool(4)) return; 
        target.AddBuff(BuffID.Poisoned, 5 * 60);
    }

    private static void HandlePerforatorsEffect(YhtPlayer modPlayer, NPC target, Projectile projectile) {
        if (!ModLoader.HasMod("CalamityMod")) return;
        if (!modPlayer.SummonerAmbitions.Contains("perforators")) return;
        if (modPlayer.SummonerAmbitionPerforatorsCooldown != 0) return;
        if (!Main.rand.NextBool(10)) return;
        modPlayer.SummonerAmbitionPerforatorsCooldown = 20 * 60;
        Vector2 direction = new(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, 1f));
        direction.Normalize();
        direction *= Main.rand.NextFloat(4f, 5f);
        int projectileType = ModContent.ProjectileType<CalamityMod.Projectiles.Boss.IchorBlob>();
        int projectileId = Projectile.NewProjectile(
            modPlayer.Player.GetSource_OnHit(target),
            target.Center,
            direction,
            projectileType,
            (int) (projectile.damage * 0.75f),
            0f,
            projectile.owner
        );
        Projectile blob = Main.projectile[projectileId];
        blob.friendly = true;
        blob.hostile = false;
        blob.DamageType = DamageClass.Summon;
    }
    
    private static void HandleHiveMindEffect(YhtPlayer modPlayer, NPC target, Projectile projectile) {
        if (!ModLoader.HasMod("CalamityMod")) return;
        if (!modPlayer.SummonerAmbitions.Contains("hive_mind")) return;
        if (modPlayer.SummonerAmbitionHiveMindCooldown != 0) return;
        if (!Main.rand.NextBool(10)) return;
        modPlayer.SummonerAmbitionHiveMindCooldown = 3 * 60;
        int projectileType = ModContent.ProjectileType<CalamityMod.Projectiles.Boss.ShadeNimbusHostile>();
        int projectileId = Projectile.NewProjectile(
            modPlayer.Player.GetSource_OnHit(target),
            target.Center,
            new Vector2(0, -0.25f),
            projectileType,
            (int) (projectile.damage * 0.75f),
            0f,
            projectile.owner
        );
        Projectile blob = Main.projectile[projectileId];
        blob.friendly = true;
        blob.hostile = false;
        blob.DamageType = DamageClass.Summon;
    }

}
