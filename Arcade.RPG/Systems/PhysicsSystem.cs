namespace Arcade.RPG.Systems;

using Arcade.RPG.Entities;
using Arcade.RPG.Lib.Geometry.Collisions;

using global::System;
using global::System.Diagnostics;

using Microsoft.Xna.Framework;

public class PhysicsSystem : System {
    public PhysicsSystem(RPG game) : base(game) { }
    public enum EnumAction {
        JoinWorld
    }

    public override void Receive(Message message) {
        Debug.WriteLine($"PhysicsSystem received message: {message.Type}");
    }

    public override void Update(RPG game, GameTime gameTime) {
        EntityManager entMgr = game.World.EntityManager;

        foreach(Entity entity1 in entMgr.cache) {
            foreach(Entity entity2 in entMgr.cache) {
                if(entity2 is TerrainEntity) continue;

                if(entity1 != entity2 && entity1 == game.Config.Viewport.Subject) {
                    Components.Physics physics1 = entity1.GetComponent<Components.Physics>(Components.EnumComponentType.Physics);
                    Components.Physics physics2 = entity2.GetComponent<Components.Physics>(Components.EnumComponentType.Physics);

                    bool collision = CollisionDetector.Check(physics1.Model, physics2.Model);
                    if(collision) {
                        long now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        Debug.WriteLine($"[{now}]: Collision detected between {entity1.Id} and {entity2.Id}");
                    }
                }
            }
        }
    }
}