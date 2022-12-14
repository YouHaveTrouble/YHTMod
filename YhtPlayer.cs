using Terraria.ModLoader;

namespace YHTMod; 

public class YhtPlayer : ModPlayer {

    public int arcaneMissle = 0;

    public override void ResetEffects() {

        this.arcaneMissle = 0;

        base.ResetEffects();
    }
}