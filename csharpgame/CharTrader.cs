using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharpgame
{
    public class CharTrader : Character
    {
        public static Texture2D TraderImage { get; set; }
        public static Texture2D TraderDeathImage { get; set; }
        public static Texture2D TradingBackground { get; set; }
        public static int AttackCount { get; set; } = 2;
        public static int DamageCount { get; set; } = 2;
        public static int DefenseCount { get; set; } = 2;
        public static int HitpointCount { get; set; } = 1;
        public static List<CharTrader> TraderList { get; set; } = new List<CharTrader>();

        public static int ArmorCount { get; set; } = 0;
        public static List<ItemArmor> ArmorProgression { get; set; } = new List<ItemArmor>();
        public static int WeaponCount { get; set; } = 0 ;
        public static List<ItemWeapon> WeaponProgression { get; set; } = new List<ItemWeapon>();

        public CharTrader(Tile t) : base(t)
        {
            Environment env = Environment.Current();
            this.Name = "Trader";
            this.CurrentHitpoints = 1024;
            this.MaxHitpoints = CurrentHitpoints;
            this.Armor = 500;
            this.Attack = 0;
            this.Damage = 1;
            this.texture = TraderImage;
            this.DeathTexture = TraderDeathImage;
            this.behavior = Behavior.None;
            TraderList.Add(this);
        }

        public static UIElement GetUpdatedPanel()
        {
            Environment env = Environment.Current();
            int ScreenX = (env.Game.GraphicsDevice.Viewport.Width / 2) - 250;
            int ScreenY = (env.Game.GraphicsDevice.Viewport.Height / 2) - 150;

            List<Tuple<Texture2D, int, int>> MiscTexList = new List<Tuple<Texture2D, int, int>>();
            List<Tuple<string, int, int>> MiscStringList = new List<Tuple<string, int, int>>();

            Tuple<string, int, int> Welcome = new Tuple<string, int, int>("Greetings! I am Snapio Bagelles.\r\nI will upgrade your equipment, for a price.", 10, 0);
            Tuple<string, int, int> SingleHeal = new Tuple<string, int, int>("1 - Heal 1 (1GP)", 10, 60);
            Tuple<string, int, int> AllHeal = new Tuple<string, int, int>("2 - Heal All (" + (env.Player.MaxHitpoints - env.Player.CurrentHitpoints) + "GP)", 10, 100);
            Tuple<string, int, int> UpgradeAttack = new Tuple<string, int, int>("3 - Train Precision (" + (10 * AttackCount) + "GP)", 10, 140);
            Tuple<string, int, int> UpgradeDamage = new Tuple<string, int, int>("4 - Train Strength (" + (5 * DamageCount) + "GP)", 10, 180);
            Tuple<string, int, int> UpgradeDefense = new Tuple<string, int, int>("5 - Train Dodging (" + (20 * DefenseCount) + "GP)", 10, 220);
            Tuple<string, int, int> UpgradeHitpoints = new Tuple<string, int, int>("6 - Upgrade Hitpoints (" + (2 * HitpointCount) + "GP)", 10, 260);
            Tuple<string, int, int> BuyTorch = new Tuple<string, int, int>("7 - Buy Torch (20GP)", 220, 60);
            Tuple<string, int, int> BuyArmor = new Tuple<string, int, int>("8 - Buy " + ArmorProgression[ArmorCount].Name + " (" + ArmorProgression[ArmorCount].Price + "GP, " + ArmorProgression[ArmorCount].Armor + "DEF)", 220, 100);
            Tuple<string, int, int> BuyWeapon = new Tuple<string, int, int>("9 - Buy " + WeaponProgression[WeaponCount].Name + " (" + WeaponProgression[WeaponCount].Price + "GP, " + WeaponProgression[WeaponCount].GetDamage().Item1 + "-" + WeaponProgression[WeaponCount].GetDamage().Item2 + "DAM)", 220, 140);

            MiscStringList.Add(Welcome);
            MiscStringList.Add(SingleHeal);
            MiscStringList.Add(AllHeal);
            MiscStringList.Add(UpgradeAttack);
            MiscStringList.Add(UpgradeDamage);
            MiscStringList.Add(UpgradeDefense);
            MiscStringList.Add(UpgradeHitpoints);
            MiscStringList.Add(BuyTorch);
            MiscStringList.Add(BuyArmor);
            MiscStringList.Add(BuyWeapon);

            return new UIElement(TradingBackground, ScreenX, ScreenY, MiscTexList, MiscStringList);
        }
    }
}
