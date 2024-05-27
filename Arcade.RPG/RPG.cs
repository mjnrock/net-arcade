namespace Arcade.RPG;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcade.RPG.Worlds;
using Arcade.RPG.Configs;
using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using System.Collections.Generic;
using Arcade.RPG.Systems;

public class RPG : Game {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public Random random;

    public Config Config { get; set; }

    public Dictionary<EnumSystemType, ISystem> Systems { get; set; } = new Dictionary<EnumSystemType, ISystem> {
        { EnumSystemType.World, new WorldSystem() },
    };

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

        this.Config = new Configs.Config();
        this.Config.Viewport.Subject = new Entity {
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

        this.World.AddEntity(this.Config.Viewport.Subject);
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


        Physics physics = this.Config.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
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
            this.Config.Viewport.Zoom.Current *= 1 + this.Config.Viewport.Zoom.Step;
        } else if(scrollDelta < 0) {
            this.Config.Viewport.Zoom.Current *= 1 - this.Config.Viewport.Zoom.Step;
        }

        this.Config.Viewport.Zoom.Current = Math.Min(this.Config.Viewport.Zoom.Max, Math.Max(this.Config.Viewport.Zoom.Min, this.Config.Viewport.Zoom.Current));
        this.Config.SyncWithSubject();

        foreach(KeyValuePair<EnumSystemType, ISystem> system in this.Systems) {
            system.Value.Update(this, gameTime);
        }

        base.Update(gameTime);

        this.previousMouseState = mouseState;
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.Black);

        float zoom = this.Config.Viewport.Zoom.Current;

        Physics subjectPhysics = this.Config.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
        Vector2 subjectPosition = subjectPhysics.Position;

        float viewportWidth = GraphicsDevice.Viewport.Width;
        float viewportHeight = GraphicsDevice.Viewport.Height;

        Vector3 viewportCenter = new Vector3(viewportWidth / 2f, viewportHeight / 2f, 0);

        float pixelX = -subjectPosition.X * this.Config.Viewport.TileBaseWidth - this.Config.Viewport.TileBaseWidth / 2;
        float pixelY = -subjectPosition.Y * this.Config.Viewport.TileBaseHeight - this.Config.Viewport.TileBaseHeight / 2;

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
