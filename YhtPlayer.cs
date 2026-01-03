using System;
using System.Collections.Generic;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using YHTMod.Buffs;

namespace YHTMod;

public class YhtPlayer : ModPlayer
{
    public int ArcaneMissile = 0;
    public int KatanaTeleportCooldown = 0;

    public bool HasAmbition = false;
    public bool SummonerAmbition = false;
    
    public HashSet<string> KilledBosses = [];
    
    public HashSet<string> SummonerAmbitionCrafts = [];

    public override void PreUpdate()
    {
        KatanaTeleportCooldown = Math.Max(KatanaTeleportCooldown - 1, 0);
    }

    public override void PostUpdateMiscEffects()
    {
        if (SummonerAmbition)
        {
            Player.AddBuff(ModContent.BuffType<SummonerAmbitionBuff>(), 1);
            Player.maxMinions += GetSummonersAmbitionMinionBonus();
        }
    }

    public override void ResetEffects()
    {
        ArcaneMissile = 0;
        HasAmbition = false;
        SummonerAmbition = false;

        base.ResetEffects();
    }
    
    public override void SaveData(TagCompound tag)
    {
        tag["killedBosses"] = new List<string>(KilledBosses);
        tag["summonerAmbitions"] = new List<string>(SummonerAmbitionCrafts);
    }
    
    public override void LoadData(TagCompound tag)
    {
        if (tag.ContainsKey("killedBosses"))
        {
            var list = tag.GetList<string>("killedBosses");
            KilledBosses = new HashSet<string>(list);
        }
        if (tag.ContainsKey("summonerAmbitions"))
        {
            var list = tag.GetList<string>("summonerAmbitions");
            SummonerAmbitionCrafts = new HashSet<string>(list);
        }
    }
    
    public int GetSummonersAmbitionMinionBonus()
    {
        return SummonerAmbitionCrafts.Count / 3 + 1;
    }
}
