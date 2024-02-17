using System;
using ASCIIFantasy;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class EnemyList
    {
        public static EnemyList instance { get; set; }
        public List<Enemy> enemies = new List<Enemy>();

        public EnemyList()
        {
            CreateEnemies();
        }
        public static EnemyList CreateInstance()
        {
            if (instance == null)
            {
                instance = new EnemyList();
            }
            return instance;
        }

        public void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void RemoveEnemy(Enemy enemy)
        {
            enemies.Remove(enemy);
        }

        public Enemy GetEnemy(string name)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.name == name)
                {
                    return enemy;
                }
            }
            throw new Exception("Enemy not found");
        }

        public void CreateEnemies()
        {

            enemies.Add(new Slime());
            enemies.Add(new Goblin());
            enemies.Add(new Skeleton());
            enemies.Add(new Spider());
            enemies.Add(new Rat());
            enemies.Add(new FireSpirit());
            enemies.Add(new GrassSpirit());
            enemies.Add(new WaterSpirit());
            enemies.Add(new GroundSpirit());
            enemies.Add(new LightSpirit());
            enemies.Add(new DarkSpirit());
            enemies.Add(new Dragon());
        }
    }   
    public class Enemy : Character
    {
        public int difficultyLevel { get; set; }

        public Enemy() : base("Unnamed", Element.Neutral, 0, 0, 0, 0, 0, 0, 0) 
        {
            difficultyLevel = 1;
        }
        public Enemy(string n, Element _element, int hp, int man, int atk, int def, int intel, int agi, int luc) : base(n,_element,hp,man,atk,def,intel,agi,luc)
        {
            difficultyLevel = 1;
        }
    }
        
    public class Slime : Enemy
    { 
        public Slime() : base()
        {
            Random rnd = new Random();
            name = "Slime";
            element = Element.Neutral;
            stats.health = rnd.Next(10, 50);
            stats.attack = rnd.Next(1, 10);
            stats.defense = rnd.Next(1, 10);
            stats.agility = rnd.Next(1, 10);
            stats.luck = rnd.Next(1, 10);
            stats.SetActualHealthAndMana();
        }    
    }

    public class Goblin : Enemy
    {
        public Goblin() : base()
        {
            Random rnd = new Random();
            name = "Grass Goblin";
            element = Element.Grass;
            stats.health = rnd.Next(20, 60);
            stats.attack = rnd.Next(5, 15);
            stats.defense = rnd.Next(3, 10);
            stats.agility = rnd.Next(5, 15);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
        }
    }

    public class Skeleton : Enemy
    {
        public Skeleton() : base()
        {
            Random rnd = new Random();
            name = "Skeleton";
            element = Element.Dark;
            stats.health = rnd.Next(15, 40);
            stats.attack = rnd.Next(8, 20);
            stats.defense = rnd.Next(1, 8);
            stats.agility = rnd.Next(5, 15);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
        }
    }

    public class Spider : Enemy
    {
        public Spider() : base()
        {
            Random rnd = new Random();
            name = "Spider";
            element = Element.Neutral;
            stats.health = rnd.Next(10, 30);
            stats.attack = rnd.Next(3, 12);
            stats.defense = rnd.Next(2, 6);
            stats.agility = rnd.Next(10, 20);
            stats.luck = rnd.Next(1, 10);
            stats.SetActualHealthAndMana();
        }
    }

    public class Rat : Enemy
    {
        public Rat() : base()
        {
            Random rnd = new Random();
            name = "Rat";
            element = Element.Ground;
            stats.health = rnd.Next(10, 25);
            stats.attack = rnd.Next(1, 8);
            stats.defense = rnd.Next(1, 5);
            stats.agility = rnd.Next(10, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
        }
    }

    public class FireSpirit : Enemy
    {
        public FireSpirit() : base()
        {
            Random rnd = new Random();
            name = "Fire Spirit";
            element = Element.Fire;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell,"Fireball"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff,"BookWorm"));
        }
    }

    public class GrassSpirit : Enemy
    {
        public GrassSpirit() : base()
        {
            Random rnd = new Random();
            name = "Grass Spirit";
            element = Element.Grass;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Vine Whip"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
        }
    }

    public class WaterSpirit : Enemy
    {
        public WaterSpirit() : base()
        {
            Random rnd = new Random();
            name = "Water Spirit";
            element = Element.Water;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Water Gun"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
        }
    }

    public class GroundSpirit : Enemy
    {
        public GroundSpirit() : base()
        {
            Random rnd = new Random();
            name = "Ground Spirit";
            element = Element.Ground;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Earthquake"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
        }
    }

    public class LightSpirit : Enemy
    {
        public LightSpirit() : base()
        {
            Random rnd = new Random();
            name = "Light Spirit";
            element = Element.Light;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Light Beam"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
        }
    }

    public class DarkSpirit : Enemy
    {
        public DarkSpirit() : base()
        {
            Random rnd = new Random();
            name = "Dark Spirit";
            element = Element.Dark;
            stats.health = rnd.Next(40, 140);
            stats.mana = rnd.Next(40, 200);
            stats.attack = rnd.Next(5, 10);
            stats.defense = rnd.Next(3, 8);
            stats.intelligence = rnd.Next(10, 30);
            stats.agility = rnd.Next(8, 20);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Dark Beam"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
        }
    }

    public class Dragon : Enemy
    {         
        public Dragon() : base()
        {
            Random rnd = new Random();
            name = "Fire Dragon";
            element = Element.Fire;
            stats.health = rnd.Next(500, 1500);
            stats.mana = rnd.Next(200, 800);
            stats.attack = rnd.Next(30, 90);
            stats.defense = rnd.Next(20, 50);
            stats.intelligence = rnd.Next(20, 70);
            stats.agility = rnd.Next(5, 15);
            stats.luck = rnd.Next(1, 5);
            stats.SetActualHealthAndMana();
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Spell, "Fireball"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "BookWorm"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "Bulk Up"));
            listAttack.Add(ListAttackGlobal.instance.GetAttack(AttackType.Buff, "Ryu Monsho"));
        }
    }   

}
