using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


using System.Diagnostics;


namespace Bouncing_Bravery
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        #region untouched xna-defined variables and functions
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = GameConstants.ScreenY;
            graphics.PreferredBackBufferWidth = GameConstants.ScreenX;
            Content.RootDirectory = "Content";
        }
        protected override void UnloadContent()
        {

        }


        #endregion

        #region user-defined variables
        Hero hero;
        Camera gameCamera;
        Sounds soundManager;
        Screen screen;
        DataManager data;
        Map map;
        EventHandler eventHandler;

        //Keyboard related
        GamePadState lastState = GamePad.GetState(PlayerIndex.One);
        KeyboardState lastKeyState = Keyboard.GetState();

        //Portal
        Model portalModel;
        Matrix[] portalTransforms;

        //Star
        Matrix[] starTransforms;
        Model starModel;

        //Tiles related
        VertexDeclaration vertexDeclaration;
        Texture2D quadTexture;
        BasicEffect quadEffect;

        //Void related
        Texture2D voidTexture;
        BasicEffect voidEffect;
        #endregion

        #region user-defined functions

        private void DrawTile(Tile quad)
        {
            quadEffect.View = gameCamera.ViewMatrix;
            quadEffect.Projection = gameCamera.ProjectionMatrix;

            foreach (EffectPass pass in quadEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    quad.Vertices, 0, 4,
                    quad.Indexes, 0, 2);
            }
        }

        private void DrawVoids(Tile v)
        {
            voidEffect.View = gameCamera.ViewMatrix;
            voidEffect.Projection = gameCamera.ProjectionMatrix;

            foreach (EffectPass pass in voidEffect.CurrentTechnique.Passes)
            {
                pass.Apply();

                GraphicsDevice.DrawUserIndexedPrimitives
                    <VertexPositionNormalTexture>(
                    PrimitiveType.TriangleList,
                    v.Vertices, 0, 4,
                    v.Indexes, 0, 2);
            }
        }

        private void setupQuad()
        {
            quadEffect = new BasicEffect(graphics.GraphicsDevice);
            quadEffect.EnableDefaultLighting();

            quadEffect.World = Matrix.Identity;
            quadEffect.TextureEnabled = true;
            quadEffect.Texture = quadTexture;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
        }

         private void setupVoid()
        {
            voidEffect = new BasicEffect(graphics.GraphicsDevice);
            voidEffect.EnableDefaultLighting();

            voidEffect.World = Matrix.Identity;
            voidEffect.TextureEnabled = true;
            voidEffect.Texture = voidTexture;

            vertexDeclaration = new VertexDeclaration(new VertexElement[]
                {
                    new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                    new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                    new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0)
                }
            );
        }

        private void CheckColisions()
        {
            if (hero.isActive)
            {
                BoundingSphere heroSphere = new BoundingSphere(
                    hero.Position, hero.Model.Meshes[0].BoundingSphere.Radius *
                                         GameConstants.scaleHero);
                //hero-star collision check
                for (int i = 0; i < data.starList.Length; i++)
                {
                    if (data.starList[i].isActive)
                    {
                        BoundingSphere starSphere =
                          new BoundingSphere(data.starList[i].position, starModel.Meshes[0].BoundingSphere.Radius * GameConstants.scaleStar);
                        if (starSphere.Intersects(heroSphere))
                        {
                            Sounds.Play("coin");
                            data.starList[i].isActive = false;
                            data.starsCaptured++;
                            break; 
                        }


                    }
                }
                //hero-danger collision check
                foreach (Danger d in data.dangerList)
                {
                    
                    if (d.dangerBox.Intersects(heroSphere))
                    {
                        eventHandler.AddEvent("restart");
                        break; //no need to check other spikes
                    }
                }

                //hero-portal check
                BoundingSphere endSphere =
                          new BoundingSphere(data.positionPortal2,
                                   portalModel.Meshes[0].BoundingSphere.Radius);
                if (endSphere.Intersects(heroSphere))
                {
                    data.isPaused = true;
                    data.gameWon = true;
                }
            }
        }

        private void UpdateStars(float elapsed)
        {
            for (int i = 0; i < data.NumStars; i++)
            {
                data.starList[i].RotationMatrix *= Matrix.CreateRotationY((MathHelper.PiOver2) * elapsed);
            }
        }

        private Matrix[] SetupEffectDefaults(Model myModel)
        {
            Matrix[] absoluteTransforms = new Matrix[myModel.Bones.Count];
            myModel.CopyAbsoluteBoneTransformsTo(absoluteTransforms);

            foreach (ModelMesh mesh in myModel.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = gameCamera.ProjectionMatrix;
                    effect.View = gameCamera.ViewMatrix;
                }
            }
            return absoluteTransforms;
        }

        private void DrawModel(Model model, Matrix modelTransform, Matrix[] absoluteBoneTransforms)
        {
            //Draw the model, a model can have multiple meshes, so loop
            foreach (ModelMesh mesh in model.Meshes)
            {
                //This is where the mesh orientation is set
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = absoluteBoneTransforms[mesh.ParentBone.Index] * modelTransform;

                    // Use the matrices provided by the game camera
                    effect.View = gameCamera.ViewMatrix;
                    effect.Projection = gameCamera.ProjectionMatrix;
                }
                //Draw the mesh, will use the effects set above.
                mesh.Draw();
            }
        }

        protected void UpdateInput(float elapsed)
        {
            // Get the game pad state.
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);
            KeyboardState currentKeyState = Keyboard.GetState();

            if (data.isPaused)
            {
                if (data.isMenu)
                {
                    if ((currentState.Buttons.A == ButtonState.Pressed && lastState.Buttons.A == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.Enter) && lastKeyState.IsKeyUp(Keys.Enter)))
                    {
                        if (screen.cursor_pos.Y == 275)
                        {
                            data.isMenu = false;
                            data.isPaused = false;
                            Sounds.Play("theme");
                        }
                        else if (screen.cursor_pos.Y == 275 + 80)
                        {
                            data.currentMenu = 2;
                        }
                        else if (screen.cursor_pos.Y == 275 + 80 * 2)
                        {
                            data.currentMenu = 3;
                        }
                        else if (screen.cursor_pos.Y == 275 + 80 * 3)
                        {
                            this.Exit();
                        }
                    }
                    else if ((currentState.DPad.Up == ButtonState.Pressed && lastState.DPad.Up == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.Up) && lastKeyState.IsKeyUp(Keys.Up)))
                    {
                        screen.cursor_pos.Y -= 80;
                        if (screen.cursor_pos.Y < 275)
                            screen.cursor_pos.Y = 518;
                    }
                    else if ((currentState.DPad.Down == ButtonState.Pressed && lastState.DPad.Down == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.Down) && lastKeyState.IsKeyUp(Keys.Down)))
                    {
                        screen.cursor_pos.Y += 80;
                        if (screen.cursor_pos.Y > 518)
                            screen.cursor_pos.Y = 275;
                    }
                    else if ((currentState.Buttons.Back == ButtonState.Pressed && lastState.Buttons.Back == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.Escape) && lastKeyState.IsKeyUp(Keys.Escape)))
                    {
                        if (data.currentMenu == 1)
                        {
                            this.Exit();
                        }
                        else
                        {
                            data.currentMenu = 1;
                        }
                    }
                }
                else if (currentKeyState.GetPressedKeys().Length > 0 && lastKeyState.GetPressedKeys().Length == 0)
                {
                    data.isPaused = false;
                    data.HeroDead = false;
                    MediaPlayer.Resume();
                    Sounds.Play("theme");
                }   
            }
            else
            {
                hero.UpdateInput(currentState, currentKeyState, lastState, lastKeyState, elapsed, data);


                //Toggle sound on and off
                if ((currentState.Buttons.Back == ButtonState.Pressed && lastState.Buttons.Back == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.LeftControl) && lastKeyState.IsKeyUp(Keys.LeftControl)))
                {
                    Sounds.ToggleSound();
                }

                //Toggle camera view
                if ((currentState.Buttons.Y == ButtonState.Pressed && lastState.Buttons.Y == ButtonState.Released) || (currentKeyState.IsKeyDown(Keys.Tab) && lastKeyState.IsKeyUp(Keys.Tab)))
                {
                    data.cameraThirdPerson = gameCamera.ToggleCamera();
                }
            }
            lastState = currentState;
            lastKeyState = currentKeyState;
        }
        #endregion


        protected override void Initialize()
        {
            #region class instantiations
            spriteBatch = new SpriteBatch(GraphicsDevice);

            data = new DataManager();
            gameCamera = new Camera(GraphicsDevice.Viewport.AspectRatio);
            data.danger1 = Content.Load<Model>("Models/spike");
            data.danger2 = Content.Load<Model>("Models/danger1");
            data.danger1Transforms = SetupEffectDefaults(data.danger1);
            data.danger2Transforms = SetupEffectDefaults(data.danger2);
            map = new Map(data);
            hero = new Hero(map);
            hero.Position.Y = GameConstants.floorHeight;
            soundManager = new Sounds(Content);
            
            screen = new Screen(spriteBatch, Content);

            eventHandler = new EventHandler(map, data, hero, screen, gameCamera);
            hero.SetupEventHandler(eventHandler);
            #endregion

            Sounds.Play("menu");
            MediaPlayer.IsRepeating = true;


            

            base.Initialize();
        }

        protected override void LoadContent()
        {
            hero.Model = Content.Load<Model>("Models/Ball");
            hero.Transforms = SetupEffectDefaults(hero.Model);
            hero.shadowModel = Content.Load<Model>("Models/shadow");
            hero.shadowTransforms = SetupEffectDefaults(hero.shadowModel);
            starModel = Content.Load<Model>("Models/starscaled");
            portalModel = Content.Load<Model>("Models/redtorus");

            voidTexture = Content.Load<Texture2D>("Textures/water");
            quadTexture = Content.Load<Texture2D>("Textures/Grass");


            starTransforms = SetupEffectDefaults(starModel);
            portalTransforms = SetupEffectDefaults(portalModel);
            

            setupQuad();
            setupVoid();
        }

        protected override void Update(GameTime gameTime)
        {
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;//0.01666667, for 60FPS

            
                // Get some input.
                UpdateInput(elapsed);

                if (data.isPaused == false)
                {

                hero.Update(elapsed);
                UpdateStars(elapsed);
                eventHandler.Update(elapsed);
                CheckColisions();


                gameCamera.Update(hero.Position, hero.avatarYaw);
                data.timeElapsed += elapsed;
                if (data.isSuper == true)
                {
                    data.starsCaptured -= elapsed;
                    if (data.starsCaptured <= 0)
                    {
                        data.isSuper = false;
                        Sounds.stopSonic();
                    }
                }
               }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            #region reset graphics
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            GraphicsDevice.DepthStencilState = DepthStencilState.None;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = SamplerState.AnisotropicWrap;
            #endregion


            #region begin 3d render
            #region starting the device
            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            #endregion

            

            foreach (Tile t in map.tiles)
            {
                DrawTile(t);
            }
            foreach (Tile v in map.voids)
            {
                DrawVoids(v);
            }

            if (hero.isActive)
            {
                Matrix heroTransformMatrix = Matrix.CreateScale(GameConstants.scaleHero) * hero.RotationMatrix * Matrix.CreateTranslation(hero.Position);
                DrawModel(hero.Model, heroTransformMatrix, hero.Transforms);
                float shadowScale = 1.0f - ((hero.Position.Y - GameConstants.floorHeight) * 0.015f);
                Matrix heroShadowTransformMatrix = Matrix.CreateScale(shadowScale) * hero.shadowRotationMatrix * Matrix.CreateTranslation(new Vector3(hero.Position.X, GameConstants.floorHeight - 2, hero.Position.Z));
                DrawModel(hero.shadowModel, heroShadowTransformMatrix, hero.shadowTransforms);
            }

            // Draw stars
            for (int i = 0; i < data.NumStars; i++)
            {
                if (data.starList[i].isActive)
                {
                    //model is already scaled, so no scaling done
                    Matrix starTransform = data.starList[i].RotationMatrix * Matrix.CreateTranslation(data.starList[i].position);
                    DrawModel(starModel, starTransform, starTransforms);
                }
            }

            // Draw portals
                    //model is already scaled, so no scaling done
                    Matrix portalTransform = Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateTranslation(data.positionPortal1);
                    DrawModel(portalModel, portalTransform, portalTransforms);
                    portalTransform = Matrix.CreateRotationX(MathHelper.PiOver2) * Matrix.CreateTranslation(data.positionPortal2);
                    DrawModel(portalModel, portalTransform, portalTransforms);

            // Draw danger
            foreach(Danger d in data.dangerList)
            {
                    //model is already scaled, so no scaling done
                    Matrix dangerTransform = Matrix.CreateScale(GameConstants.scaleDanger) * d.RotationMatrix * Matrix.CreateTranslation(d.position);
                    DrawModel(d.model, dangerTransform, d.Transforms);
            }


            #endregion
            #region start2d drawing
            screen.DrawText(data);
            #endregion

            base.Draw(gameTime);
        }
    }
}
