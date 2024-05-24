namespace Arcade.RPG.Entities;

using Arcade.RPG.Components;
using Arcade.RPG.Lib.Models;

using Microsoft.Xna.Framework;

public class TerrainEntity : Entity {
    public Terrain terrain;

    public TerrainEntity(int x, int y, Terrain terrain) : base() {
        this.AddComponent(EnumComponentType.Physics, new Physics(
            position: new Vector2(x, y),
            velocity: Vector2.Zero
        ));

        this.terrain = terrain;
    }
}