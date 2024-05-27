namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;

using Microsoft.Xna.Framework;


public class Ellipse : Shape {
    public float SemiMajorAxis { get; set; }
    public float SemiMinorAxis { get; set; }

    public Ellipse(Vector2 origin, float semiMajorAxis, float semiMinorAxis) : base(origin) {
        if(semiMajorAxis <= 0 || semiMinorAxis <= 0) {
            throw new ArgumentException("Semi-major and semi-minor axes must be positive values.");
        }
        SemiMajorAxis = semiMajorAxis;
        SemiMinorAxis = semiMinorAxis;
    }

    public float RadiusX {
        get => SemiMajorAxis;
    }
    public float RadiusY {
        get => SemiMinorAxis;
    }

    public float Area {
        get => MathF.PI * SemiMajorAxis * SemiMinorAxis;
    }

    public float Circumference {
        get {
            float a = SemiMajorAxis;
            float b = SemiMinorAxis;

            return MathF.PI * (3 * (a + b) - MathF.Sqrt((3 * a + b) * (a + 3 * b)));
        }
    }

    public override bool Contains(Vector2 point) {
        float x = point.X - Origin.X;
        float y = point.Y - Origin.Y;

        return MathF.Pow(x / SemiMajorAxis, 2) + MathF.Pow(y / SemiMinorAxis, 2) <= 1;
    }

    public static Ellipse Create(Vector2 origin, float semiMajorAxis, float semiMinorAxis) {
        return new Ellipse(origin, semiMajorAxis, semiMinorAxis);
    }
}