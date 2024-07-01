namespace Arcade.RPG.Systems;

using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Worlds;

using Microsoft.Xna.Framework;

public class WorldSystem : System {
    public WorldSystem(RPG game) : base(game) { }
    public enum EnumAction {
        JoinWorld
    }

    public override void Receive(Message message) {
        if(message.Type == EnumAction.JoinWorld.ToString()) {
            Entity entity = message.Payload;

            if(entity != null) {
                this.Game.World.AddEntity(entity);
            }
        }
    }

    public override void Update(RPG game, GameTime gameTime) {
        AtlasWorld world = game.World as AtlasWorld;
        EntityManager entMgr = world.entityManager;

        entMgr.ClearCache();

        Entity viewportSubject = game.Config.Viewport.Subject;
        Components.Physics subjectPhysics = viewportSubject.GetComponent<Components.Physics>(EnumComponentType.Physics);

        if(subjectPhysics != null) {
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
    }
}