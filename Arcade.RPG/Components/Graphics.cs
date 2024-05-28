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

    public override void Update(RPG game, GameTime gameTime, Entity entity) { }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) {
        Physics physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);

        Vector2 position = physicsComponent.Position;

        int pixelX = (int)(position.X * game.Config.Viewport.TileBaseWidth);
        int pixelY = (int)(position.Y * game.Config.Viewport.TileBaseHeight);

        int width = game.Config.Viewport.TileBaseWidth;
        int height = game.Config.Viewport.TileBaseHeight;

        if(physicsComponent.model is Lib.Geometry.Shapes.Shape shape) {
            if(shape is Lib.Geometry.Shapes.Circle circle) {
                int radius = (int)(circle.Radius * game.Config.Viewport.TileBaseWidth);

                Texture2D circleTexture = CreateCircleTexture(graphicsDevice, radius, this.color);
                spriteBatch.Draw(
                    circleTexture,
                    new Vector2(pixelX - radius, pixelY - radius),
                    this.color
                );
            } else if(shape is Lib.Geometry.Shapes.Rectangle rectangle) {
                spriteBatch.Draw(
                    this.texture,
                    new Microsoft.Xna.Framework.Rectangle(
                        (int)(pixelX),
                        (int)(pixelY),
                        (int)(rectangle.Width * game.Config.Viewport.TileBaseWidth),
                        (int)(rectangle.Height * game.Config.Viewport.TileBaseHeight)
                    ),
                    this.color
                );
            } else {
                spriteBatch.Draw(
                    this.texture,
                    new Microsoft.Xna.Framework.Rectangle(
                        pixelX,
                        pixelY,
                        width,
                        height
                    ),
                    this.color
                );
            }
        }
    }


    private static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius, Color color) {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
        Color[] data = new Color[diameter * diameter];

        float radiiSquared = radius * radius;

        for(int x = 0; x < diameter; x++) {
            for(int y = 0; y < diameter; y++) {
                int index = x * diameter + y;
                Vector2 pos = new Vector2(x - radius, y - radius);
                if(pos.LengthSquared() <= radiiSquared) {
                    data[index] = color;
                } else {
                    data[index] = Color.Transparent;
                }
            }
        }

        texture.SetData(data);
        return texture;
    }
}