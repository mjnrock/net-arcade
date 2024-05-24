namespace Arcade.RPG.Components;

using System.Diagnostics;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Graphics : Component {
    private Texture2D _texture;
    private int _size;
    private Color _color;

    public Graphics(GraphicsDevice graphicsDevice, int size, Color startColor, Color endColor) : base(EnumComponentType.Graphics) {
        _size = size;
        _color = InterpolateColor(startColor, endColor, size);

        _texture = new Texture2D(graphicsDevice, 1, 1);
        _texture.SetData(new[] { _color });
    }

    private Color InterpolateColor(Color startColor, Color endColor, int size) {
        float factor = (float)(size - 50) / (200 - 50); // Assuming size ranges from 50 to 250
        return Color.Lerp(startColor, endColor, factor);
    }

    public override void Update(GameTime gameTime, Entity entity) {
        // Graphics components may not need to update anything,
        // but this could include animations or other visual updates.
    }

    public override void Draw(SpriteBatch spriteBatch, Entity entity) {
        // Draw the entity at its current position
        var physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);
        if(physicsComponent == null) {
            return;
        }

        var position = physicsComponent.Position;
        spriteBatch.Draw(_texture, new Rectangle((int)position.X, (int)position.Y, _size, _size), _color);
    }
}
