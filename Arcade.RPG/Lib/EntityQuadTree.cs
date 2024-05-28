namespace Arcade.Lib;

using System.Collections.Generic;

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
        int subWidth = (int)(Bounds.Width / 2);
        int subHeight = (int)(Bounds.Height / 2);
        int x = (int)Bounds.X;
        int y = (int)Bounds.Y;

        Nodes[0] = new EntityQuadTree(Level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
        Nodes[1] = new EntityQuadTree(Level + 1, new Rectangle(x, y, subWidth, subHeight));
        Nodes[2] = new EntityQuadTree(Level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
        Nodes[3] = new EntityQuadTree(Level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
    }

    private int GetIndex(Rectangle pRect) {
        int index = -1;
        double verticalMidpoint = Bounds.X + (Bounds.Width / 2);
        double horizontalMidpoint = Bounds.Y + (Bounds.Height / 2);

        bool topQuadrant = (pRect.Y < horizontalMidpoint && pRect.Y + pRect.Height < horizontalMidpoint);
        bool bottomQuadrant = (pRect.Y > horizontalMidpoint);

        if(pRect.X < verticalMidpoint && pRect.X + pRect.Width < verticalMidpoint) {
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

    public void Insert(Entity entity) {
        Physics physics = entity.GetComponent<Physics>(EnumComponentType.Physics);
        if(physics == null) {
            return;
        }

        Rectangle pRect = new Rectangle((int)physics.X, (int)physics.Y, 1, 1);

        if(Nodes[0] != null) {
            int index = GetIndex(pRect);

            if(index != -1) {
                Nodes[index].Insert(entity);
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
                Rectangle objRect = new Rectangle((int)physicsObj.X, (int)physicsObj.Y, 1, 1);
                int index = GetIndex(objRect);
                if(index != -1) {
                    Nodes[index].Insert(Objects[i]);
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

        Rectangle pRect = new Rectangle((int)physics.X, (int)physics.Y, 1, 1);
        return Retrieve(new List<Entity>(), pRect);
    }
}