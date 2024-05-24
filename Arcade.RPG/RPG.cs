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
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private List<Entity> _entities;
    private Random _random;

    public RPG() {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        // Retrieve the screen resolution
        int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

        // Set the preferred back buffer size to the screen resolution
        _graphics.PreferredBackBufferWidth = screenWidth;
        _graphics.PreferredBackBufferHeight = screenHeight;

        // Enable full screen mode
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();

        _entities = new List<Entity>();
        _random = new Random();
    }

    protected override void Initialize() {
        base.Initialize();
    }

    private float GetRandomVelocityComponent() {
        return _random.Next(2) == 0 ? -100 : 100;
    }
    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        for(int i = 0; i < 25; i++) {
            Vector2 randomPosition = new Vector2(
                _random.Next(0, _graphics.PreferredBackBufferWidth),
                _random.Next(0, _graphics.PreferredBackBufferHeight)
            );
            Vector2 randomVelocity = new Vector2(
                GetRandomVelocityComponent(),
                GetRandomVelocityComponent()
            );
            Physics physicsComponent = new Physics(randomPosition, randomVelocity);

            Graphics graphicsComponent = new Graphics(
                GraphicsDevice,
                50 + (int)Math.Floor((float)_random.NextDouble() * 200),
                Color.Red,
                Color.Blue
            );

            Entity entity = new Entity(new Dictionary<EnumComponentType, IComponent> {
                { EnumComponentType.Graphics, graphicsComponent },
                { EnumComponentType.Physics, physicsComponent }
            });
            _entities.Add(entity);
        }
    }



    protected override void Update(GameTime gameTime) {
        if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
            Exit();
        }

        var keyboard = Keyboard.GetState();
        int nudge = 10;

        foreach(var entity in _entities) {
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

        _spriteBatch.Begin();

        foreach(var entity in _entities) {
            entity.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
