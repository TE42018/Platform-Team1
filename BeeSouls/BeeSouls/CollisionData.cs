using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BeeSouls
{
    class CollisionData
    {
        public int Tile { get; set; }
        public Rectangle Area { get; set; }

        public CollisionData()
        {
            Area = Rectangle.Empty;
            
        }
    }
}
