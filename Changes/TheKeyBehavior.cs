
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using YHTMod.Items;

namespace YHTMod.Changes; 

public class TheKeyBehavior : GlobalTile {

    public override void RightClick(int i, int j, int type) {

        if (type != TileID.Containers) {
            base.RightClick(i, j, type);
            return;
        }

        bool foundKey = false;
        foreach (Item item in Main.LocalPlayer.inventory) {
            if (item.type.Equals(ModContent.ItemType<TheKey>())) {
                foundKey = true;
                break;
            }
        }
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(foundKey.ToString()), Color.White);

        if (!foundKey) {
            return;
        }
        ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral("Found key!"), Color.White);

        if (!Chest.IsLocked(i, j)) {
            base.RightClick(i, j, type);
            return;
        }

        Chest.Unlock(i, j);
        if (Main.netMode == NetmodeID.MultiplayerClient)
            NetMessage.SendData(MessageID.Unlock, number: 1, number2: 1f, number3: i, number4: j);


    }
    
}