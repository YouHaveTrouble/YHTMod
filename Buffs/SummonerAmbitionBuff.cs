using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Buffs;

public class SummonerAmbitionBuff : ModBuff
{
    public override void SetStaticDefaults()
    {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
    {
        var modPlayer = Main.LocalPlayer.TryGetModPlayer<YhtPlayer>(out var mp) ? mp : null;
        if (modPlayer == null) return;

        tip = Language.GetTextValue("Mods.YHTMod.Buffs.SummonerAmbitionBuff.Description", modPlayer.SummonerAmbitions.Count);
    }

    public override void Update(Player player, ref int buffIndex)
    {
        var modPlayer = player.GetModPlayer<YhtPlayer>();

        if (modPlayer.SummonerAmbition)
        {
            player.buffTime[buffIndex] = 18000;
        }
        else
        {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
