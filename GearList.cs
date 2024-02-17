using ASCIIFantasy;
using System;

class GearList
{
    public List<GearPiece> listGear = new List<GearPiece>();
    public static GearList instance;

    GearPiece startHead = GearPiece.CreateInstance().CreateNewGear(
        GearPiece.GearType.Head,
        "Starter Head",
        _bonusHealth: 1,
        _bonusIntelligence: 1
        );

    GearPiece startChest = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Chest,
        "Starter Chest",
        _bonusHealth: 1,
        _bonusDefense: 1
        );

    GearPiece startLegs = GearPiece.instance.CreateNewGear(
    GearPiece.GearType.Legs,
    "Starter Legs",
    _bonusHealth: 1,
    _bonusDefense: 1
    );

    GearPiece startBoots = GearPiece.instance.CreateNewGear(
    GearPiece.GearType.Feet,
    "Starter Boots",
    _bonusHealth: 1,
    _bonusAgility: 1
    );

    GearPiece startWeaponStaff = GearPiece.instance.CreateNewGear(
   GearPiece.GearType.Weapon,
   "Starter Staff",
   _bonusIntelligence: 2
   );

    GearPiece startWeaponSword = GearPiece.instance.CreateNewGear(
   GearPiece.GearType.Weapon,
   "Starter Sword",
   _bonusAttack: 2
   );

    GearPiece tapionegide = GearPiece.instance.CreateNewGear(
    GearPiece.GearType.Weapon,
    "Tapionegide",
    _bonusAttack: 50,
    _bonusIntelligence: 30,
    _bonusAgility: 10,
    _bonusLuck: 20
);

    GearPiece SwordDawn = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Weapon,
        "Sword of Dawn",
        _bonusAttack: 20,
        _bonusLuck: 3
);
    GearPiece HeadDawn = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Head,
        "Helm of Dawn",
        _bonusIntelligence: 15,
        _bonusMana: 10,
        _bonusLuck: 2
);
    GearPiece ChestDawn = GearPiece.instance.CreateNewGear
        (
               GearPiece.GearType.Chest,
               "Armor of Dawn",
                _bonusHealth: 20,
                _bonusDefense: 10,
                _bonusLuck: 5
        );
    GearPiece LegsDawn = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Chest,
        "Shorts of  Dawn",
        _bonusHealth: 20,
        _bonusDefense: 10,
        _bonusLuck: 2
);
    GearPiece FeetDawn = GearPiece.instance.CreateNewGear(
         GearPiece.GearType.Feet,
         "Boots of Dawn",
         _bonusAgility: 10,
         _bonusLuck: 5
 );
    GearPiece StaffSorcererKing = GearPiece.instance.CreateNewGear(
    GearPiece.GearType.Weapon,
    "Staff of The Sorcerer King",
    _bonusIntelligence: 20,
    _bonusMana: 50
);
    GearPiece HeadSorcererKing = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Head,
        "Hat of The Sorcerer King",
        _bonusIntelligence: 30
);
    GearPiece ChestSorcererKing = GearPiece.instance.CreateNewGear
        (
               GearPiece.GearType.Chest,
               "Toga of The Sorcerer King",
                _bonusHealth: 20,
                _bonusDefense: 5,
                _bonusMana: 20
        );
    GearPiece LegsSorcererKing = GearPiece.instance.CreateNewGear(
        GearPiece.GearType.Chest,
        "Skirt of  The Sorcerer King",
       _bonusIntelligence: 25,
       _bonusMana: 20
);
    GearPiece FeetSorcererKing = GearPiece.instance.CreateNewGear(
         GearPiece.GearType.Feet,
         "Pump of The Sorcerer King",
         _bonusIntelligence: 15,
         _bonusLuck: 5
 );



    public GearList()
    {
        listGear.Add(startHead);
        listGear.Add(startChest);
        listGear.Add(startLegs);
        listGear.Add(startBoots);
        listGear.Add(startWeaponSword);
        listGear.Add(startWeaponStaff);
        listGear.Add(SwordDawn);
        listGear.Add(HeadDawn);
        listGear.Add(ChestDawn);
        listGear.Add(LegsDawn);
        listGear.Add(FeetDawn);
        listGear.Add(StaffSorcererKing);
        listGear.Add(HeadSorcererKing);
        listGear.Add(ChestSorcererKing);
        listGear.Add(LegsSorcererKing);
        listGear.Add(FeetSorcererKing);
        listGear.Add(tapionegide);
    }

    public static GearList CreateInstance()
    {
        if (instance == null)
        {
            instance = new GearList();
        }
        return instance;
    }
}