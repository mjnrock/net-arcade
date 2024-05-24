namespace Arcade.RPG.Worlds;

using System;

using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Lib;
using Arcade.RPG.Lib.Models;

public class AtlasWorld : World {
    public static Atlas LoadAtlas(string mapName) {
        Atlas atlas = AtlasMapLoader.LoadFromFile($"Content\\Data\\maps\\{mapName}.json");

        return atlas;
    }

    public Atlas atlas;

    public AtlasWorld(string mapName, RPG game) : base(game) {
        this.atlas = AtlasWorld.LoadAtlas(mapName);

        Random random = new Random();
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
    }
}