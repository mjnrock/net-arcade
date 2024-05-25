namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class Graphics : Component {
    public Texture2D texture;
    public int size;
    public Color color;

    public static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, int radius) {
        int diameter = radius * 2;
        Texture2D texture = new Texture2D(graphicsDevice, diameter, diameter);
        Color[] colorData = new Color[diameter * diameter];

        float radiussq = radius * radius;

        for(int y = 0; y < diameter; y++) {
            for(int x = 0; x < diameter; x++) {
                int index = y * diameter + x;
                Vector2 pos = new Vector2(x - radius, y - radius);
                if(pos.LengthSquared() <= radiussq) {
                    colorData[index] = Color.White;
                } else {
                    colorData[index] = Color.Transparent;
                }
            }
        }

        texture.SetData(colorData);
        return texture;
    }

    public Graphics(GraphicsDevice graphicsDevice, Color color) : base(EnumComponentType.Graphics) {
        this.color = color;

        this.texture = new Texture2D(graphicsDevice, 1, 1);
        this.texture.SetData(new[] { this.color });
    }

    public override void Update(RPG game, GameTime gameTime, Entity entity) {}

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) {
        Physics physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);

        Vector2 position = physicsComponent.Position;

        int pixelX = (int)(position.X * game.Konfig.Viewport.TileBaseWidth);
        int pixelY = (int)(position.Y * game.Konfig.Viewport.TileBaseHeight);

        int width = game.Konfig.Viewport.TileBaseWidth;
        int height = game.Konfig.Viewport.TileBaseHeight;

        if(entity is TerrainEntity) {
            spriteBatch.Draw(this.texture, new Rectangle(
                x: pixelX,
                y: pixelY,
                width: width,
                height: height
            ), this.color);
        } else {
            Texture2D circle = Graphics.CreateCircleTexture(graphicsDevice, game.Konfig.Viewport.TileBaseWidth / 2);
            spriteBatch.Draw(circle, new Rectangle(
                x: pixelX,
                y: pixelY,
                width: width,
                height: height
            ), this.color);
        }
    }

}
