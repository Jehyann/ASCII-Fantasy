using ASCIIFantasy;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ASCIIFantasy
{
    public class Inventory
    {
        public List<GearPiece> listGearInventory { get; set;}
        public List<Item> listItemInventory { get; set;}

        public Inventory()
        {
            listGearInventory = new();
            listItemInventory = new();

        }

        public void AddGear(GearPiece c)
        {
            listGearInventory.Add(c);
        }

        public void RemoveGear(GearPiece c)
        {
            listGearInventory.Remove(c);
        }

        public void AddItem(Item _item, int _number)
        {
            if (listItemInventory.Contains(_item))
            {
                listItemInventory[listItemInventory.IndexOf(_item)].numberItem += _number;
            }
            else
            {
                _item.numberItem = _number;
                listItemInventory.Add(_item);
            }
        }

        public void SelectBagToOpen()
        {
            string[] options = new string[3];
            options[0] = "Return";
            int selectedIndex = 0;
            bool isChoiceDone = false;
            for (int i = 0; i <3; i++)
            {
                switch (i)
                {
                    case 0:
                        options[i] = "Return";                        
                        break;
                    case 1:
                        options[i] = "Gear";
                        break;
                    case 2:
                        options[i] = "Item";
                        break;
                }
            }
            while (!isChoiceDone)
            {
                DisplayBag(options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        isChoiceDone = true;
                    }
                    else if(selectedIndex == 1)
                    {
                        SelectGearToDisplay();
                    }
                    else
                    {
                        SelectItemToDisplay();
                    }
                }
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Length;
                            break;
                    }
                }
            }
            //renvoie au menu précédent
        }

        public void SelectGearToDisplay()
        {
            string[] options = new string[this.listGearInventory.Count + 1];
            options[0] = "Return";
            int selectedIndex = 0;
            bool isChoiceDone = false;
            for (int i = 0; i < listGearInventory.Count; i++)
            {
                options[i + 1] = listGearInventory[i].gearName;
            }
            while (!isChoiceDone)
            {
                DisplayGearInventory(options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        isChoiceDone = true;
                    }
                    else
                    {
                        ShowGearStats(listGearInventory[selectedIndex - 1]);
                    }
                }
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Length;
                            break;
                    }
                }
            }
        }

        public void SelectItemToDisplay()
        {
            string[] options = new string[this.listItemInventory.Count + 1];
            options[0] = "Return";
            int selectedIndex = 0;
            bool isChoiceDone = false;
            for (int i = 0; i < listItemInventory.Count; i++)
            {
                options[i + 1] = listItemInventory[i].itemName;
            }
            while (!isChoiceDone)
            {
                DisplayGearInventory(options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        isChoiceDone = true;
                    }
                    else
                    {
                        ShowItemDescription(listItemInventory[selectedIndex - 1]);
                    }
                }
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Length;
                            break;
                    }
                }
            }
        }

        public void ShowGearStats(GearPiece _gearSelected)
        {
            string[] options = new string[1];
            options[0] = "Return";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" > ");
            Console.WriteLine(options[0] + "\n");
            bool isChoiceDone = false;
            while (!isChoiceDone)
            {
                DisplayGearStats(_gearSelected);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    isChoiceDone = true;
                }
            }
        }
        public void ShowItemDescription(Item _itemSelected)
        {
            string[] options = new string[1];
            options[0] = "Return";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" > ");
            Console.WriteLine(options[0] + "\n");
            bool isChoiceDone = false;
            while (!isChoiceDone)
            {
                DisplayItemDesc(_itemSelected);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    isChoiceDone = true;
                }
            }
        }

        public void DisplayBag(string[] options, int selectedIndex)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" > ");
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("   ");
                    Console.WriteLine(options[i]);
                }
            }
        }

        public void DisplayGearStats(GearPiece _selectedGear)
        {

            Console.WriteLine(
                $"\t{_selectedGear.gearName.ToUpper()}\n\n" +
                $"\t\tType : {_selectedGear.type}\n\n" +
                $"\tBonus Health : {_selectedGear.bonusHealth}\n" +
                $"\tBonus Mana : {_selectedGear.bonusMana}\n" +
                $"\tBonus Attack : {_selectedGear.bonusAttack}\n" +
                $"\tBonus Defense : {_selectedGear.bonusDefense}\n" +
                $"\tBonus Intelligence : {_selectedGear.bonusIntelligence}\n" +
                $"\tBonus Agility : {_selectedGear.bonusAgility}\n" +
                $"\tBonus Luck : {_selectedGear.bonusLuck} \n");

        }
        public void DisplayItemDesc(Item _itemSelected)
        {

            Console.WriteLine(
                $"\t{_itemSelected.itemName.ToUpper()}\n\n" +
                $"\t{_itemSelected.type}\n" +
                $"\t{_itemSelected.description}");

        }


        public void DisplayGearInventory(string[] options, int selectedIndex)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" > ");
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("   ");
                    Console.WriteLine(options[i]);
                }
            }
        }

        public int SelectInventoryItemToUse(int turn,Player player)
        {
            string[] options = new string[listItemInventory.Count + 1];
            options[0] = "Return";
            int selectedIndex = 0;
            bool isChoiceDone = false;
            for (int i = 0; i < listItemInventory.Count; i++)
            {
                if (listItemInventory[i].numberItem > 0)
                    options[i + 1] = listItemInventory[i].itemName + " x" + listItemInventory[i].numberItem;
            }
            while (!isChoiceDone)
            {
                DisplayItemInventory(options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        isChoiceDone = true;
                        break;
                    }
                    else
                    {
                        turn = UseItemChoosed(listItemInventory[selectedIndex - 1], player);
                        return turn;
                    }
                }
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Length) % options.Length;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Length;
                            break;
                    }
                }
            }
            return turn;
        }

        public void DisplayItemInventory(string[] options, int selectedIndex)
        {
            for (int i = 0; i < options.Length; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" > ");
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("   ");
                    Console.WriteLine(options[i]);
                }
            }
        }

        public int UseItemChoosed(Item _item, Player player)
        {
            if (_item.numberItem > 0)
            {
                _item.Use(player.listCharacters[0]);
            }
            return 1;
        }

        /*    static void Main(string[] args)
            {
                Character player1 = new Character("Player1", 100, 100, 10, 10, 10, 10, 10);
                Character player2 = new Character("Player2", 100, 100, 10, 10, 10, 10, 10);
                Player player = new Player();
                player.listCharacters.Add(player1);
                player.listCharacters.Add(player2);
                player1.AddAttack("Fireball");
                player1.AddAttack("Bulk_up");
                player2.AddAttack("Fireball");
                player.SelectCharacter();
            }*/

    }
 
}
