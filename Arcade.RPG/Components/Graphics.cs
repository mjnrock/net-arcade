namespace Arcade.RPG.Components;

using System.Diagnostics;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Graphics : Component {
    public Texture2D texture;
    public int size;
    public Color color;

    public Graphics(GraphicsDevice graphicsDevice, int size, Color startColor, Color endColor) : base(EnumComponentType.Graphics) {
        this.size = size;
        this.color = InterpolateColor(startColor, endColor, size);

        this.texture = new Texture2D(graphicsDevice, 1, 1);
        this.texture.SetData(new[] { this.color });
    }

    private Color InterpolateColor(Color startColor, Color endColor, int size) {
        float factor = (float)(size - 50) / (200 - 50); // Assuming size ranges from 50 to 250
        return Color.Lerp(startColor, endColor, factor);
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {
        // Graphics components may not need to update anything,
        // but this could include animations or other visual updates.
    }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) {
        // Draw the entity at its current position
        var physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);
        if(physicsComponent == null) {
            return;
        }

        var position = physicsComponent.Position;
        spriteBatch.Draw(this.texture, new Rectangle((int)position.X, (int)position.Y, this.size, this.size), this.color);
    }
}
