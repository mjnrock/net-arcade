﻿namespace Arcade.RPG.Entities;

using Arcade.RPG.Components;
using Arcade.RPG.Lib.Models;

using Microsoft.Xna.Framework;

public class TerrainEntity : Entity {
    public Terrain terrain;

    public TerrainEntity(int x, int y, Terrain terrain) : base() {
        this.AddComponent(EnumComponentType.Physics, new Physics(
            //model: new Lib.Geometry.Shapes.Shape(
            //    origin: new Vector2(x, y)
            //),
            model: new Lib.Geometry.Shapes.Rectangle(
                origin: new Vector2(x, y),
                width: 1.0f,
                height: 1.0f
            ),
            velocity: Vector2.Zero
        ));

        this.terrain = terrain;
    }
}