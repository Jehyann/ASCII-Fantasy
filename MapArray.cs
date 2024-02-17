using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIFantasy
{
    public class MapArray
    {
        public Map activeMap { get; set; }
        public Map[][] maps { get; set; }

        public static MapArray instance { get; set; }

        public static MapArray CreateInstance()
        {
            if (instance == null)
            {
                instance = new MapArray();
            }
            return instance;
        }
        public MapArray()
        {
            maps = new Map[198][];
            for (int i = 0; i < 198; i++)
            {
                maps[i] = new Map[198];
            }
        }
    }
}
