using System;
using ASCIIFantasy;

namespace ASCIIFantasy
{
    public class Spell : Attack
    {
        public Spell(string _name, Element _element,int _power, int _cost, int _level) : base(_name, _element, _power, _cost, _level)
        {
            type = AttackType.Spell;
        }

        public override void Use(Character attacker, Character receiver)
        {
            
            int mana = attacker.stats.actual_mana;
            if ((mana - cost) >= 0)
            {
                bool crit = IsCriticalHit(attacker.stats.luck);
                bool dodged = IsDodged(receiver.stats.agility);
                Console.WriteLine($" {attacker.name} used {attack_name}");
                int damage = DamageCalculation(attacker, receiver);
                attacker.stats.IncrementMana(-cost);
                if (dodged)
                {
                    Console.WriteLine($" But {receiver.name} dodged !");
                }
                else if (crit)
                {
                    damage *= 2;
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
            }
            else
            {
                Console.WriteLine($" {attacker.name} tried using {attack_name} but forgot to look at his mana");
                Console.WriteLine($" {attacker.name} lost his turn because of that...");
            }
            IsCharacterDead(receiver);
        }

        public int DamageCalculation(Character attacker, Character receiver)
        {
            int tmpDamage = rnd.Next(attacker.stats.intelligence + 1) + power;
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
