using System.Collections.Generic;
using Terraria.ModLoader;
using YHTMod.Items;

namespace YHTMod;

public class YHTMod : Mod {
    private static YHTMod _mod;

    public YHTMod() {
        _mod = this;
    }

    public static YHTMod GetInstance() {
        return _mod;
    }

    public static HashSet<int> GetAmbitionItems() {
        return [
            ModContent.ItemType<SummonersAmbition>(),
            ModContent.ItemType<WarriorsAmbition>()
        ];
    }
}
