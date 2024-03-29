using System;
using Terraria.ModLoader;

namespace YHTMod; 

public class YhtPlayer : ModPlayer {

    public int arcaneMissle = 0;
    public int katanaTeleportCooldown = 0;

    public override void PreUpdate() {
        this.katanaTeleportCooldown = Math.Max(this.katanaTeleportCooldown - 1, 0);
    }

    public override void ResetEffects() {

        this.arcaneMissle = 0;

        base.ResetEffects();
    }
}