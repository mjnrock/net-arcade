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

    public List<Entity> entities {
        get {
            return this.entityManager.entities;
        }
    }
    public List<Entity> cache {
        get {
            return this.entityManager.cache;
        }
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

    public void Update(RPG game, GameTime gameTime) {
        this.entityManager.ClearCache();
        this.entities.ForEach(entity => this.entityManager.WriteCache(entity));

        foreach(Entity entity in this.entityManager.cache) {
            entity.Update(game, gameTime);
        }
    }
    public void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) {
        graphicsDevice.Clear(Color.LightGray);

        spriteBatch.Begin();

        foreach(Entity entity in this.entityManager.cache) {
            entity.Draw(game, graphicsDevice, gameTime, spriteBatch);
        }

        spriteBatch.End();
    }
}