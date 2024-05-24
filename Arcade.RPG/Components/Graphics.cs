namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Graphics : Component {
    public Texture2D texture;
    public int size;
    public Color color;

    public Graphics(GraphicsDevice graphicsDevice, Color color) : base(EnumComponentType.Graphics) {
        this.color = color;

        this.texture = new Texture2D(graphicsDevice, 1, 1);
        this.texture.SetData(new[] { this.color });
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {}

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) {
        Physics physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);

        int size = 32;
        Vector2 position = physicsComponent.Position;
        spriteBatch.Draw(this.texture, new Rectangle(
            x: (int)position.X * size,
            y: (int)position.Y * size,
            width: size,
            height: size
        ), this.color);
    }
}
