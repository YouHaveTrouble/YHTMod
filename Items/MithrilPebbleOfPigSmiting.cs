using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using YHTMod.Projectiles.Weapons;

namespace YHTMod.Items;

public class MithrilPebbleOfPigSmiting : ModItem {
    public override void SetStaticDefaults() {
        DisplayName.SetDefault("Mithril Pebble of Pig Smiting");
        Tooltip.SetDefault(
            "For you see long ago this pebble was forged in the fiery pits of Tartarus. By the grand blacksmith of Lucifer himself. In a time before the world began there were\n" +
            "pigs that roamed the world of aincrad. They took what they pleased and did what they wanted. The humans tried to survive but the pigs were too powerful. Then a man stood against the hogs!\n" +
            "His name... Well its not a Him its a Her...HER name...was...Akane! Lucifer crafted this mighty weapon for Akane. Amd she threw it Over and Over, Again and Again. She freed\n" +
            "the humans but the pigs wouldn't give up! In a mighty battle Akane fell to the Hogs Hero, Becon! Like Bacon but with a 'E' instead of a 'A'. ANYWAY Becon used his Cleaver to strike\n" +
            "down Akane. And the pebble fell. One of the hogs tried to pick it up but that hog died as soon as he touched the stone. Another Hero picked up the pebble. It was Akanes Apprentice. Sophie.\n" +
            "The hogs ran from the battlefield as Sophie threw the pebble. Striking many of them down. But there were too many. After returning home Sophie trained hard and set off to find Becon.\n" +
            "She killed hog after hog looking for him. One little piggy spilled and told her when he was. She made Becon, Into Bacon. Battle after battle the Hogs Became weaker. With time so did Sophie.\n" +
            "She died because she ate too much Pork. And the Pebble was passed too Marjosa. He made a decision. Instead of killing them all. He made them walk on four legs. He would eat them and keep them\n" +
            "locked up where they cant escape. Some Hogs rebelled but most of them went with it. Accepting defeat. Then as Marjosa was Hunting wild Hogs. 3 appeared at once. Taking him by surprise.\n" +
            "They hit him with there sharp horns. Marjosa hit the with the Mighty Pebble killing them all but he was weak and far from anyone who could offer help. And Thus, Marjosa\n" +
            "Guardian of the Pebble fell to his knees and passed from this world. Leaving behind the mighty weapon. For he knew. One day its power would be required once more.\n" +
            "But the Legacy of the pebble lives on.");
        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 16;
        Item.width = 8;
        Item.height = 8;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 2f;
        Item.value = 100;
        Item.rare = ItemRarityID.Green;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.crit = 2;
        Item.noMelee = true;
        Item.noUseGraphic = true;

        Item.shootSpeed = 20f;
        Item.shoot = ModContent.ProjectileType<MithrilPebbleOfPigSmitingProjectile>();
    }
}