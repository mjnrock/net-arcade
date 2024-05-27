namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

public class Rectangle : Polygon {
    public Rectangle(Vector2 origin, float width, float height)
        : base(new List<Vector2> {
                origin,
                new Vector2(origin.X + width, origin.Y),
                new Vector2(origin.X + width, origin.Y + height),
                new Vector2(origin.X, origin.Y + height) }) { }

    public float Width {
        get => Vector2.Distance(Vertices[0], Vertices[1]);
    }

    public float Height {
        get => Vector2.Distance(Vertices[1], Vertices[2]);
    }

    public Vector2 TopLeft {
        get => Vertices[0];
    }
    public Vector2 TopRight {
        get => Vertices[1];
    }
    public Vector2 BottomRight {
        get => Vertices[2];
    }
    public Vector2 BottomLeft {
        get => Vertices[3];
    }
    public Vector2 Center {
        get => new Vector2((Vertices[0].X + Vertices[2].X) / 2, (Vertices[0].Y + Vertices[2].Y) / 2);
    }

    public override float Perimeter {
        get {
            float perimeter = 0f;
            for(int i = 0; i < Vertices.Count; i++) {
                Vector2 current = Vertices[i];
                Vector2 next = Vertices[(i + 1) % Vertices.Count];
                perimeter += Vector2.Distance(current, next);
            }
            return perimeter;
        }
    }

    public override float Area {
        get {
            float area = 0f;
            for(int i = 0; i < Vertices.Count; i++) {
                Vector2 current = Vertices[i];
                Vector2 next = Vertices[(i + 1) % Vertices.Count];
                area += current.X * next.Y - next.X * current.Y;
            }
            return Math.Abs(area) / 2f;
        }
    }

    public override bool Contains(Vector2 point) {
        return point.X >= Vertices[0].X && point.X <= Vertices[1].X && point.Y >= Vertices[0].Y && point.Y <= Vertices[2].Y;
    }

    public static Rectangle CreateSquare(Vector2 origin, float sideLength) {
        return new Rectangle(origin, sideLength, sideLength);
    }

    public static Rectangle Create(Vector2 origin, float width, float height) {
        return new Rectangle(origin, width, height);
    }
}