using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace YHTMod.Buffs;

public class WarriorAmbitionBuff : ModBuff {
    public override void SetStaticDefaults() {
        Main.buffNoSave[Type] = true;
        Main.buffNoTimeDisplay[Type] = true;
    }

    public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare) {
        YhtPlayer modPlayer = Main.LocalPlayer.TryGetModPlayer(out YhtPlayer mp) ? mp : null;
        if (modPlayer == null) return;
        tip = Language.GetTextValue("Mods.YHTMod.Buffs.WarriorAmbitionBuff.Description",
            modPlayer.WarriorAmbitions.Count);
    }

    public override void Update(Player player, ref int buffIndex) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();

        if (modPlayer.WarriorAmbition) {
            player.buffTime[buffIndex] = 18000;
        }
        else {
            player.DelBuff(buffIndex);
            buffIndex--;
        }
    }
}
