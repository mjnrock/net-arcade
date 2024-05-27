namespace Arcade.RPG.Systems;

using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Worlds;

using global::System.Diagnostics;

using Microsoft.Xna.Framework;

public class WorldSystem : System {
    public WorldSystem(RPG game) : base(game) { }
    public enum EnumAction {
        JoinWorld
    }

    public override void Receive(Message message) {
        Debug.WriteLine($"WorldSystem received message: {message.Type}");

        if(message.Type == EnumAction.JoinWorld.ToString()) {
            Entity entity = message.Payload;

            if(entity != null) {
                this.Game.World.AddEntity(entity);
            }
        }
    }

    public override void Update(RPG game, GameTime gameTime) {
        World world = game.World;
        EntityManager entMgr = world.EntityManager;
        entMgr.ClearCache();

        Entity viewportSubject = game.Config.Viewport.Subject;
        Components.Physics physics = viewportSubject.GetComponent<Components.Physics>(EnumComponentType.Physics);

        if(physics != null) {
            world.entities.ForEach(entity => {
                Components.Physics physics = entity.GetComponent<Components.Physics>(EnumComponentType.Physics);

                if(physics != null) {
                    Vector2 position = physics.Position;

                    if(game.Config.IsWithinViewport(position, 1.0f)) {
                        entMgr.WriteCache(entity);
                    }
                }
            });
        } else {
            world.entities.ForEach(entity => {
                entMgr.WriteCache(entity);
            });
        }

        world.Update(game, gameTime);
    }
}