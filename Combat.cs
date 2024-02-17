using System;
using ASCIIFantasy;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ASCIIFantasy
{
    public class Combat
    {
        Player player;
        Enemy enemy;
        List<Character> listCharacters = new();
        Random rnd = new Random();
        int baseExp = 10;

        public Combat(Player _player,int levelCircle)
        {
            player = _player;
            listCharacters = player.listCharacters;
            enemy = CreateNewEnemy(levelCircle);
            Debug.WriteLine(enemy.stats.health);
            listCharacters[0].stats.StoreInitialStats();
            StartCombat();
        }

        public Combat(Player _player, Enemy _enemy)
        {
            player = _player;
            enemy = _enemy;
            listCharacters = player.listCharacters;
            listCharacters[0].stats.StoreInitialStats();
            StartCombat();
        }

        public Enemy CreateNewEnemy(int levelCircle)
        {
            //faire une liste d'enemies possible
            Random rnd = new Random();
            int level = 0;
            int indexEnemy = 0;
            
            switch (levelCircle)
            {
                case 0:
                    level = rnd.Next(1, 11);
                    indexEnemy = rnd.Next(0, 5);
                    break;
                case 1:
                    level = rnd.Next(11, 21);
                    indexEnemy = rnd.Next(0, 5);
                    break;
                case 2:
                    level = rnd.Next(21, 31);
                    indexEnemy = rnd.Next(5, 11);
                    break;
                case 3:
                    level = rnd.Next(31, 41);
                    indexEnemy = rnd.Next(5, 11);
                    break;
                case 4:
                    level = rnd.Next(41, 51);
                    indexEnemy = rnd.Next(5, 11);
                    break;
                case 5:
                    level = rnd.Next(51, 61);
                    indexEnemy = rnd.Next(5, 11);
                    break;
                case 6:
                    level = rnd.Next(61, 71);
                    indexEnemy = rnd.Next(5, 11);
                    break;
                case 7:
                    level = rnd.Next(71, 81);
                    indexEnemy = 11;
                    break;
                case 8:
                    level = rnd.Next(81, 91);
                    indexEnemy = 11;
                    break;
                case 9:
                    level = rnd.Next(91, 101);
                    indexEnemy = 11;
                    break;
            }
            Enemy enemy = EnemyList.instance.enemies.ElementAt(indexEnemy);
            enemy.stats.level = level;
            int difficulty = 0;
            if(enemy.stats.level>10 && enemy.stats.level<=31)
            {
                difficulty = 1;
            }
            else if(enemy.stats.level>35 && enemy.stats.level <= 80)
            {
                difficulty = 2;
            }
            else if(enemy.stats.level>81 && enemy.stats.level <= 100)
            {
                difficulty = 3;
            }
            enemy.difficultyLevel = difficulty;
            return enemy;
        }

        public void FieldGame()
        {
            Console.Write("+");
            for (int i = 0; i < 73; i++)
                Console.Write("-");
            Console.Write("+");
            Console.WriteLine();
        }
 
        public void FieldPlayer1(Character c1)
        {
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine("\t\t\t\t" + c1.name + "  Level "+c1.stats.level + "\n\t");
            c1.stats.ShowHealth();
            Console.Write("\t");
            c1.stats.ShowMana();
            Console.WriteLine("\n\n");
        }

        public void FieldPlayer2(Character c2)
        {
            Console.WriteLine();
            Console.WriteLine("\t\t\t\t"+c2.name +"  Level " + c2.stats.level + "\n\t");
            c2.stats.ShowHealth();
            Console.Write("\t");
            c2.stats.ShowMana();
            Console.WriteLine("\n\n");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }
        }

        public void FieldSetup(int turn)
        {
            FieldGame();
            FieldPlayer2(enemy);
            FieldPlayer1(listCharacters[0]);
            FieldGame();
        }

        public int MeleeAttack(int turn)
        {
            if (turn == 0)
            {
                Console.Clear();
                FieldGame();
                listCharacters[0].GetAttack("Melee").Use(listCharacters[0], enemy);
                Console.WriteLine(" End of " + listCharacters[0].name + "'s turn");
            }
            else
            {
                enemy.GetAttack("Melee").Use(enemy, listCharacters[0]);
                Console.WriteLine(" End of " + listCharacters[0].name + "'s turn");
            }
            return turn == 1 ? 0 : 1;
        }

        public int SpellChoice(int turn)
        {
            List<string> spells = listCharacters[0].GetListSpells();
            List<int> spellsCost = listCharacters[0].GetListCost();
            int length = spells.Count;
            string[] options = new string[length+1];
            options[0] = "Return";
            int selectedIndex = 0;

            for (int i = 0; i < length; i++)
            {
                options[i+1] = (spells[i] + " , Cost : " + spellsCost[i] + "mana");
            }
            while (turn == 0)
            {
                DisplayPlayerChoice(turn, options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    (turn, selectedIndex) = SpellCharacterChoice(turn, selectedIndex,spells);
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
        public (int, int) SpellCharacterChoice(int turn, int selectedIndex,List<string> spells)
        {
            if (selectedIndex == 0) 
            {
                Console.WriteLine(" " + listCharacters[0].name + " return to action choice");
                return (turn, selectedIndex);
            }
            else if (selectedIndex-1 < spells.Count)
            {
                string spellTmp = spells[selectedIndex-1];
                //execution du sort;
                Console.Clear();
                FieldGame();
                AttackType attackType = listCharacters[0].GetAttack(spellTmp).type;
                if (attackType == AttackType.Heal)
                {
                    if (listCharacters[0].GetAttack(spellTmp) is Heal healAttack)
                    {
                        healAttack.UseHeal(turn, listCharacters[0], listCharacters);
                    }
                }
                else
                {
                    listCharacters[0].GetAttack(spellTmp).Use(listCharacters[0], enemy);
                }
                Console.WriteLine(" End of " + listCharacters[turn].name + "'s turn");
                return (turn == 1 ? 0 : 1, selectedIndex);
            }
            else
            {
                Console.WriteLine(" Not a valid number");
                return (turn, selectedIndex);
            }
        }

        public (int,int) ChangeCharacterChoice(int turn, int selectedIndex, bool forceChange)
        {
            if ((selectedIndex == 0) && !forceChange)
            {
                Console.WriteLine(" " + listCharacters[0].name + " return to action choice");
                return (turn,selectedIndex);
            }
            else if (selectedIndex < listCharacters.Count)
            {
                Console.Clear();
                FieldGame();
                if(forceChange)
                    selectedIndex = GetIndexCharacterAliveToChange(turn, selectedIndex);
                Console.WriteLine(" " + listCharacters[0].name + " changed to " + listCharacters[selectedIndex].name);
                Character temp = listCharacters[0];
                listCharacters[0].stats.RestoreInitialStats();
                listCharacters[0] = listCharacters[selectedIndex];
                listCharacters[selectedIndex] = temp;
                listCharacters[0].stats.StoreInitialStats();
                
                return (turn == 1 ? 0 : 1, selectedIndex);
            }
            else
            {
                Console.WriteLine(" Not a valid number");
                return (turn, selectedIndex);
            }
        }

        public int GetIndexCharacterAliveToChange(int turn, int selectedIndex)
        {
            List<int> indexCharacterAlive = new List<int>();
            for (int i = 0; i < listCharacters.Count; i++)
            {
                if (!listCharacters[i].isDead)
                {
                    indexCharacterAlive.Add(i);
                }
            }
            return indexCharacterAlive[selectedIndex];
        }

        public int ChangeCharacter(int turn,bool forceChange = false)
        {
            List<string> options = new List<string>();
            int selectedIndex = 0;
            if (!forceChange)
            {
                options.Add("Return");
                for (int i = 1; i < listCharacters.Count; i++)
                {
                    if (!listCharacters[i].isDead)
                    {
                        options.Add(listCharacters[i].name);
                    }
                }
            }
            else
            {
                for (int i = 0; i < listCharacters.Count; i++)
                {
                    if (!listCharacters[i].isDead)
                    {
                        options.Add(listCharacters[i].name);
                    }
                }
            }
            
            while (turn == 0 )
            {
                DisplayPlayerChoiceWithHealth(turn, options, selectedIndex, forceChange);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    (turn, selectedIndex) = ChangeCharacterChoice(turn, selectedIndex, forceChange);
                    if (selectedIndex == 0)
                        break;
                }
                else
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.UpArrow:
                            selectedIndex = (selectedIndex - 1 + options.Count) % options.Count;
                            break;
                        case ConsoleKey.DownArrow:
                            selectedIndex = (selectedIndex + 1) % options.Count;
                            break;
                    }
                }
            }
            return turn;
        }

        public void DisplayPlayerChoiceWithHealth(int turn, List<string> options, int selectedIndex,bool forceChange)
        {
            this.FieldSetup(turn);
            for (int i = 0; i < options.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("> ");
                    Console.WriteLine(options[i]);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("  ");
                    Console.WriteLine(options[i]);
                }
                if(forceChange)
                {
                    selectedIndex = GetIndexCharacterAliveToChange(turn, selectedIndex);
                    listCharacters[selectedIndex].stats.ShowHealth();
                    listCharacters[selectedIndex].stats.ShowMana();
                }
                else
                {
                    if (i > 0 && i < listCharacters.Count)
                    {
                        listCharacters[i].stats.ShowHealth();
                        listCharacters[i].stats.ShowMana();
                    }
                }
            }
        }

        public void DisplayPlayerChoice(int turn, string[] options, int selectedIndex)
        {
            this.FieldSetup(turn);
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

        public int PlayerBaseChoice(int turn, int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    turn = MeleeAttack(turn);
                    return turn;
                case 1:
                    turn = SpellChoice(turn);
                    return turn;
                case 2:
                    if (turn == 0)
                        Console.Clear();
                    Console.WriteLine(listCharacters[0].ToString());
                    return turn;
                case 3:
                    turn = ChangeCharacter(turn);
                    return turn;
                case 4:
                    turn = Player.instance.inventory.SelectInventoryItemToUse(turn,Player.instance);
                    return turn;
                default:
                    Console.WriteLine(" Not a valid number");
                    return turn;
            }
        }

        public int ChoicePlayer(int turn)
        {
            Console.CursorVisible = false;
            string[] options = { "Attack", "Spells", "Stats", "Change Character" , "Inventory"};
            int selectedIndex = 0;

            while (turn == 0)
            {
                DisplayPlayerChoice(turn, options, selectedIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                Console.Clear();

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    turn = PlayerBaseChoice(turn, selectedIndex);
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

        public int ChoiceEnemy(int turn)
        {
            int choice = 0;
            Random rnd = new Random();
            if (enemy.GetListSpells().Count == 0 || enemy.difficultyLevel == 0 || 
                (enemy.stats.actual_mana < enemy.GetLowestSpellCost() && enemy.difficultyLevel >0))
            {
                turn = MeleeAttack(turn);
                return turn;
            }
            if (enemy.difficultyLevel >0) 
            {
                choice = rnd.Next(1, 3);

                if (enemy.difficultyLevel>2)
                {
                    if (Attack.IsElementalWeakness(enemy.GetAttack("Melee").element, listCharacters[0].element) &&
                        enemy.stats.attack >= enemy.stats.intelligence)
                    {
                        if (enemy.HaveBuffAttacks() && enemy.GetLowestBuffCost() <= enemy.stats.actual_mana)
                        {
                            choice = 2;
                        }
                        else
                        {
                            choice = 1;
                        }
                    }
                    else if (enemy.HaveEffectiveSpell(listCharacters[0].element) && enemy.GetLowestSpellCost() <= enemy.stats.actual_mana)
                    {
                        choice = 2;
                    }
                }

                switch (choice)
                {
                    case 1:
                        turn = MeleeAttack(turn);
                        return turn;
                    case 2:
                        turn = SpellChoiceEnemy(turn);
                        return turn;
                    default:
                        Console.WriteLine(" Not a valid number");
                        return turn;
                }
            }
            else
            {
                return turn;
            }
            
        }

        public int SpellChoiceEnemy(int turn)
        {
            int choixspell = 0;
            List<string> spells;
            List<int> spellsCost;
            string spellTmp = "";
            spells = enemy.GetListSpells();
            spellsCost = enemy.GetListCost();
            if(enemy.difficultyLevel >1)
            {
                bool goodChoice = false;
                while (!goodChoice)
                {
                    choixspell = rnd.Next(0, spells.Count);
                    spellTmp = spells[choixspell];
                    Attack tmpToUse = enemy.GetAttack(spellTmp);

                    if (tmpToUse.cost <= enemy.stats.actual_mana)
                    {
                        if (tmpToUse.type == AttackType.Buff)
                        {
                            if (enemy.difficultyLevel == 3)
                            {
                                Buff tmp = (Buff)tmpToUse;
                                if ((tmp.statToBuff == "attack" && enemy.stats.attack >= enemy.stats.intelligence) ||
                                    (tmp.statToBuff == "intelligence" && enemy.stats.attack < enemy.stats.intelligence))
                                {
                                    goodChoice = true;
                                    break;
                                }
                                if (tmp.statToBuff != "attack" && tmp.statToBuff != "intelligence")
                                {
                                    goodChoice = true;
                                    break;
                                }
                            }
                            goodChoice = true;
                            break;
                        }

                        else if (tmpToUse.type == AttackType.Spell && Attack.IsElementalWeakness(tmpToUse.element, listCharacters[0].element))
                        {
                            goodChoice = true;
                            break;
                        }
                        else
                        {
                            if (tmpToUse.type == AttackType.Heal)
                            {
                                if (enemy.stats.actual_hp < enemy.stats.health / 2)
                                {
                                    goodChoice = true;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }
            }
            //execution du sort;
            enemy.GetAttack(spellTmp).Use(enemy, listCharacters[0]);
            Console.WriteLine(" End of " + enemy.name + "'s turn");
            return turn == 1 ? 0 : 1;
        }

        public void GiveExp()
        {
            int expToGive = baseExp * enemy.stats.level;
            foreach (Character c in listCharacters)
            {
                c.stats.GetExp(expToGive);
            }
        }

        public void StartCombat()
        {
            Debug.Write("Combat started\n");
            int turn = 0;
            int winner;
            bool stillCharactersAlive = true;
            do
            {
                if (turn == 0)
                {
                    turn = this.ChoicePlayer(turn);
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                        Console.Write("-");
                    Console.WriteLine();
                    turn = this.ChoiceEnemy(turn);
                }
                stillCharactersAlive = false;
                foreach (Character p in player.listCharacters)
                {
                    if (!p.isDead)
                    {
                        stillCharactersAlive = true;
                        break;
                    }
                }
                if (player.listCharacters[0].isDead && stillCharactersAlive)
                {
                    this.ChangeCharacter(turn, true);
                }
                foreach(Character p in player.listCharacters)
                {
                    p.stats.RegenMana();
                }
                enemy.stats.RegenMana();

            } while (stillCharactersAlive && (enemy.stats.actual_hp > 0));
            winner = (enemy.stats.actual_hp > 0 ? 1 : 0);
            bool leave = false;
            do
            {
                if (winner == 0)
                {
                    Console.WriteLine($" {enemy.name} has been defeated !");
                    enemy.isDead = true;
                    GiveExp();
                }
                else
                {
                    Console.WriteLine(" You lost !");
                }
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    leave = true;
                }
            } while (!leave);
           
        }

       /* static void Main(string[] args)
        {
            Player player = new Player();
            Character char1 = new Character("Player",Element.Neutral, 100, 100, 1, 10, 5, 10, 10);
            Character char2 = new Character("VICTROR",Element.Neutral, 100, 100, 1, 10, 10, 10, 10);
            Character enemy = new Character("Enemy",Element.Grass, 50, 50, 30, 5, 5, 5, 5);
            player.listCharacters.Add(char1);
            player.listCharacters.Add(char2);
            Spell fireball = new Fireball();
            Buff ryuMonsho = new RyuMonsho();
            Heal heal = new SmallHeal();
            char1.AddAttack(fireball);
            char2.AddAttack(ryuMonsho);
            char2.AddAttack(heal);
            enemy.AddAttack(fireball);
            char1.stats.StoreInitialStats();
            Combat combat = new Combat(player, enemy);
            int turn = 0;
            int winner;
            bool stillCharactersAlive = true;
            do
            {
                if (turn == 0)
                {
                    turn = combat.ChoicePlayer(turn);
                }
                else
                {
                    for (int i = 0; i < 25; i++)
                        Console.Write("-");
                    Console.WriteLine();
                    turn = combat.ChoiceEnemy(turn);
                }
                stillCharactersAlive = false;
                foreach (Character p in player.listCharacters)
                {
                    if (!p.isDead)
                    {
                        stillCharactersAlive = true;
                        break;
                    }
                }
                if (player.listCharacters[0].isDead && stillCharactersAlive)
                {
                    combat.ChangeCharacter(turn, true);
                }

            } while (stillCharactersAlive && (enemy.stats.actual_hp > 0));
            winner = (enemy.stats.actual_hp > 0 ? 1 : 0);
            if(winner == 0)
            {
                Console.WriteLine($" {enemy.name} has been defeated !");
            }
            else
            {
                Console.WriteLine(" You lost !");
            }
        }*/

    }

}
