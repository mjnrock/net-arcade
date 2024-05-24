namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Physics : Component {
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    public Physics(Vector2 position, Vector2 velocity, string? id = null) : base(id, EnumComponentType.Physics) {
        Position = position;
        Velocity = velocity;
    }

    public override void Update(GameTime gameTime, Entity entity) {
        Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
    }

    public override void Draw(SpriteBatch spriteBatch, Entity entity) {
        // Physics components generally don't draw anything,
        // so this might be left empty or could log a message if needed.
    }
}
