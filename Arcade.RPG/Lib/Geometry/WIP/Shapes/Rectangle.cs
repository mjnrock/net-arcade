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
        get => Vector2.Distance(this.Vertices[0], this.Vertices[1]);
    }

    public float Height {
        get => Vector2.Distance(this.Vertices[1], this.Vertices[2]);
    }

    public Vector2 TopLeft {
        get => this.Vertices[0];
    }
    public Vector2 TopRight {
        get => this.Vertices[1];
    }
    public Vector2 BottomRight {
        get => this.Vertices[2];
    }
    public Vector2 BottomLeft {
        get => this.Vertices[3];
    }
    public Vector2 Center {
        get => new Vector2((this.Vertices[0].X + this.Vertices[2].X) / 2, (this.Vertices[0].Y + this.Vertices[2].Y) / 2);
    }

    public override float Perimeter {
        get {
            float perimeter = 0f;
            for(int i = 0; i < this.Vertices.Count; i++) {
                Vector2 current = this.Vertices[i];
                Vector2 next = this.Vertices[(i + 1) % this.Vertices.Count];
                perimeter += Vector2.Distance(current, next);
            }
            return perimeter;
        }
    }

    public override float Area {
        get {
            float area = 0f;
            for(int i = 0; i < this.Vertices.Count; i++) {
                Vector2 current = this.Vertices[i];
                Vector2 next = this.Vertices[(i + 1) % this.Vertices.Count];
                area += current.X * next.Y - next.X * current.Y;
            }
            return Math.Abs(area) / 2f;
        }
    }

    public override bool Contains(Vector2 point) {
        return point.X >= this.TopLeft.X && point.X <= this.TopRight.X && point.Y >= this.TopLeft.Y && point.Y <= this.BottomRight.Y;
    }

    public static Rectangle CreateSquare(Vector2 origin, float sideLength) {
        return new Rectangle(origin, sideLength, sideLength);
    }

    public static Rectangle Create(Vector2 origin, float width, float height) {
        return new Rectangle(origin, width, height);
    }
}