namespace Arcade.RPG;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Arcade.RPG.Entities;
using Arcade.RPG.Components;
using System.Diagnostics;

public class RPG : Game {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private List<Entity> entities;
    private Random random;

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

        this.entities = new List<Entity>();
        this.random = new Random();
    }

    protected override void Initialize() {
        base.Initialize();
    }

    private float GetRandomVelocityComponent() {
        return this.random.Next(2) == 0 ? -100 : 100;
    }
    protected override void LoadContent() {
        this.spriteBatch = new SpriteBatch(GraphicsDevice);

        for(int i = 0; i < 25; i++) {
            Vector2 randomPosition = new Vector2(
                this.random.Next(0, this.graphics.PreferredBackBufferWidth),
                this.random.Next(0, this.graphics.PreferredBackBufferHeight)
            );
            Vector2 randomVelocity = new Vector2(
                GetRandomVelocityComponent(),
                GetRandomVelocityComponent()
            );
            Physics physicsComponent = new Physics(randomPosition, randomVelocity);

            Graphics graphicsComponent = new Graphics(
                GraphicsDevice,
                50 + (int)Math.Floor((float)this.random.NextDouble() * 200),
                Color.Red,
                Color.Blue
            );

            Entity entity = new Entity(new Dictionary<EnumComponentType, IComponent> {
                { EnumComponentType.Graphics, graphicsComponent },
                { EnumComponentType.Physics, physicsComponent }
            });
            this.entities.Add(entity);
        }
    }



    protected override void Update(GameTime gameTime) {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        var keyboard = Keyboard.GetState();
        int nudge = 10;

        foreach(var entity in this.entities) {
            var physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);

            if(keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.A)) {
                physicsComponent.Position = new Vector2(physicsComponent.Position.X - nudge, physicsComponent.Position.Y);
            } else if(keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.D)) {
                physicsComponent.Position = new Vector2(physicsComponent.Position.X + nudge, physicsComponent.Position.Y);
            }
            if(keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.W)) {
                physicsComponent.Position = new Vector2(physicsComponent.Position.X, physicsComponent.Position.Y - nudge);
            } else if(keyboard.IsKeyDown(Keys.Down) || keyboard.IsKeyDown(Keys.S)) {
                physicsComponent.Position = new Vector2(physicsComponent.Position.X, physicsComponent.Position.Y + nudge);
            }

            entity.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.LightGray);

        this.spriteBatch.Begin();

        foreach(var entity in this.entities) {
            entity.Draw(this.spriteBatch);
        }

        this.spriteBatch.End();

        base.Draw(gameTime);
    }
}
