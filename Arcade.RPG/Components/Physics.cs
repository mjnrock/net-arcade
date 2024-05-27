namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;
using Arcade.RPG;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Arcade.RPG.Lib.Geometry.Shapes;

public class Physics : Component {
    public Shape Model { get; set; }
    public Vector2 Velocity { get; set; }
    /* The speed/sec of the entity to be applied to the velocity each game loop */
    public float Speed { get; set; }

    public Physics(Shape model, Vector2 velocity, float speed = 10.0f, string? id = null) : base(id, EnumComponentType.Physics) {
        this.Model = model;
        this.Velocity = velocity;
        this.Speed = speed;
    }
    public Vector2 Position {
        get => Model.Origin;
        set => Model.Origin = value;
    }
    public float X {
        get => Model.Origin.X;
    }
    public float Y {
        get => Model.Origin.Y;
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Position += this.Velocity * this.Speed * elapsedTime;
    }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) { }
}
