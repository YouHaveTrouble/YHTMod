using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace YHTMod.Changes;

public class WarriorItemEffects : GlobalItem {
    
    public override bool InstancePerEntity => true;

    public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();

        if (!modPlayer.WarriorAmbition) {
            base.UseItemHitbox(item, player, ref hitbox, ref noHitbox);
            return;
        }

        if (item.DamageType == DamageClass.Melee && !item.noMelee) {
            float scale = modPlayer.GetWarriorsAmbitionMeleeSizeBonus();
            if (scale > 1f) {
                int newW = (int)(hitbox.Width * scale);
                int newH = (int)(hitbox.Height * scale);
                int cx = hitbox.X + hitbox.Width / 2;
                int cy = hitbox.Y + hitbox.Height / 2;
                hitbox.X = cx - newW / 2;
                hitbox.Y = cy - newH / 2;
                hitbox.Width = newW;
                hitbox.Height = newH;
            }
        }
        
        base.UseItemHitbox(item, player, ref hitbox, ref noHitbox);
    }
    
    public override void HoldItem(Item item, Player player) {
        YhtPlayer modPlayer = player.GetModPlayer<YhtPlayer>();

        if (!modPlayer.WarriorAmbition) {
            base.HoldItem(item, player);
            return;
        }

        if (item.DamageType == DamageClass.Melee && !item.noMelee) {
            float sizeBonus = modPlayer.GetWarriorsAmbitionMeleeSizeBonus();
            if (sizeBonus >= 1f)
            {
                item.scale *= sizeBonus;
            }
        }
        base.HoldItem(item, player);
    }
}
