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
    public int SummonerAmbitionPerforatorsCooldown = 0;
    public int SummonerAmbitionHiveMindCooldown = 0;


    public bool SummonerAmbition = false;
    public bool WarriorAmbition = false;

    /**
     * Set of boss and event ids for unlocking perks from Summoner's Ambition accessory
     */
    public HashSet<string> SummonerAmbitions = [];
    public HashSet<string> WarriorAmbitions = [];

    public override void PreUpdate() {
        KatanaTeleportCooldown = Math.Max(KatanaTeleportCooldown - 1, 0);
        SummonerAmbitionDeerclopsCooldown = Math.Max(SummonerAmbitionDeerclopsCooldown - 1, 0);
        SummonerAmbitionPerforatorsCooldown = Math.Max(SummonerAmbitionPerforatorsCooldown - 1, 0);
        SummonerAmbitionHiveMindCooldown = Math.Max(SummonerAmbitionHiveMindCooldown - 1, 0);
    }

    public override void PostUpdateEquips() {
        if (SummonerAmbition) {
            Player.AddBuff(ModContent.BuffType<SummonerAmbitionBuff>(), 1);
            Player.maxMinions += GetSummonersAmbitionMinionBonus();

            if (SummonerAmbitions.Contains("king_slime")) {
                if (ModLoader.HasMod("CalamityMod")) {
                    Player.whipRangeMultiplier += 0.1f;
                }
                else {
                    Player.whipRangeMultiplier += 0.2f;
                }
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
            if (ModLoader.HasMod("CalamityMod") && SummonerAmbitions.Contains("slime_god")) {
                Player.whipRangeMultiplier += 0.1f;
            }
        }
        if (WarriorAmbition) {
            Player.AddBuff(ModContent.BuffType<WarriorAmbitionBuff>(), 1);
            Player.statDefense += GetWarriorsAmbitionDefenseBonus();
            
            if (ModLoader.HasMod("CalamityMod") && SummonerAmbitions.Contains("desert_scourge")) {
                Player.statLifeMax2 += 10;
            }

        }
    }

    public bool hasAmbitionEquipped() {
        if (SummonerAmbition) return true;
        if (WarriorAmbition) return true;
        return false;
    }

    public override void ResetEffects() {
        ArcaneMissile = 0;
        SummonerAmbition = false;
        WarriorAmbition = false;

        base.ResetEffects();
    }

    public override void SaveData(TagCompound tag) {
        tag["summonerAmbitions"] = new List<string>(SummonerAmbitions);
        tag["warriorAmbitions"] = new List<string>(WarriorAmbitions);
    }

    public override void LoadData(TagCompound tag) {
        if (tag.ContainsKey("summonerAmbitions")) {
            IList<string> list = tag.GetList<string>("summonerAmbitions");
            SummonerAmbitions = new HashSet<string>(list);
        }
        if (tag.ContainsKey("warriorAmbitions")) {
            IList<string> warriorList = tag.GetList<string>("warriorAmbitions");
            WarriorAmbitions = new HashSet<string>(warriorList);
        }
    }

    public int GetSummonersAmbitionMinionBonus() {
        int amount = 1;
        if (SummonerAmbitions.Contains("wall_of_flesh")) {
            amount += 1;
        }

        return amount;
    }

    public int GetWarriorsAmbitionDefenseBonus() {
        int amount = 2;

        if (ModLoader.HasMod("CalamityMod") && SummonerAmbitions.Contains("perforators")) {
            amount += 2;
        }

        if (ModLoader.HasMod("CalamityMod") && SummonerAmbitions.Contains("hive_mind")) {
            amount += 2;
        }
        
        if (WarriorAmbitions.Contains("wall_of_flesh")) {
            amount += 5;
        }

        return amount;
    }

    public float GetWarriorsAmbitionMeleeSizeBonus() {
        float scale = 1f;
        
        if (WarriorAmbitions.Contains("king_slime")) {
            if (ModLoader.HasMod("CalamityMod")) {
                scale += 0.025f;
            }
            else {
                scale += 0.05f;
            }
        }

        if (ModLoader.HasMod("CalamityMod") && WarriorAmbitions.Contains("slime_god")) {
            scale += 0.025f;
        }
        
        return scale;
    }
}
