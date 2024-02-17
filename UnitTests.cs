using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ASCIIFantasy.Tests
{
    public class UnitTests
    {
        static Enemy enemy;
        static Player player;
        static Spell spell1;
        static Spell spell2;
        static Spell spell3;

        [SetUp]
        public void Setup()
        {
            enemy = new Enemy("Fire Spirit",Element.Fire,100,100,5,5,5,5,5);
            player = new Player();
            Character char1 = new Character("Char1", Element.Neutral, 100, 100, 10, 10, 10, 10, 10);
            spell1 = new Spell("Water Gun", Element.Water, 5, 10, 10);
            spell2 = new Spell("Vine Whip", Element.Grass, 5, 10, 10);
            spell3 = new Spell("Light Beam", Element.Light, 5, 10, 10);
            char1.AddAttack(ListAttackGlobal.instance.GetAttack(AttackType.Spell,"Water Gun"));
            char1.AddAttack(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Vine Whip"));
            char1.AddAttack(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Light Beam"));
        }


        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]

        public void TestCombat(int testNumber)
        {
            int enemyBaseHealth = 100;
            int resultHealthSuperEffective = 100 - (24 + 5);
            int resultHealthNotVeryEffective = 100 - (6 + 5);
            int resultHealthNormal = 100 - (12 + 5);
            int resultHealthCritical = 100 - (24 + 5);
            int resultHealthSuperEffectiveCritical = 100 - (48 + 5);
            int resultHealthDodged = 100;
            int resultHealthNoMana = 100;
            int resultHealthDead = 0;
            switch(testNumber)
            {
                case 1:
                    UseSpellTest(player, enemy, false, false, spell1);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthSuperEffective));
                    break;
                case 2:
                    UseSpellTest(player, enemy, false, false, spell2);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthNotVeryEffective));
                    break;
                case 3:
                    UseSpellTest(player, enemy, false, false, spell3);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthNormal));
                    break;
                case 4:
                    UseSpellTest(player, enemy, true, false, spell1);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthCritical));
                    break;
                case 5:
                    UseSpellTest(player, enemy, true, false, spell1);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthSuperEffectiveCritical));
                    break;
                case 6:
                    UseSpellTest(player, enemy, false, true, spell3);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthDodged));
                    break;
                case 7:
                    player.stats.IncrementMana(-100);
                    UseSpellTest(player, enemy, false, false, spell3);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthNoMana));
                    break;
                case 8:
                    enemy.stats.actual_hp = 0;
                    UseSpellTest(player, enemy, false, false, spell3);
                    Assert.That(enemy.stats.actual_hp, Is.EqualTo(resultHealthDead));
                    break;
            }
           
            
        }

        public void UseSpellTest(Character attacker, Character receiver, bool crit, bool dodged, Attack spell)
        {
            int mana = attacker.stats.actual_mana;
            if ((mana - 10) >= 0)
            {
                int damage = DamageCalculation(attacker, receiver, spell);
                attacker.stats.IncrementMana(-spell.cost);
                if (dodged)
                {
                }
                else if (crit)
                {
                    damage *= 2;
                    damage -= receiver.stats.defense;
                    if (damage < 0)
                        damage = 0;
                    receiver.stats.IncrementHealth(-damage);
                }
                else
                {
                    damage -= receiver.stats.defense;
                    if (damage < 0)
                        damage = 0;
                    receiver.stats.IncrementHealth(-damage);
                }
            }
            else
            {
            }
            IsCharacterDead(receiver);
        }

        public void IsCharacterDead(Character character)
        {
            if (character.stats.actual_hp <= 0)
            {
                character.stats.actual_hp = 0;
                character.isDead = true;
            }
        }
        public int DamageCalculation(Character attacker, Character receiver, Attack spell)
        {
            int tmpDamage = 7 + spell.power;
            if (Attack.IsElementalWeakness(spell.element, receiver.element))
            {

                tmpDamage *= 2;
            }
            else if (Attack.IsElementalResistance(spell.element, receiver.element))
            {

                tmpDamage /= 2;
            }
            return tmpDamage;
        }
    }
}
