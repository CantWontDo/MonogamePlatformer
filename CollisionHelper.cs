using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePlatformer
{
    public static class CollisionHelper
    {
        enum CollisionLayers
        {
            None,
            Player,
            Enemy,
            Tile,
            PlayerProjectile,
            EnemyProjectile
        }
    }
}
