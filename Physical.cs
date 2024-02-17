using System;
using System.Xml.Linq;
using ASCIIFantasy;

namespace ASCIIFantasy
{
    public class Physical : Attack
    {
        public Physical(string _name, Element _element, int _power, int _cost, int _level) : base(_name, _element, _power, _cost, _level)
        {
            this.type = AttackType.Physical;
        }

        public override void Use(Character attacker, Character receiver)
        {
            bool crit = IsCriticalHit(attacker.stats.luck);
            bool dodged = IsDodged(receiver.stats.agility);
            Console.WriteLine($" {attacker.name} used {attack_name}");
            int damage = DamageCalculation(attacker, receiver);
            if (dodged)
            {
                Console.WriteLine($" But {receiver.name} dodged !");
            }
            else if (crit)
            {
                damage = damage * 2;
                damage -= rnd.Next(receiver.stats.defense + 1);
                if (damage < 0)
                    damage = 0;
                receiver.stats.IncrementHealth(-damage);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($" Critical hit ! {receiver.name} received {damage} damage!");
                Console.ResetColor();
            }
            else
            {
                damage -= rnd.Next(receiver.stats.defense + 1);
                if (damage < 0)
                    damage = 0;
                receiver.stats.IncrementHealth(-damage);
                Console.WriteLine($" {receiver.name} received {damage} damage!");
            }
            IsCharacterDead(receiver);
        }

        public int DamageCalculation(Character attacker, Character receiver)
        {
            int tmpDamage = rnd.Next(attacker.stats.attack + 1);
            if (Attack.IsElementalWeakness(element, receiver.element))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" It's super effective !");
                Console.ResetColor();
                tmpDamage *= 2;
            }
            else if (Attack.IsElementalResistance(element, receiver.element))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" It's not very effective...");
                Console.ResetColor();
                tmpDamage /= 2;
            }
            return tmpDamage;
        }
    }
}
