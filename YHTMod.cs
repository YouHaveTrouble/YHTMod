using Terraria.ModLoader;

namespace YHTMod;

public class YHTMod : Mod {
    private static YHTMod _mod;

    public YHTMod() {
        _mod = this;
    }

    public static YHTMod GetInstance() {
        return _mod;
    }
}
