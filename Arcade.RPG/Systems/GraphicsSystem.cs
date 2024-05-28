namespace Arcade.RPG.Systems;

using Arcade.RPG.Entities;
using Arcade.RPG.Lib.Geometry.Collisions;
using Arcade.RPG.Worlds;

using global::System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GraphicsSystem : System {
    public GraphicsSystem(RPG game) : base(game) { }
    public enum EnumAction {
        JoinWorld
    }

    public override void Receive(Message message) {
        this.Game.Debug.Log($"Received message: {message.Type}");
    }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) {
        base.Draw(game, graphicsDevice, gameTime, spriteBatch);
    }
}