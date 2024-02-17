using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class Heal : Attack 
    {
        public Heal(string name, Element element, int power, int cost, int level) : base(name, element, power, cost, level)
        {
            type = AttackType.Heal;
        }

        public int UseHeal(int turn, Character attacker, List<Character> listCharacters)
        {
            int mana = attacker.stats.actual_mana;
            if ((mana - cost) >= 0)
            {
                string[] options = new string[listCharacters.Count+1];
                options[0] = "Return";
                int selectedIndex = 0;
                for (int i = 0; i < listCharacters.Count; i++)
                {
                    options[i+1] = listCharacters[i].name;
                }
                while (turn == 0)
                {
                    DisplayListCharacter(listCharacters, options, selectedIndex);

                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    Console.Clear();

                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        (turn, selectedIndex) = HealCharacterSelected(turn, listCharacters, selectedIndex);
                        if (selectedIndex == 0)
                            break;
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
            else
            {
                Console.WriteLine($" {attacker.name} tried using {attack_name} but forgot to look at his mana");
                Console.WriteLine($" {attacker.name} lost his turn because of that...");
                return turn;
            }
        }
        public virtual (int, int) HealCharacterSelected(int turn, List<Character> listCharacters, int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return (turn, 0);
            }
            else if (selectedIndex > 0 && selectedIndex <= listCharacters.Count)
            {
                int value = GetHealValue(listCharacters);
                bool crit = IsCriticalHit(listCharacters[0].stats.luck);
                Console.Clear();
                Console.WriteLine($" {listCharacters[0].name} used {attack_name}");
                if (crit)
                {
                    value = value * 2;
                    Console.WriteLine($" Lucky critical ! {listCharacters[0].name} healed {value} hp to {listCharacters[selectedIndex].name}!");
                }
                else
                {
                    Console.WriteLine($" {listCharacters[0].name} healed {value} hp to {listCharacters[selectedIndex].name}");
                }
                listCharacters[0].stats.IncrementMana(-cost);
                listCharacters[selectedIndex].stats.IncrementHealth(value);
                return (turn == 1 ? 0 : 1, selectedIndex);
            }
            else
            {
                Console.WriteLine("Invalid choice");
                return (turn, selectedIndex);
            }
        }
        public void DisplayListCharacter(List<Character> listCharacters, string[] options, int selectedIndex)
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
                if (i > 0 && i <= listCharacters.Count)
                {
                    listCharacters[i-1].stats.ShowHealth();
                    listCharacters[i-1].stats.ShowMana();
                }
            }
        }

        public int GetHealValue(List<Character> listCharacters)
        {
            return rnd.Next(listCharacters[0].stats.intelligence + 1) + power;
        }
        
    }

    public class SmallHeal : Heal
    {
        public SmallHeal() : base("Small heal", Element.Neutral, 5, 5, 5){}
        
    }

    public class MediumHeal : Heal
    {
        public MediumHeal() : base("Medium heal", Element.Neutral, 10, 10, 20) { }
    }

    public class BigHeal : Heal
    {
        public BigHeal() : base("Big heal", Element.Neutral, 20, 20, 40) { }
    }

    public class MegaHeal : Heal
    {
        public MegaHeal() : base("Mega heal", Element.Neutral, 40, 40, 65) { }
    }
}
