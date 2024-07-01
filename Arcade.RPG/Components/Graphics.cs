namespace Arcade.RPG.Components;

using System;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Shapes = Arcade.RPG.Lib.Geometry.Shapes;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using System.Diagnostics;

public class Graphics : Component {
    public Texture2D texture;
    public Color color;
    public Shapes.Shape model;

    public Graphics(GraphicsDevice graphicsDevice, Color color) : base(EnumComponentType.Graphics) {
        this.color = color;
        this.texture = new Texture2D(graphicsDevice, 1, 1);
        this.texture.SetData(new[] { this.color });
    }

    public override void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch, Entity entity) {
        Physics physicsComponent = entity.GetComponent<Physics>(EnumComponentType.Physics);

        if(this.model == null) {
            this.model = physicsComponent.model;
            UpdateTexture(graphicsDevice, game);
        }

        DrawShape(spriteBatch, physicsComponent, game);
    }

    private void UpdateTexture(GraphicsDevice graphicsDevice, RPG game) {
        if(this.model is Shapes.Circle circle) {
            int radius = Math.Max(0, (int)Math.Round(circle.Radius * game.Config.Viewport.TileBaseWidth));
            this.texture = CreateCircleTexture(graphicsDevice, radius, this.color);
        } else if(this.model is Shapes.Rectangle rectangle) {
            int width = Math.Max(0, (int)Math.Round(rectangle.Width * game.Config.Viewport.TileBaseWidth));
            int height = Math.Max(0, (int)Math.Round(rectangle.Height * game.Config.Viewport.TileBaseHeight));
            this.texture = CreateRectangleTexture(graphicsDevice, width, height, this.color);
        }
    }

    private void DrawShape(SpriteBatch spriteBatch, Physics physicsComponent, RPG game) {
        Vector2 position = physicsComponent.Position;

        float x = position.X * game.Config.Viewport.TileBaseWidth - this.model.Width * game.Config.Viewport.TileBaseWidth / 2.0f;
        float y = position.Y * game.Config.Viewport.TileBaseHeight + this.model.Height * game.Config.Viewport.TileBaseHeight / 2.0f;

        spriteBatch.Draw(
            this.texture,
            new Vector2(
                x,
                y
            ),
            this.color
        );
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
    private static Texture2D CreateRectangleTexture(GraphicsDevice graphicsDevice, int width, int height, Color color) {
        if(width <= 0 || height <= 0) {
            throw new ArgumentException("Width and height must be greater than zero.");
        }

        Texture2D texture = new Texture2D(graphicsDevice, width, height);
        Color[] data = new Color[width * height];

        for(int y = 0; y < height; y++) {
            for(int x = 0; x < width; x++) {
                int index = y * width + x;
                data[index] = color;
            }
        }

        texture.SetData(data);
        return texture;
    }

}