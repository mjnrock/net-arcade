using Arcade.RPG.Components;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;

namespace Arcade.RPG.Systems;

public class InputSystem : System {

    private MouseState _previousMouseState = Mouse.GetState();

    public InputSystem(RPG game) : base(game) { }
    public enum EnumAction { }

    public override void Receive(Message message) {}


    public override void Update(RPG game, GameTime gameTime) {
        KeyboardState keyboardState = Keyboard.GetState();

        //TODO: Break this "Meta Input" out into its own something
        /* Allow for some control over the game */
        if(keyboardState.IsKeyDown(Keys.F1)) {
            game.Config.Settings.IsPaused = !game.Config.Settings.IsPaused;
        }
        if(keyboardState.IsKeyDown(Keys.Escape)) {
            game.Exit();
        }

        if(game.Config.Settings.IsPaused) {
            return;
        }

        /* Only continue if the game is active */
        Physics playerPhysics = game.Config.Viewport.Subject.GetComponent<Physics>(EnumComponentType.Physics);
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

        playerPhysics.velocity = newVelocity;
    }
    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) {
        MouseState mouseState = Mouse.GetState();

        int scrollDelta = mouseState.ScrollWheelValue - this._previousMouseState.ScrollWheelValue;
        if(scrollDelta > 0) {
            game.Config.Viewport.Zoom.Current *= 1 + game.Config.Viewport.Zoom.Step;
        } else if(scrollDelta < 0) {
            game.Config.Viewport.Zoom.Current *= 1 - game.Config.Viewport.Zoom.Step;
        }

        game.Config.Viewport.Zoom.Current = Math.Min(game.Config.Viewport.Zoom.Max, Math.Max(game.Config.Viewport.Zoom.Min, game.Config.Viewport.Zoom.Current));
        game.Config.SyncWithSubject();


        /* Prep for next iteration */
        this._previousMouseState = mouseState;
    }
}