namespace Arcade.RPG.Worlds;

using System.Collections.Generic;

using Arcade.RPG.Entities;
using Arcade.RPG.Lib;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class World : Identity {
    public RPG game;
    public EntityManager entityManager;

    public World(RPG game) : base() {
        this.game = game;
        this.entityManager = new EntityManager();
    }

    public World AddEntity(Entity entity) {
        this.entityManager.Add(entity);

        return this;
    }
    public World AddEntities(List<Entity> entities) {
        this.entityManager.Add(entities);

        return this;
    }
    public World RemoveEntity(Entity entity) {
        this.entityManager.Remove(entity);

        return this;
    }
    public World RemoveEntities(List<Entity> entities) {
        this.entityManager.Remove(entities);

        return this;
    }

    public void Update(GameTime gameTime) {
        foreach(Entity entity in this.entityManager.cache) {
            entity.Update(gameTime);
        }
    }
    public void Draw(SpriteBatch spriteBatch) {
        foreach(Entity entity in this.entityManager.cache) {
            entity.Draw(spriteBatch);
        }
    }
}