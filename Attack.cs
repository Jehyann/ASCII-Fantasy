using ASCIIFantasy;

namespace ASCIIFantasy
{
    public enum AttackType 
    {
        Physical,
        Spell,
        Buff,
        Heal,
    }
    public enum Element
    {
        Fire,
        Water,
        Grass,
        Ground,
        Light,
        Dark,
        Neutral,
    }

    public class Attack
    {
        public AttackType type { get; set; }
        public Element element { get; set; }
        public int cost { get; set; }
        public int power { get; set; }
        public string attack_name { get; set; }

        public int level { get; set; }

        public static Random rnd { get; set; } = rnd = new Random();

        public Attack(){}
        public Attack(string _name, Element _element, int _power, int _cost, int _level)
        {
            attack_name = _name;
            element = _element;
            power = _power;
            cost = _cost;
            level = _level;
        }

        public virtual void Use(Character attacker, Character receiver){ }

        public void IsCharacterDead(Character character)
        {
            if (character.stats.actual_hp <= 0)
            {
                character.stats.actual_hp = 0;
                character.isDead = true;
            }
        }

        // Verifies if the attack is a critical hit.
        protected bool IsCriticalHit(int luck)
        {
            double chance = 100 * (1 - Math.Exp(-luck / 100.0));
            return rnd.Next(100) + 1 <= chance;
        }

        // Verifies if the receiver of the attack dodged.
        protected bool IsDodged(int agility)
        {
            double chance = 100 * (1 - Math.Exp(-agility / 100.0));
            return rnd.Next(100) + 1 <= chance;
        }

        public static bool IsElementalWeakness(Element element, Element receiverElement)
        {
            if(element == Element.Fire && receiverElement == Element.Grass)
                return true;
            if(element == Element.Water && receiverElement == Element.Fire)
                return true;
            if(element == Element.Grass && receiverElement == Element.Ground)
                return true;
            if(element == Element.Ground && receiverElement == Element.Water)
                return true;
            if(element == Element.Light && receiverElement == Element.Dark)
                return true;
            if(element == Element.Dark && receiverElement == Element.Light)
                return true;
            return false;
        }

        public static bool IsElementalResistance(Element element, Element receiverElement)
        {
            if(element == Element.Fire && receiverElement == Element.Water)
                return true;
            if(element == Element.Water && receiverElement == Element.Ground)
                return true;
            if(element == Element.Grass && receiverElement == Element.Fire)
                return true;
            if(element == Element.Ground && receiverElement == Element.Grass)
                return true;
            if(element == Element.Light && receiverElement == Element.Dark)
                return true;
            if(element == Element.Dark && receiverElement == Element.Light)
                return true;
            return false;
        }

    }

    public class ListAttackGlobal
    {

        public static ListAttackGlobal instance { get; set; }
        public List<Attack> listAttackPhysical { get; set; }
        public List<Attack> listAttackSpell { get; set; }
        public List<Attack> listAttackHeal { get; set; }
        public List<Attack> listAttackBuff { get; set; }

        public ListAttackGlobal()
        {
            listAttackPhysical = new List<Attack> 
            {
                new Physical("Melee", Element.Neutral, 0, 0, 0),  
            };

            listAttackSpell = new List<Attack>
            {
                new Fireball(), 
                new VineWhip(),
                new WaterGun(),
                new LightBeam(),
                new DarkBeam(),
                new Earthquake(),
            };

            listAttackHeal = new List<Attack>
            {
                new SmallHeal(),
                new MediumHeal(),
                new BigHeal(),
                new MegaHeal(),
            };

            listAttackBuff = new List<Attack>
            {
                new BulkUp(),
                new BookWorm(),
                new Evasion(),
                new Luckier(),
                new RyuMonsho(),
            };

        }

        public static ListAttackGlobal CreateInstance()
        {
            if (instance == null)
                instance = new ListAttackGlobal();
            return instance;
        }

        public Attack GetAttack(AttackType type,string name)
        {
            List<Attack> listToCheck;
            switch(type)
            {
                case AttackType.Physical:
                    listToCheck = listAttackPhysical;
                    break;
                case AttackType.Spell:
                    listToCheck = listAttackSpell;
                    break;
                case AttackType.Heal:
                    listToCheck = listAttackHeal;
                    break;
                case AttackType.Buff:
                    listToCheck = listAttackBuff;
                    break;
                default:
                    throw new Exception("List type not found");
            }
            foreach (Attack attack in listToCheck)
            {
                if (attack.attack_name == name)
                    return attack;
            }
            throw new Exception("Attack not found");
        }

    }
}