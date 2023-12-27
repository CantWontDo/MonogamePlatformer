using Nez;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace MonogamePlatformer.Components
{
    public class PlayerComponent : Component, IUpdatable
    {
        float MoveSpeed = 0;

        float Acceleration = 8f;

        float Decceleration = 16f;

        float MaxSpeed = 300;

        VirtualAxis _xInput;

        VirtualAxis _yInput;

        VelocityComponent _velocityComponent;

        BoxCollider _playerCollider;

        public PlayerComponent(float maxSpeed)
        {
            MaxSpeed = maxSpeed;
        }

        private void SetUpInput()
        {
            

            _xInput = new VirtualAxis();
            _xInput.Nodes.Add(new VirtualAxis.GamePadLeftStickX());
            _xInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer
                , Keys.A, Keys.D));
            _xInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer,
                Keys.Left, Keys.Right));


            _yInput = new VirtualAxis();
            _yInput.Nodes.Add(new VirtualAxis.GamePadLeftStickY());
            _yInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer
                , Keys.W, Keys.S));
            _yInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer,
                Keys.Up, Keys.Down));
        }

        public override void OnAddedToEntity()
        {
            base.OnAddedToEntity();
            _velocityComponent = Entity.GetComponent<VelocityComponent>();
            _playerCollider = Entity.GetComponent<BoxCollider>();
            SetUpInput();
        }

        public override void OnRemovedFromEntity()
        {
            _xInput.Deregister();
        }
        
        public void Update()
        {
            Vector2 deltaMovement = Vector2.Zero;
            CollisionResult collisionResult;

            if(_playerCollider.CollidesWithAny(ref deltaMovement, out collisionResult))
            {
                Nez.Debug.Log("collison result: {0}", collisionResult);
            }

            Entity.Position += deltaMovement;

            Vector2 velocity = Vector2.Zero;

            if (_xInput.Value == 0 && _yInput == 0)
            {
                MoveSpeed = Mathf.Lerp(MoveSpeed, 0, Decceleration * Time.DeltaTime);
            }
            else
            {
                MoveSpeed = Mathf.Lerp(MoveSpeed, MaxSpeed, Acceleration * Time.DeltaTime);
            }
            MoveSpeed = Mathf.RoundToInt(MoveSpeed);


            velocity.X += (_xInput.Value * MoveSpeed * Time.DeltaTime);
            velocity.Y += (_yInput.Value * MoveSpeed * Time.DeltaTime);

            _velocityComponent.Velocity = velocity;
        }

    }
}
