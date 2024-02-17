using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class StatsCharacter
    {
        public int health { get; set; }
        public int mana{ get; set; }
        public int attack{ get; set; }
        public int defense{ get; set; }
        public int intelligence{ get; set; }
        public int agility { get; set; }
        private int speed;
        public int luck { get; set; }
        public int actual_hp { get; set; }
        public int actual_mana { get; set; }

        private Dictionary<string, int> initialStats = new Dictionary<string, int>();
        public List<int> statsList = new List<int>();

        private int gearBonusHealth;
        private int gearBonusMana;
        private int gearBonusAttack;
        private int gearBonusDefense;
        private int gearBonusIntelligence;
        private int gearBonusAgility;
        private int gearBonusLuck;




        public int level { get; set; } = 1;
        public int experience { get; set; } = 0;
        public int experienceToNextLevel { get; set; } = 100;
        public int competencePointsAvailable { get; set; } = 0;

        public StatsCharacter()
        {
            health = 0;
            mana = 0;
            attack = 0;
            defense = 0;
            intelligence = 0;
            agility = 0;
            luck = 0;
            actual_hp = health;
            actual_mana = mana;
            statsList.Add(health);
            statsList.Add(mana);
            statsList.Add(attack);
            statsList.Add(defense);
            statsList.Add(intelligence);
            statsList.Add(agility);
            statsList.Add(luck);
        }

        public StatsCharacter(int health, int mana, int attack, int defense, int intelligence, int agility, int luck)
        {
            this.health = health;
            this.mana = mana;
            this.attack = attack;
            this.defense = defense;
            this.intelligence = intelligence;
            this.agility = agility;
            this.luck = luck;
            this.actual_hp = health;
            this.actual_mana = mana;
            statsList.Add(health);
            statsList.Add(mana);
            statsList.Add(attack);
            statsList.Add(defense);
            statsList.Add(intelligence);
            statsList.Add(agility);
            statsList.Add(luck);
        }

        public void SetStats(int health, int mana, int attack, int defense, int intelligence, int agility, int luck)
        {
            this.health = health;
            this.mana = mana;
            this.attack = attack;
            this.defense = defense;
            this.intelligence = intelligence;
            this.agility = agility;
            this.luck = luck;
            this.actual_hp = health;
            this.actual_mana = mana;
        }

        public void IncrementHealth(int i)
        {
            actual_hp += i;
            if (actual_hp > health) actual_hp = health;
            else if (actual_hp < 0) actual_hp = 0;
        }

        public void IncrementMana(int i)
        {
            actual_mana += i;
            if (actual_mana > mana) actual_mana = mana;
        }

        public void IncrementAttack(int i)
        {
            attack += i;
        }

        public void IncrementDefense(int i)
        {
            defense += i;
        }


        public void IncrementIntel(int i)
        {
            intelligence += i;
        }

        public void IncrementAgility(int i)
        {
            agility += i;
        }

        public void IncrementLuck(int i)
        {
            luck += i;
        }
 
        public void SetActualHealthAndMana()
        {
            actual_hp = health;
            actual_mana = mana;
        }

        public int GetBonusHealth()
        {
            return gearBonusHealth;
        }

        public int GetBonusMana()
        {
            return gearBonusMana;
        }
        public int GetBonusAttack()
        {
            return gearBonusAttack;
        }
        public int GetBonusDefense()
        {
            return gearBonusDefense;
        }

        public int GetBonusIntelligence()
        {
            return gearBonusIntelligence;
        }
        public int GetBonusAgility()
        {
            return gearBonusAgility;
        }

        public int GetBonusLuck()
        {
            return gearBonusLuck;
        }

        public void SetBonusHealth(int i)
        {
            gearBonusHealth = i;
        }

        public void SetBonusMana(int i)
        {
            gearBonusMana = i;
        }
        public void SetBonusAttack(int i)
        {
            gearBonusAttack = i;
        }
        public void SetBonusDefense(int i)
        {
            gearBonusDefense = i;
        }
        public void SetBonusIntelligence(int i)
        {
            gearBonusIntelligence = i;
        }
        public void SetBonusAgility(int i)
        {
            gearBonusAgility = i;
        }
        public void SetBonusLuck(int i)
        {
            gearBonusLuck = i;
        }


        public void ShowHealth()
        {
            Console.Write(" Health: ");
            int healthPercentage = (int)((double)actual_hp / health * 100);
            int totalSegments = 20;
            int filledSegments = healthPercentage / 5;
            if (healthPercentage > 50)
                Console.ForegroundColor = ConsoleColor.Green;
            else if (healthPercentage > 25)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < filledSegments; i++)
                Console.Write("█");
            Console.ResetColor();
            for (int i = filledSegments; i < totalSegments; i++)
                Console.Write("_");
            Console.Write(" " + actual_hp + "/" + health);
        }

        public void ShowMana()
        {
            Console.Write(" Mana: ");
            int manaPercentage;
            if (mana > 0)
            {
                manaPercentage = (int)((double)actual_mana / mana * 100);
            }
            else
            {
                manaPercentage = 0;
            }
            int totalSegments = 20;
            int filledSegments = manaPercentage / 5;
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < filledSegments; i++)
            {
                Console.Write("█");
            }
            Console.ResetColor();
            for (int i = filledSegments; i < totalSegments; i++)
            {
                Console.Write("_");
            }
            Console.WriteLine(" " + actual_mana + "/" + mana);
        }

        public void RegenMana()
        {
            IncrementMana(mana / 20);
        }
        public void StoreInitialStats()
        {
            initialStats["attack"] = attack;
            initialStats["defense"] = defense;
            initialStats["intelligence"] = intelligence;
            initialStats["agility"] = agility;
            initialStats["luck"] = luck;
        }

        public void RestoreInitialStats()
        {
            Debug.WriteLine($"{initialStats["attack"]}");
            attack = initialStats["attack"];
            defense = initialStats["defense"];
            intelligence = initialStats["intelligence"];
            agility = initialStats["agility"];
            luck = initialStats["luck"];
        }

        public void GetExp(int exp)
        {
            Console.WriteLine(" You gained " + exp + " experience points!");
            experience += exp;
            if (experience >= experienceToNextLevel)
            {
                LevelUp();
               
            }
        }

        public void UpdateStatsList()
        {
            statsList[0] = health;
            statsList[1] = mana;
            statsList[2] = attack;
            statsList[3] = defense;
            statsList[4] = intelligence;
            statsList[5] = agility;
            statsList[6] = luck;
        }

        public void LevelUp()
        {
            level++;
            Console.WriteLine($" You leveled up to level {level}!");
            health += 50;
            mana += 20;
            attack += 5;
            defense += 1;
            intelligence += 5;
            agility += 5;
            luck += 5;
            experience -= experienceToNextLevel;
            UpdateStatsList();
            int newExperienceToNextLevel = level * 200;
            experienceToNextLevel = newExperienceToNextLevel;
        }
    }
}
