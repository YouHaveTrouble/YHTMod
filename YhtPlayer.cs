using Terraria.ModLoader;

namespace YHTMod; 

public class YhtPlayer : ModPlayer {

    public bool arcaneMissle = false;

    public override void ResetEffects() {

        this.arcaneMissle = false;

        base.ResetEffects();
    }
}