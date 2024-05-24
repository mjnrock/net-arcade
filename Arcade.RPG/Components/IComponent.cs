namespace Arcade.RPG.Components;

using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface IComponent {
    string id { get; set; }
    EnumComponentType type { get; set; }

    void Update(GameTime gameTime, Entity entity);
    void Draw(SpriteBatch spriteBatch, Entity entity);
}