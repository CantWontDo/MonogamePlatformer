using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonogamePlatformer.Components
{
    public class SpinnerComponent : Component, IUpdatable
    {
        float timer;
        float distance;

        public SpinnerComponent(float distance)
        {
            this.distance = distance;
        }

        public void Update()
        {
            timer += Time.DeltaTime;

            float x = Mathf.Cos(timer);
            float y = Mathf.Sin(timer);

            Entity.Position += new Vector2(x, y) * 2;
        }
    }
}
