using Arcade.RPG.Components;
using Arcade.RPG.Entities;
using Arcade.RPG.Worlds;

using Microsoft.Xna.Framework;

namespace Arcade.RPG.Systems;

public class WorldSystem : ISystem {
    public WorldSystem() {

    }

    public void Update(RPG game, GameTime gameTime) {
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

    public void Draw(RPG game, GameTime gameTime) { }
}