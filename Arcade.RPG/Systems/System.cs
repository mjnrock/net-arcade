using Microsoft.Xna.Framework;

namespace Arcade.RPG.Systems;

public class System {
    public RPG Game { get; set; }

    public System(RPG game) {
        this.Game = game;
    }

    public virtual void Receive(Message message) {
    }
    public virtual void Route(EnumSystemType to, Message message) {
        this.Game.Route(to, message);
    }

    public virtual void Update(RPG game, GameTime gameTime) { }
    public virtual void Draw(RPG game, GameTime gameTime) { }
}