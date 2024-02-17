using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class Gear
    {
        public List<GearPiece> pieces = new();
        public GearPiece head { get; set; } = GearPiece.CreateInstance().CreateNewGear(GearPiece.GearType.Head, "", true);
        public GearPiece chest { get; set; } = GearPiece.instance.CreateNewGear(GearPiece.GearType.Chest, "", true);
        public GearPiece legs { get; set; } = GearPiece.instance.CreateNewGear(GearPiece.GearType.Legs, "", true);
        public GearPiece feet { get; set; } = GearPiece.instance.CreateNewGear(GearPiece.GearType.Feet, "", true);
        public GearPiece weapon { get; set; } = GearPiece.instance.CreateNewGear(GearPiece.GearType.Weapon, "", true);

        public Gear()
        {
            pieces.Add(head);
            pieces.Add(chest);
            pieces.Add(legs);
            pieces.Add(feet);
            pieces.Add(weapon);
        }
    }
}
