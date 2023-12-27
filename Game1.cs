using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;

namespace MonogamePlatformer
{
    public class Game1 : Core 
    {
        Camera camera;
        protected override void Initialize()
        {
            //Core.DebugRenderEnabled = true;
            Physics.SpatialHashCellSize = 16;
            base.Initialize();
            camera = new Camera();
            camera.SetEnabled(true);
            Window.AllowUserResizing = true;

            Scene = new BasicScene(camera);
        }
    }
}
