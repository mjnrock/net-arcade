namespace Arcade.RPG.Lib.Geometry.Shapes;

using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;

public class Triangle : Polygon {
    public Triangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3) : base(new List<Vector2> { vertex1, vertex2, vertex3 }) { }

    public Vector2 Center {
        get => new Vector2((this.Vertices[0].X + this.Vertices[1].X + this.Vertices[2].X) / 3, (this.Vertices[0].Y + this.Vertices[1].Y + this.Vertices[2].Y) / 3);
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
        int count = this.Vertices.Count;
        bool result = false;
        int j = count - 1;

        for(int i = 0; i < count; i++) {
            if(this.Vertices[i].Y < point.Y && this.Vertices[j].Y >= point.Y || this.Vertices[j].Y < point.Y && this.Vertices[i].Y >= point.Y) {
                if(this.Vertices[i].X + (point.Y - this.Vertices[i].Y) / (this.Vertices[j].Y - this.Vertices[i].Y) * (this.Vertices[j].X - this.Vertices[i].X) < point.X) {
                    result = !result;
                }
            }
            j = i;
        }

        return result;
    }

    public static Triangle CreateEquilateralTriangle(Vector2 origin, float sideLength) {
        float height = (float)(Math.Sqrt(3) / 2 * sideLength);
        List<Vector2> vertices = new List<Vector2> {
                origin,
                new Vector2(origin.X + sideLength / 2, origin.Y - height),
                new Vector2(origin.X - sideLength / 2, origin.Y - height)
            };
        return new Triangle(vertices[0], vertices[1], vertices[2]);
    }
    public static Triangle CreateIsoscelesTriangle(Vector2 origin, float baseLength, float height) {
        List<Vector2> vertices = new List<Vector2> {
                origin,
                new Vector2(origin.X + baseLength, origin.Y),
                new Vector2(origin.X + baseLength / 2, origin.Y - height)
            };
        return new Triangle(vertices[0], vertices[1], vertices[2]);
    }

    public static Triangle CreateRightTriangle(Vector2 origin, float baseLength, float height) {
        List<Vector2> vertices = new List<Vector2> {
                origin,
                new Vector2(origin.X + baseLength, origin.Y),
                new Vector2(origin.X, origin.Y - height)
            };
        return new Triangle(vertices[0], vertices[1], vertices[2]);
    }

    public static Triangle CreateScaleneTriangle(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3) {
        return new Triangle(vertex1, vertex2, vertex3);
    }

    public static Triangle Create(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3) {
        return new Triangle(vertex1, vertex2, vertex3);
    }
}