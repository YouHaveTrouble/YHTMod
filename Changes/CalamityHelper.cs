using System.Collections.Generic;
using Terraria.ModLoader;

namespace YHTMod.Changes;

[ExtendsFromMod("CalamityMod")]
public class CalamityHelper {
    
    public static readonly CalamityHelper Instance = new();

    private static Dictionary<string, int> _bossIconIds;

    private static Mod _calamity;

    private CalamityHelper() {
        ModLoader.TryGetMod("CalamityMod", out Mod calamity);
        _calamity = calamity;
        if (_calamity == null) return;

        _bossIconIds = new Dictionary<string, int> {
            { "desert_scourge", ModContent.TryFind("CalamityMod", "LoreDesertScourge", out ModItem item1) ? item1.Type : -1 },
            { "crabulon", ModContent.TryFind("CalamityMod", "LoreCrabulon", out ModItem item2) ? item2.Type : -1 },
            { "perforators", ModContent.TryFind("CalamityMod", "LorePerforators", out ModItem item3) ? item3.Type : -1 },
            { "hive_mind", ModContent.TryFind("CalamityMod", "LoreHiveMind", out ModItem item4) ? item4.Type : -1 },
            { "slime_god", ModContent.TryFind("CalamityMod", "LoreSlimeGod", out ModItem item5) ? item5.Type : -1 }
        };
    }

    public static int GetBossIconId(string bossKey) {
        return _bossIconIds.GetValueOrDefault(bossKey,  -1);
    }
    
    public static Mod GetCalamityMod() {
        return _calamity;
    }
    
}
