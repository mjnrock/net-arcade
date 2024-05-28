namespace Arcade.RPG.Worlds;

using System;
using System.Collections.Generic;

using Arcade.Lib;
using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Lib;
using Arcade.RPG.Lib.Models;

using Microsoft.Xna.Framework;

public class AtlasWorld : World {
    public Atlas atlas;
    public EntityQuadTree collisionQuadTree;
    public Rectangle worldBounds;

    public static Atlas LoadAtlas(string mapName) {
        Atlas atlas = AtlasMapLoader.LoadFromFile($"Content\\Data\\maps\\{mapName}.json");

        return atlas;
    }

    public AtlasWorld(string mapName, RPG game) : base(game) {
        this.atlas = AtlasWorld.LoadAtlas(mapName);

        this.atlas.ForEach((tile, terrain) => {
            var entity = new TerrainEntity(
                x: tile.X,
                y: tile.Y,
                terrain: terrain
            );

            entity.AddComponent(EnumComponentType.Graphics, new Graphics(
                graphicsDevice: game.GraphicsDevice,
                color: terrain.Texture
            ));

            this.AddEntity(entity);

            return true;
        });

        this.worldBounds = new Rectangle(0, 0, this.atlas.Map.Rows, this.atlas.Map.Columns);
        this.collisionQuadTree = new EntityQuadTree(0, worldBounds);
    }

    public void RefreshQuadTree() {
        collisionQuadTree.Clear();
        foreach(Entity entity in this.entityManager.cache) {
            collisionQuadTree.Insert(this.game, entity);
        }
    }

    public List<Entity> GetPotentialCollisions(Entity entity) {
        return collisionQuadTree.RetrievePotentialCollisions(entity);
    }
}