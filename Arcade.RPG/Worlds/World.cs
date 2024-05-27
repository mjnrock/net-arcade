namespace Arcade.RPG.Worlds;

using System.Collections.Generic;
using System.Diagnostics;

using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Lib;
using Arcade.RPG.Lib.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class World : Identity {
    public RPG Game;
    public EntityManager EntityManager;

    public World(RPG game) : base() {
        this.Game = game;
        this.EntityManager = new EntityManager();
    }

    public List<Entity> entities {
        get {
            return this.EntityManager.entities;
        }
    }
    public List<Entity> cache {
        get {
            return this.EntityManager.cache;
        }
    }

    public World AddEntity(Entity entity) {
        this.EntityManager.Add(entity);

        return this;
    }
    public World AddEntities(List<Entity> entities) {
        this.EntityManager.Add(entities);

        return this;
    }
    public World RemoveEntity(Entity entity) {
        this.EntityManager.Remove(entity);

        return this;
    }
    public World RemoveEntities(List<Entity> entities) {
        this.EntityManager.Remove(entities);

        return this;
    }
}