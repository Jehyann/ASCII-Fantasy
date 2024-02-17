using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class Fireball : Spell
    { 
        public Fireball() : base("Fireball",Element.Fire,5,10,10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

    public class VineWhip : Spell
    {
        public VineWhip() : base("Vine Whip", Element.Grass, 5, 10, 10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

    public class WaterGun : Spell
    {
        public WaterGun() : base("Water Gun", Element.Water, 5, 15, 10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

    public class LightBeam : Spell
    {
        public LightBeam() : base("Light Beam", Element.Light, 5, 10, 10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

    public class DarkBeam : Spell
    {
        public DarkBeam() : base("Dark Beam", Element.Dark, 5, 10, 10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

    public class Earthquake : Spell
    {
        public Earthquake() : base("Earthquake", Element.Ground, 5, 10, 10) { }

        public override void Use(Character attacker, Character receiver)
        {
            base.Use(attacker, receiver);
        }

    }

}
