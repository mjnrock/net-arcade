using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Arcade.RPG.Systems;

public class System {
    public RPG Game { get; set; }

    public System(RPG game) {
        this.Game = game;
    }

    public virtual void Receive(Message message) { }
    /* Push a message to the game's router */
    public virtual void Route(EnumSystemType to, Message message) {
        this.Game.Route(to, message);
    }

    public virtual void Update(RPG game, GameTime gameTime) { }
    public virtual void Draw(RPG game, GraphicsDevice graphicsDevice, GameTime gameTime, SpriteBatch spriteBatch) { }
}