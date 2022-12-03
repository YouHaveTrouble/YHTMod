using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace YHTMod.Items;

public class KatanaRedo : GlobalItem {
    
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        if (item.type != ItemID.Katana) return;
        tooltips.Insert(5, new TooltipLine(YHTMod.GetInstance(), "flavor" , "Nothing personel kid."));
        tooltips.Insert(6, new TooltipLine(YHTMod.GetInstance(), "usage" , "Right click to teleport behind them."));
    }

    public override bool AltFunctionUse(Item item, Player player) {
        if (item.type != ItemID.Katana) {
            return base.AltFunctionUse(item, player);
        }
        return true;
    }

    public override bool? UseItem(Item item, Player player) {
        if (item.type != ItemID.Katana || player.altFunctionUse != 2) return null;
        for (int i = 0; i < Main.maxNPCs; i++) {
            NPC npc = Main.npc[i];
            if (!npc.CanBeChasedBy()) continue;

            float between = Vector2.Distance(npc.Center, player.Center);
            bool inRange = between < 650;
            
            bool lineOfSight = Collision.CanHitLine(player.position, player.width, player.height, npc.position,
                npc.width, npc.height);
            
            if (inRange && lineOfSight) {
                Vector2 tpPos = npc.position;
                tpPos.X += -(npc.direction * npc.width + (player.width*2));
                
                if (Collision.TileCollision(tpPos, Vector2.Zero, player.width, player.height) != Vector2.Zero) return true;
                
                player.Teleport(tpPos, TeleportationStyleID.RodOfDiscord);
                player.velocity = Vector2.Zero;
                player.NinjaDodge();
                return true;
            }
        }
        
        return null;
    }
}