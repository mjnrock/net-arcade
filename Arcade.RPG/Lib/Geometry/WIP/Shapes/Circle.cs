namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;

using Microsoft.Xna.Framework;

public class Circle : Ellipse {
    public Circle(Vector2 origin, float radius) : base(origin, radius, radius) { }

    public float Radius {
        get => SemiMajorAxis; // or SemiMinorAxis since they are the same
        set {
            if(value <= 0) {
                throw new ArgumentException("Radius must be a positive value.");
            }
            SemiMajorAxis = value;
            SemiMinorAxis = value;
        }
    }

    public Vector2 Center {
        get => Origin;
    }

    public float Diameter {
        get => 2 * Radius;
    }

    public new float Area {
        get => MathF.PI * MathF.Pow(Radius, 2);
    }

    public new float Circumference {
        get => 2 * MathF.PI * Radius;
    }

    public override bool Contains(Vector2 point) {
        return Vector2.Distance(Origin, point) <= Radius;
    }

    public static Circle Create(Vector2 origin, float radius) {
        return new Circle(origin, radius);
    }
}