namespace Arcade.RPG;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcade.RPG.Worlds;
using Arcade.RPG.Config;

public class RPG : Game {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public Random random;

    public Konfig Konfig { get; set; }
    public World World { get; set; }

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
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        this.spriteBatch = new SpriteBatch(GraphicsDevice);
    }



    protected override void Update(GameTime gameTime) {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        this.World.Update(this, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.LightGray);

        float zoom = this.Konfig.Viewport.Zoom;
        Matrix scalingMatrix = Matrix.CreateScale(zoom, zoom, 1f);

        this.spriteBatch.Begin(transformMatrix: scalingMatrix);

        this.World.Draw(this, GraphicsDevice, gameTime, this.spriteBatch);

        this.spriteBatch.End();

        base.Draw(gameTime);
    }
}
