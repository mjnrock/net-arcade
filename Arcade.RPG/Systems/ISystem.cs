using Microsoft.Xna.Framework;

namespace Arcade.RPG.Systems;

public interface ISystem {
    public void Update(RPG game, GameTime gameTime);
    public void Draw(RPG game, GameTime gameTime);
}