namespace Arcade.Lib;

using System.Collections.Generic;
using System.Diagnostics;

using Arcade.RPG;
using Arcade.RPG.Components;
using Arcade.RPG.Entities;

using Microsoft.Xna.Framework;

public class EntityQuadTree {
    private readonly int MAX_OBJECTS = 4;
    private readonly int MAX_LEVELS = 5;

    private int Level;
    private List<Entity> Objects;
    private Rectangle Bounds;
    private EntityQuadTree[] Nodes;

    public EntityQuadTree(int level, Rectangle bounds) {
        this.Level = level;
        this.Objects = new List<Entity>();
        this.Bounds = bounds;
        this.Nodes = new EntityQuadTree[4];
    }

    public void Clear() {
        Objects.Clear();
        for(int i = 0; i < Nodes.Length; i++) {
            if(Nodes[i] != null) {
                Nodes[i].Clear();
                Nodes[i] = null;
            }
        }
    }

    private void Split() {
        int subWidth = Bounds.Width / 2;
        int subHeight = Bounds.Height / 2;
        int x = Bounds.X;
        int y = Bounds.Y;

        Nodes[0] = new EntityQuadTree(Level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
        Nodes[1] = new EntityQuadTree(Level + 1, new Rectangle(x, y, subWidth, subHeight));
        Nodes[2] = new EntityQuadTree(Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
        Nodes[3] = new EntityQuadTree(Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
    }

    private int GetIndex(Rectangle pRect) {
        int index = -1;
        double verticalMidpoint = Bounds.X + (Bounds.Width / 2);
        double horizontalMidpoint = Bounds.Y + (Bounds.Height / 2);

        bool topQuadrant = pRect.Y < horizontalMidpoint && (pRect.Y + pRect.Height) < horizontalMidpoint;
        bool bottomQuadrant = pRect.Y > horizontalMidpoint;

        if(pRect.X < verticalMidpoint && (pRect.X + pRect.Width) < verticalMidpoint) {
            if(topQuadrant) {
                index = 1;
            } else if(bottomQuadrant) {
                index = 2;
            }
        } else if(pRect.X > verticalMidpoint) {
            if(topQuadrant) {
                index = 0;
            } else if(bottomQuadrant) {
                index = 3;
            }
        }

        return index;
    }

    public void Insert(RPG game, Entity entity) {
        Physics physics = entity.GetComponent<Physics>(EnumComponentType.Physics);
        if(physics == null) {
            return;
        }

        /* NOTE: This appears to have fixed the issue, but verify that it is correct */
        int entityWidth = (int)(physics.model.Width * game.Config.Viewport.TileBaseWidth);
        int entityHeight = (int)(physics.model.Height * game.Config.Viewport.TileBaseHeight);

        Rectangle pRect = new Rectangle((int)physics.X, (int)physics.Y, entityWidth, entityHeight);

        if(Nodes[0] != null) {
            int index = GetIndex(pRect);
            if(index != -1) {
                Nodes[index].Insert(game, entity);
                return;
            }
        }

        Objects.Add(entity);

        if(Objects.Count > MAX_OBJECTS && Level < MAX_LEVELS) {
            if(Nodes[0] == null) {
                Split();
            }

            int i = 0;
            while(i < Objects.Count) {
                Physics physicsObj = Objects[i].GetComponent<Physics>(EnumComponentType.Physics);
                Rectangle objRect = new Rectangle((int)physicsObj.X, (int)physicsObj.Y, entityWidth, entityHeight);
                int index = GetIndex(objRect);
                if(index != -1) {
                    Nodes[index].Insert(game, Objects[i]);
                    Objects.RemoveAt(i);
                } else {
                    i++;
                }
            }
        }
    }

    public List<Entity> Retrieve(List<Entity> returnObjects, Rectangle pRect) {
        int index = GetIndex(pRect);
        if(index != -1 && Nodes[0] != null) {
            Nodes[index].Retrieve(returnObjects, pRect);
        }

        returnObjects.AddRange(Objects);
        return returnObjects;
    }

    public List<Entity> RetrievePotentialCollisions(Entity entity) {
        Physics physics = entity.GetComponent<Physics>(EnumComponentType.Physics);
        if(physics == null) {
            return new List<Entity>();
        }

        // Use actual entity dimensions if available
        int entityWidth = 10; // Replace with actual width
        int entityHeight = 10; // Replace with actual height
        Rectangle pRect = new Rectangle((int)physics.X, (int)physics.Y, entityWidth, entityHeight);
        return Retrieve(new List<Entity>(), pRect);
    }
}