using Nez;
using Nez.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonogamePlatformer.Components;
using Nez.UI;
using Microsoft.Xna.Framework.Input;

namespace MonogamePlatformer
{
    public class BasicScene : Scene
    {
        Camera camera;
        TextComponent x;
        TextComponent y;

        SpriteRenderer spriteRenderer;
        VelocityComponent velocityComponent;
        PlayerComponent playerComponent;
        Entity playerEntity;
        List<Entity> projectiles;

        Texture2D projectile_tex;
        public BasicScene(Camera camera)
        {
            AddRenderer(new ScreenSpaceRenderer(100, 999));
            this.camera = camera;
        }
        Entity mouse;
        Entity directions;
        public override void Initialize()
        {
            base.Initialize();
            projectiles = new List<Entity>();

            SetDesignResolution(640, 360, SceneResolutionPolicy.NoBorderPixelPerfect);
            Screen.SetSize(1280, 720);
            

            var moonTex = Content.LoadTexture(Nez.Content.Pawn);

            var back = Content.LoadTexture(Nez.Content.Twastheprospector);

            projectile_tex = Content.LoadTexture(Nez.Content.Ball);

            var pos = CreateEntity("pos", new Vector2(100, 100));
            playerEntity = CreateEntity("player", new Vector2(Screen.Width / 4, Screen.Height / 4));
            mouse = CreateEntity("mouse");
            directions = CreateEntity("direction");


            //var pos2 = CreateEntity("pos", new Vector2(100, 200));


            //pos2.AddComponent(new SpriteRenderer(back));

            playerEntity.AddComponent(new SpriteRenderer(moonTex));
            spriteRenderer = playerEntity.GetComponent<SpriteRenderer>();
            playerEntity.AddComponent(new VelocityComponent(new Vector2(-1, 0)));
            velocityComponent = playerEntity.GetComponent<VelocityComponent>();
            playerEntity.AddComponent(new BoxCollider());

            var noob = CreateEntity("noob", Vector2.One * 150);
            noob.AddComponent(new SpriteRenderer(Content.LoadTexture(Nez.Content.Twastheprospector)));
            noob.AddComponent(new BoxCollider());

            playerEntity.AddComponent(new PlayerComponent(200));
            playerComponent = playerEntity.GetComponent<PlayerComponent>();

            var mousetex = Content.LoadTexture(Nez.Content.Mouse);

            mouse.AddComponent(new SpriteRenderer(projectile_tex));

            directions.AddComponent(new SpriteRenderer(mousetex));
            
            playerEntity.AddComponent(new PixelSnapComponent());
            

            pos.AddComponent(new TextComponent());
            x = pos.GetComponent<TextComponent>();

        }
        Vector2 direction = Vector2.Zero;
        public override void Update()
        {
            base.Update();
            if(velocityComponent.Velocity.X < 0)
            {
                spriteRenderer.FlipX = false;
            }
            else if(velocityComponent.Velocity.X > 0)
            {
                spriteRenderer.FlipX = true;
            }
            direction = Input.MousePosition - playerEntity.Position;

            direction = Vector2Ext.Normalize(direction);
            mouse.SetPosition(Input.MousePosition);
            if(Input.LeftMouseButtonPressed)
            {
                var projectile = CreateEntity("projectile" + projectiles.Count + 1, playerEntity.Position + direction * 13);
                projectile.AddComponent(new SpriteRenderer(projectile_tex));
                projectile.AddComponent(new VelocityComponent());
                projectile.AddComponent(new PixelSnapComponent());

                projectile.AddComponent(new CircleCollider());

                VelocityComponent projectileVelocity = projectile.GetComponent<VelocityComponent>();
                
                projectileVelocity.Velocity = direction;
                projectiles.Add(projectile);
            }
            float angle = Mathf.Atan2(direction.X, direction.Y);
            directions.Position = playerEntity.Position + (new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * 50);
            
            x.SetText("X: " + direction.X + " Y: " + direction.Y);
        }
    }
}
