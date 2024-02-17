using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class Chest
    {
        public static void LootChest()
        {
            Random random = new Random();
            int type = random.Next(1, 51);
            if (type <= 20)
            {
                LootItem();
            }
            else if (type > 20 && type <= 40)
            {
                LootGear();
            }
            else
            {
                LootSpell();
            }
        }


        static void LootItem()
        {
            Random random = new Random();
            int loot = random.Next(1, 101);
            if (loot <= 60)
            {
                Player.instance.inventory.AddItem(ItemList.instance.listItem[0], random.Next(2, 6));
            }
            else if (loot > 60 && loot <= 80)
            {
                Player.instance.inventory.AddItem(ItemList.instance.listItem[1], random.Next(1, 6));
            }
            else if (loot > 80 && loot <= 90)
            {
                Player.instance.inventory.AddItem(ItemList.instance.listItem[2], random.Next(1, 6));
            }
            else if (loot > 90 && loot <= 95)
            {
                Player.instance.inventory.AddItem(ItemList.instance.listItem[3], random.Next(1, 6));
            }
            else
            {
                Player.instance.inventory.AddItem(ItemList.instance.listItem[3], 10);
            }
        }

        static void LootGear()
        {
            Random random = new Random();
            int loot = random.Next(1, 101);
            if (loot <= 60)
            {
                Player.instance.inventory.AddGear(GearList.instance.listGear[random.Next(0, 7)]);
            }
            else if (loot > 60 && loot <= 85)
            {
                Player.instance.inventory.AddGear(GearList.instance.listGear[random.Next(7, 12)]);
            }
            else if (loot > 85 && loot <= 95)
            {
                Player.instance.inventory.AddGear(GearList.instance.listGear[random.Next(12, 17)]);
            }
            else if (loot > 95 && loot <= 100)
            {
                Player.instance.inventory.AddGear(GearList.instance.listGear[17]);
            }

        }

        static void LootSpell()
        {
            Random random = new Random();
            int type = random.Next(1, 5);
            int loot;
            switch(type)
            {
                case 1:
                    loot = random.Next(0, ListAttackGlobal.instance.listAttackSpell.Count());
                    Player.instance.AddAttack(ListAttackGlobal.instance.listAttackSpell[loot]);
                    break;
                case 2:
                    loot = random.Next(0, ListAttackGlobal.instance.listAttackBuff.Count());
                    Player.instance.AddAttack(ListAttackGlobal.instance.listAttackBuff[loot]);
                    break;
                case 3:
                    loot = random.Next(0, ListAttackGlobal.instance.listAttackHeal.Count());
                    Player.instance.AddAttack(ListAttackGlobal.instance.listAttackHeal[loot]);
                    break;
                case 4:
                    loot = random.Next(0, ListAttackGlobal.instance.listAttackPhysical.Count());
                    Player.instance.AddAttack(ListAttackGlobal.instance.listAttackPhysical[loot]);
                    break;
            }
        }
    }
}
