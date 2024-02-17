using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class GearPiece
    {
        public static GearPiece instance;
        public bool isNull = false;
        public bool isEquiped = false;

        public string gearName { get; set; }
        public enum GearType
        {
            Head,
            Chest,
            Legs,
            Feet,
            Weapon
        }
        public GearType type;

        public int bonusHealth { get; set; }
        public int bonusMana { get; set; }
        public int bonusAttack { get; set; }
        public int bonusDefense { get; set; }
        public int bonusIntelligence { get; set; }
        public int bonusAgility { get; set; }
        public int bonusLuck { get; set; }


        public GearPiece CreateNewGear(GearType type, string _name, bool _isNull = false , int _bonusHealth = 0, int _bonusMana = 0, int _bonusAttack = 0, int _bonusDefense = 0, int _bonusIntelligence = 0, int _bonusAgility = 0, int _bonusLuck = 0)
        {
           GearPiece gear =  new GearPiece();
            gear.type = type;
            gear.gearName = _name;
            gear.bonusHealth = _bonusHealth;
            gear.bonusMana = _bonusMana;
            gear.bonusAttack = _bonusAttack;
            gear.bonusDefense = _bonusDefense;
            gear.bonusIntelligence = _bonusIntelligence;
            gear.bonusAgility = _bonusAgility;
            gear.bonusLuck = _bonusLuck;
            gear.isNull = _isNull;
            return gear;
        }


        public static GearPiece CreateInstance()
        {
            if (instance == null)
            {
                instance = new GearPiece();
            }
            return instance;
        }

    }
}
