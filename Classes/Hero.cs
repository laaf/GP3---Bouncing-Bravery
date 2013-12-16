using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


using System.Diagnostics;


namespace Bouncing_Bravery
{
    class Hero
    {
        public Model Model;
        public Model shadowModel;
        public Matrix[] Transforms;
        public Matrix[] shadowTransforms;
        //Position of the model in world space
        public Vector3 Position;
        //Velocity of the model, applied each frame to the model's position
        public Vector3 Velocity = Vector3.Zero;
        public float upSpeed;
        //Amplifies controller speed input
        private const float VelocityScale = 5.0f;
        public float avatarYaw;
        //For verifying if the unit is alive
        public bool isActive;
        private Map map;
        private EventHandler eventHandler;
        public bool bouncing;

        //For handling rotations
        public Matrix RotationMatrix = Matrix.CreateRotationX(MathHelper.PiOver2);
        public Matrix shadowRotationMatrix = Matrix.CreateRotationY(MathHelper.PiOver2);
        public float ForwardDirection;

        //For handling collisions
        public BoundingSphere BoundingSphere;


        private void updateNewYaw(float newVal)
        {
            avatarYaw += newVal;
            
            while (avatarYaw>= MathHelper.TwoPi)
            {
                avatarYaw -= MathHelper.TwoPi;
            }
            while (avatarYaw < 0)
            {
                avatarYaw += MathHelper.TwoPi;
            }
        }

        public void addRotation(float newVal, int axis)
        {
            while (newVal >= MathHelper.TwoPi)
            {
                newVal -= MathHelper.TwoPi;
            }
            while (newVal < 0)
            {
                newVal += MathHelper.TwoPi;
            }
            
            if (newVal != 0)
            {

                if (axis == 1) //eixo horizontal
                {
                    RotationMatrix *= Matrix.CreateRotationY(newVal);
                    updateNewYaw(newVal);
                }
                else
                {
                    RotationMatrix *= Matrix.CreateRotationX(newVal);
                }
            }
        }

        public Hero(Map map)
        {
            Model = null;
            ForwardDirection = 0.0f;
            isActive = true;
            upSpeed = 0.0f;
            avatarYaw = 0;
            BoundingSphere = new BoundingSphere();
            bouncing = false;
            this.map = map;
            Position = map.defaultPlayerPos;
        }

        public void SetupEventHandler(EventHandler eh)
        {
            this.eventHandler = eh;
        }

        public void Update(float elapsed)
        {
            float RemainingTime;
            if (Position.Y > GameConstants.floorHeight) // Apply gravity if we're not already on the ground
            {
                Velocity.Y += GameConstants.gravity * elapsed * GameConstants.timeScale;
                if (GameConstants.floorHeight + GameConstants.speedThreshold_AllowJump < Position.Y) //if we are close to floor, keep jumping allowed
                    bouncing = true;
            }
            Velocity.Y -= Velocity.Y * GameConstants.airResistance * elapsed;
            Position.Y += Velocity.Y * elapsed * GameConstants.timeScale;

            if (Position.Y < GameConstants.floorHeight) //We've hit the floor
            {
                Sounds.Play("bounce");
                RemainingTime = -Position.Y / Velocity.Y;
                Position.Y = GameConstants.floorHeight;

                // Remove excess gravity
                Velocity.Y -= RemainingTime * GameConstants.gravity;


                // Invert velocity
                Velocity.Y = -Velocity.Y;
                // Apply bleeding out after collision
                Velocity.Y *= GameConstants.coefficientRestitution;

                Velocity.Y += elapsed * GameConstants.gravity;
                // If velocity changed sign again, clamp it to zero (we need to be moving up)
                if (Velocity.Y <= 0)
                    Velocity.Y = 0;

                
                
                bouncing = false;
            }
        }

        public void UpdateInput(GamePadState controllerState, KeyboardState currentKeyState, GamePadState lastState, KeyboardState lastKeyState, float elapsed, DataManager data)
        {

            float jSpeed = GameConstants.jumpSpeed;
            float moveRate = GameConstants.movementRate;

            if (data.isSuper == true)
            {
                jSpeed += 4.0f;
                moveRate += 10.0f;
            }

            if (currentKeyState.IsKeyDown(Keys.P)) //go super(sonic)
            {
                if (data.isSuper == false)
                {
                    if (data.starsCaptured > 0)
                    {
                        data.isSuper = true;
                        Sounds.goSonic();
                    }
                }
            }

            if (currentKeyState.IsKeyDown(Keys.A))
            {
                addRotation(0.08f, 1);
                if (data.cameraThirdPerson == 0)
                    Position.X -= moveRate * elapsed;
            }
            else if (currentKeyState.IsKeyDown(Keys.D))
            {
                addRotation(-0.08f, 1);
                if(data.cameraThirdPerson == 0)
                    Position.X += moveRate * elapsed;
            }
            else
                // Rotate the model using the left thumbstick, and scale it down
                addRotation(controllerState.ThumbSticks.Left.X * 0.10f, 1);

            if (currentKeyState.IsKeyDown(Keys.W))
            {
                addRotation(-0.08f, 0);
                if (data.cameraThirdPerson == 0)
                    Position.Z -= moveRate * elapsed;
                else
                {
                    Position.X += (float)Math.Sin(avatarYaw) * moveRate * elapsed;
                    Position.Z += (float)Math.Cos(avatarYaw) * moveRate * elapsed;
                }
            }
            else if (currentKeyState.IsKeyDown(Keys.S))
            {
                addRotation(0.08f, 0);
                if (data.cameraThirdPerson == 0)
                    Position.Z += moveRate * elapsed;
                else
                {
                    Position.X -= (float)Math.Sin(avatarYaw) * moveRate * elapsed;
                    Position.Z -= (float)Math.Cos(avatarYaw) * moveRate * elapsed;
                }
            }
            else
                // Rotate the model using the left thumbstick, and scale it down
                addRotation(controllerState.ThumbSticks.Left.Y * 0.10f, 0);

            if (currentKeyState.IsKeyDown(Keys.J) && lastKeyState.IsKeyUp(Keys.J) && bouncing == false)
            {
                Sounds.Play("jump");
                Velocity.Y = jSpeed * GameConstants.timeScale;
                bouncing = true;
            }

            if (map.ExistsTile(Position) == false && Position.Y == GameConstants.floorHeight) //hero has fallen
            {
                eventHandler.AddEvent("restart");
            }
        }
    }
}
