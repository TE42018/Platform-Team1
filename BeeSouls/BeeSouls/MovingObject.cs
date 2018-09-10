using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSouls
{
    class MovingObject
    {
        bool IsDead { get; set; }
        Vector2 Position { get; set; }
        Vector2 Velocity { get; set; }
    } 
}
