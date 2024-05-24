namespace Arcade.RPG;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcade.RPG.Entities;
using Arcade.RPG.Components;
using System.Diagnostics;
using Arcade.RPG.Worlds;

public class RPG : Game {
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public Random random;

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

        this.World = new World(this);
    }

    protected override void Initialize() {
        base.Initialize();
    }

    protected override void LoadContent() {
        this.spriteBatch = new SpriteBatch(GraphicsDevice);

        for(int i = 0; i < 25; i++) {
            var entity = new Entity();
            entity.AddComponent(EnumComponentType.Physics, new Physics(
                position: Vector2.One * this.random.Next(0, 1920),
                velocity: Vector2.One * 32
            ));
            entity.AddComponent(EnumComponentType.Graphics, new Graphics(
                graphicsDevice: GraphicsDevice,
                size: 50 + (int)Math.Floor((float)this.random.NextDouble() * 200),
                startColor: Color.Red,
                endColor: Color.Blue
            ));

            this.World.AddEntity(entity);
        }
    }



    protected override void Update(GameTime gameTime) {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        this.World.Update(this, gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        this.World.Draw(this, GraphicsDevice, gameTime, this.spriteBatch);

        base.Draw(gameTime);
    }
}
