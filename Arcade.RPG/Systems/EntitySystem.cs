using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Diagnostics;

namespace Arcade.RPG.Systems;

public class EntitySystem : System {
    public EntitySystem(RPG game) : base(game) { }
    public enum EnumAction { }

    public override void Receive(Message message) {
        Debug.WriteLine($"EntitySystem received message: {message.Type}");
    }


    public override void Update(RPG game, GameTime gameTime) {
        EntityManager entMgr = game.World.entityManager;

        foreach(Entity entity in entMgr.cache) {
            entity.Update(game, gameTime);
        }
    }
    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) {
        EntityManager entMgr = game.World.entityManager;

        foreach(Entity entity in entMgr.cache) {
        //foreach(Entity entity in entMgr) {
            entity.Draw(game, graphicsDevice, gameTime, spriteBatch);
        }
    }
}