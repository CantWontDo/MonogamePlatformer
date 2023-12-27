using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogamePlatformer.Components
{
    public class VelocityComponent : Component, IUpdatable
    {
        public Vector2 Velocity { get; set; }

        public VelocityComponent(Vector2 velocity)
        {
            Velocity = velocity;
        }

        public VelocityComponent()
        {
            Velocity = Vector2.Zero;
        }

        public void Update()
        {
            Entity.Position += Velocity;
        }
    }
}
