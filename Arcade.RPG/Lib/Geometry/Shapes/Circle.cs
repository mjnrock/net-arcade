namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;

using Microsoft.Xna.Framework;

public class Circle : Shape {
    public float Radius { get; set; }

    public Circle(Vector2 origin, float radius) : base(origin) {
        Radius = radius;
    }

    public Vector2 Center {
        get => Origin;
    }

    public float Diameter {
        get => 2 * Radius;
    }
    public float Area {
        get => MathF.PI * MathF.Pow(Radius, 2);
    }
    public float Circumference {
        get => 2 * MathF.PI * Radius;
    }

    public override bool Contains(Vector2 point) {
        return Vector2.Distance(Origin, point) <= Radius;
    }

    public static Circle Create(Vector2 origin, float radius) {
        return new Circle(origin, radius);
    }
}