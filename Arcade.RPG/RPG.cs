namespace Arcade.RPG;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcade.RPG.Worlds;
using Arcade.RPG.Config;
using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using System.Collections.Generic;

public class RPG : Game {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public Random random;

    public Konfig Konfig { get; set; }
    public World World { get; set; }

    private MouseState previousMouseState = Mouse.GetState();

    public RPG() {
        this.graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Retrieve the screen resolution
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        // Set the preferred back buffer size to the screen resolution
        this.graphics.PreferredBackBufferWidth = screenWidth;
        this.graphics.PreferredBackBufferHeight = screenHeight;

        // Enable full screen mode
        this.graphics.IsFullScreen = true;
        this.graphics.ApplyChanges();

        this.random = new Random();

        this.World = new AtlasWorld("demoCaveMap", this);

        this.Konfig = new Konfig();
        this.Konfig.Viewport.Subject = new Entity {
            components = new Dictionary<EnumComponentType, IComponent> {
                { EnumComponentType.Physics, new Physics(
                    position: new Vector2(2, 2),
                    velocity: Vector2.Zero,
                    speed: 4.0f
                )},
                { EnumComponentType.Graphics, new Graphics(
                    graphicsDevice: this.GraphicsDevice,
                    color: Color.CornflowerBlue
                )}
            }
        };

        this.World.AddEntity(this.Konfig.Viewport.Subject);
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        this.spriteBatch = new SpriteBatch(GraphicsDevice);
    }



    protected override void Update(GameTime gameTime) {
        MouseState mouseState = Mouse.GetState();
        KeyboardState keyboardState = Keyboard.GetState();

        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape)) {
            Exit();
        }


        Physics physics = this.Konfig.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
        Vector2 newVelocity = Vector2.Zero;

        if(keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left)) {
            newVelocity.X = -1.0f;
        } else if(keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right)) {
            newVelocity.X = 1.0f;
        }
        if(keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up)) {
            newVelocity.Y = -1.0f;
        } else if(keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down)) {
            newVelocity.Y = 1.0f;
        }

        physics.Velocity = newVelocity;


        int scrollDelta = mouseState.ScrollWheelValue - this.previousMouseState.ScrollWheelValue;
        if(scrollDelta > 0) {
            this.Konfig.Viewport.Zoom.Current *= 1 + this.Konfig.Viewport.Zoom.Step;
        } else if(scrollDelta < 0) {
            this.Konfig.Viewport.Zoom.Current *= 1 - this.Konfig.Viewport.Zoom.Step;
        }
        this.Konfig.Viewport.Zoom.Current = Math.Min(this.Konfig.Viewport.Zoom.Max, Math.Max(this.Konfig.Viewport.Zoom.Min, this.Konfig.Viewport.Zoom.Current));



        this.World.Update(this, gameTime);

        base.Update(gameTime);

        this.previousMouseState = mouseState;
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);

        float zoom = this.Konfig.Viewport.Zoom.Current;

        Physics subjectPhysics = this.Konfig.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
        Vector2 subjectPosition = subjectPhysics.Position;

        float viewportWidth = GraphicsDevice.Viewport.Width;
        float viewportHeight = GraphicsDevice.Viewport.Height;

        Vector3 viewportCenter = new Vector3(viewportWidth / 2f, viewportHeight / 2f, 0);

        float pixelX = -subjectPosition.X * this.Konfig.Viewport.TileBaseWidth - this.Konfig.Viewport.TileBaseWidth / 2;
        float pixelY = -subjectPosition.Y * this.Konfig.Viewport.TileBaseHeight - this.Konfig.Viewport.TileBaseHeight / 2;

        Matrix translationMatrix = Matrix.CreateTranslation(
            new Vector3(
                pixelX,
                pixelY,
                0
            )
        );
        Matrix transformationMatrix = translationMatrix * Matrix.CreateScale(zoom, zoom, 1f) * Matrix.CreateTranslation(viewportCenter);

        this.spriteBatch.Begin(
            samplerState: SamplerState.PointClamp,
            transformMatrix: transformationMatrix
        );

        this.World.Draw(this, GraphicsDevice, gameTime, this.spriteBatch);

        this.spriteBatch.End();

        base.Draw(gameTime);
    }


}
