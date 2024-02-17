using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class BulkUp : Buff
    {
        public BulkUp() : base("Bulk Up", Element.Neutral, 10, 20, 15,"attack") { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

        public override void BuffStats(Character attacker, Character receiver)
        {
            int ptsOfBuff = (attacker.stats.attack * power) / 100;
            Console.WriteLine($" {attacker.name} buffed by "+ptsOfBuff+" points his attack stat!");
            attacker.stats.IncrementAttack(ptsOfBuff);
        }
    }

    public class RyuMonsho : Buff
    {
        public RyuMonsho() : base("Ryu Monsho", Element.Neutral, 30, 50, 40, "attack") { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

        public override void BuffStats(Character attacker, Character receiver)
        {
            int ptsOfBuff = power;
            Console.WriteLine($" {attacker.name} buffed by " + ptsOfBuff + " points his attack stat!");
            attacker.stats.IncrementAttack(ptsOfBuff);
        }
    }

    public class BookWorm : Buff
    {
        public BookWorm() : base("BookWorm", Element.Neutral, 10, 20, 15,"intelligence") { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

        public override void BuffStats(Character attacker, Character receiver)
        {
            int ptsOfBuff = (attacker.stats.intelligence * power) / 100;
            Console.WriteLine($" {attacker.name} buffed by " + ptsOfBuff + " points his intelligence stat!");
            attacker.stats.IncrementAttack(ptsOfBuff);
        }
    }

    public class Evasion : Buff
    {
        public Evasion() : base("Evasion", Element.Neutral, 10, 15, 15, "agility") { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

        public override void BuffStats(Character attacker, Character receiver)
        {
            int ptsOfBuff = (attacker.stats.agility * power) / 100;
            Console.WriteLine($" {attacker.name} buffed by " + ptsOfBuff + " points his agility stat!");
            attacker.stats.IncrementAttack(ptsOfBuff);
        }
    }

    public class Luckier : Buff
    {
        public Luckier() : base("Luckier", Element.Neutral, 5, 10, 15, "luck") { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

        public override void BuffStats(Character attacker, Character receiver)
        {
            int ptsOfBuff = (attacker.stats.luck * power) / 100;
            Console.WriteLine($" {attacker.name} buffed by " + ptsOfBuff + " points his luck stat!");
            attacker.stats.IncrementAttack(ptsOfBuff);
        }
    }

}
