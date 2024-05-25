namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;
using Arcade.RPG;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;

public class Physics : Component {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }
    public float Speed { get; set; }

    public Physics(Vector2 position, Vector2 velocity, float speed = 10.0f, string? id = null) : base(id, EnumComponentType.Physics) {
        this.Position = position;
        this.Velocity = velocity;
        this.Speed = speed;
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {
        float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        this.Position += this.Velocity * this.Speed * elapsedTime;
    }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) { }
}
