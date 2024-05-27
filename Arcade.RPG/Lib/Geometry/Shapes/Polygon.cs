namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

public class Polygon : Shape {
    public List<Vector2> Vertices { get; set; }

    public Polygon(List<Vector2> vertices) : base(vertices[0]) {
        if(vertices.Count < 3) {
            throw new ArgumentException("A polygon must have at least 3 vertices.");
        }
        Vertices = vertices;
    }

    public virtual float Perimeter {
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

    public virtual float Area {
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
        int count = Vertices.Count;
        bool result = false;
        int j = count - 1;

        for(int i = 0; i < count; i++) {
            if(Vertices[i].Y < point.Y && Vertices[j].Y >= point.Y || Vertices[j].Y < point.Y && Vertices[i].Y >= point.Y) {
                if(Vertices[i].X + (point.Y - Vertices[i].Y) / (Vertices[j].Y - Vertices[i].Y) * (Vertices[j].X - Vertices[i].X) < point.X) {
                    result = !result;
                }
            }
            j = i;
        }

        return result;
    }

    public static Polygon CreateRegularPolygon(Vector2 origin, int sides, float radius) {
        if(sides < 3) {
            throw new ArgumentException("A polygon must have at least 3 sides.");
        }

        List<Vector2> vertices = new List<Vector2>();
        for(int i = 0; i < sides; i++) {
            float angle = MathF.PI * 2 / sides * i;
            vertices.Add(new Vector2(origin.X + radius * MathF.Cos(angle), origin.Y + radius * MathF.Sin(angle)));
        }

        return new Polygon(vertices);
    }

    public static Polygon CreateStarPolygon(Vector2 origin, int points, float radius) {
        if(points < 3) {
            throw new ArgumentException("A polygon must have at least 3 points.");
        }

        List<Vector2> vertices = new List<Vector2>();
        for(int i = 0; i < points * 2; i++) {
            float angle = MathF.PI * 2 / (points * 2) * i;
            float r = i % 2 == 0 ? radius : radius / 2;
            vertices.Add(new Vector2(origin.X + r * MathF.Cos(angle), origin.Y + r * MathF.Sin(angle)));
        }

        return new Polygon(vertices);
    }

    public static Polygon Create(List<Vector2> vertices) {
        return new Polygon(vertices);
    }
}