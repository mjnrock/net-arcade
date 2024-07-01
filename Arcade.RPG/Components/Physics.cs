namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;
using Arcade.RPG;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arcade.RPG.Lib.Geometry.Shapes;

public class Physics : Component {
    public Shape model { get; set; }
    public Vector2 velocity { get; set; }
    /* The speed/sec of the entity to be applied to the velocity each game loop */
    public float speed { get; set; }

    public Physics(Shape model, Vector2 velocity, float speed = 10.0f, string? id = null) : base(id, EnumComponentType.Physics) {
        this.model = model;
        this.velocity = velocity;
        this.speed = speed;
    }
    public Vector2 Position {
        get => model.Origin;
        set => model.Origin = value;
    }
    public float X {
        get => model.Origin.X;
    }
    public float Y {
        get => model.Origin.Y;
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Position += this.velocity * this.speed * elapsedTime;
    }
}