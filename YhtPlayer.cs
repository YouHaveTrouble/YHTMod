using System;
using Terraria.ModLoader;

namespace YHTMod;

public class YhtPlayer : ModPlayer
{
    public int ArcaneMissile = 0;
    public int KatanaTeleportCooldown = 0;

    public override void PreUpdate()
    {
        KatanaTeleportCooldown = Math.Max(KatanaTeleportCooldown - 1, 0);
    }

    public override void ResetEffects()
    {
        ArcaneMissile = 0;

        base.ResetEffects();
    }
}