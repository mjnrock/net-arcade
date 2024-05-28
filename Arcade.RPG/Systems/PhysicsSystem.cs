namespace Arcade.RPG.Systems;

using Arcade.RPG.Entities;
using Arcade.RPG.Lib.Geometry.Collisions;
using Arcade.RPG.Worlds;

using global::System;
using global::System.Collections.Generic;
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
        AtlasWorld world = game.World as AtlasWorld;
        EntityManager entMgr = world.entityManager;

        foreach(Entity entity1 in entMgr.cache) {
            if(entity1 is TerrainEntity) continue;

            List<Entity> entities = world.collisionQuadTree.RetrievePotentialCollisions(entity1);
            foreach(Entity entity2 in entities) {
                if(entity2 is TerrainEntity) continue;

                if(entity1 != entity2 && entity1 == game.Config.Viewport.Subject) {
                    Components.Physics physics1 = entity1.GetComponent<Components.Physics>(Components.EnumComponentType.Physics);
                    Components.Physics physics2 = entity2.GetComponent<Components.Physics>(Components.EnumComponentType.Physics);

                    bool collision = CollisionDetector.Check(physics1.model, physics2.model);
                    if(collision) {
                        // STUB: Implement collision responses
                        game.Debug.Log($"Collision detected between {entity1.Id} and {entity2.Id}");
                        Components.Graphics graphics1 = entity1.GetComponent<Components.Graphics>(Components.EnumComponentType.Graphics);
                        Components.Graphics graphics2 = entity2.GetComponent<Components.Graphics>(Components.EnumComponentType.Graphics);
                        Color tempColor = graphics1.color;
                        graphics1.color = graphics2.color;
                        graphics2.color = tempColor;
                    }
                }
            }
        }
    }
}