using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogamePlatformer.Components
{
    public class PixelSnapComponent : Component
    {
        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            SetUpdateOrder(10);
        }
        public override void OnEntityTransformChanged(Transform.Component comp)
        {
            
            base.OnEntityTransformChanged(comp);
        
            Vector2 intPos;
            intPos.X = Mathf.RoundToInt(Entity.Position.X);
            intPos.Y = Mathf.RoundToInt(Entity.Position.Y);
            Entity.Position = intPos;
        }

         
    }
}
