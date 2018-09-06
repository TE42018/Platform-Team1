using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeSouls
{
     abstract class GameObject : IMovingObjects
    {
       public bool IsDead { get; set; }
       public Vector2 Position { get; set; }
       public Vector2 Velocity { get; set; }
    }
}
