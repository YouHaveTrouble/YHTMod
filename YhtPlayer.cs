using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using YHTMod.Buffs;

namespace YHTMod;

public class YhtPlayer : ModPlayer {
    public int ArcaneMissile = 0;
    public int KatanaTeleportCooldown = 0;
    public int SummonerAmbitionDeerclopsCooldown = 0;


    public bool SummonerAmbition = false;

    /**
     * Set of boss and event ids for unlocking perks from Summoner's Ambition accessory
     */
    public HashSet<string> SummonerAmbitions = [];

    public override void PreUpdate() {
        KatanaTeleportCooldown = Math.Max(KatanaTeleportCooldown - 1, 0);
        SummonerAmbitionDeerclopsCooldown = Math.Max(SummonerAmbitionDeerclopsCooldown - 1, 0);
    }

    public override void PostUpdateEquips() {
        if (SummonerAmbition) {
            Player.AddBuff(ModContent.BuffType<SummonerAmbitionBuff>(), 1);
            Player.maxMinions += GetSummonersAmbitionMinionBonus();

            if (SummonerAmbitions.Contains("king_slime")) {
                Player.whipRangeMultiplier += 0.2f;
            }

            if (ModLoader.HasMod("CalamityMod") && SummonerAmbitions.Contains("desert_scourge")) {
                Player.statLifeMax2 += 5;
            }

            if (SummonerAmbitions.Contains("eater_of_worlds")) {
                Player.GetArmorPenetration(DamageClass.Summon) += 5;
            }

            if (SummonerAmbitions.Contains("brain_of_cthulhu")) {
                Player.GetDamage(DamageClass.Summon) += 0.05f;
            }

            if (SummonerAmbitions.Contains("skeletron")) {
                Player.GetKnockback(DamageClass.Summon) += 0.1f;
            }
        }
    }

    public override void ResetEffects() {
        ArcaneMissile = 0;
        SummonerAmbition = false;

        base.ResetEffects();
    }

    public override void SaveData(TagCompound tag) {
        tag["summonerAmbitions"] = new List<string>(SummonerAmbitions);
    }

    public override void LoadData(TagCompound tag) {
        if (!tag.ContainsKey("summonerAmbitions")) return;
        IList<string> list = tag.GetList<string>("summonerAmbitions");
        SummonerAmbitions = new HashSet<string>(list);
    }

    public int GetSummonersAmbitionMinionBonus() {
        int amount = 1;
        if (SummonerAmbitions.Contains("wall_of_flesh")) {
            amount += 1;
        }

        return amount;
    }
}
