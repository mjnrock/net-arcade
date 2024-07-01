namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;

using Microsoft.Xna.Framework;

public class Circle : Shape {
    public float Radius { get; set; }

    public Circle(Vector2 origin, float radius) : base(origin) {
        this.Radius = radius;
    }

    public override float Width {
        get => this.Diameter;
        set => this.Diameter = value;
    }

    public override float Height {
        get => this.Diameter;
        set => this.Diameter = value;
    }

    public float Diameter {
        get => this.Radius * 2;
        set => this.Radius = value / 2;
    }

    public float Circumference {
        get => (float)(2 * Math.PI * this.Radius);
        set => this.Radius = value / (float)(2 * Math.PI);
    }

    public float Area {
        get => (float)(Math.PI * Math.Pow(this.Radius, 2));
        set => this.Radius = (float)Math.Sqrt(value / Math.PI);
    }

    public static Circle Create(Vector2 origin, float radius) {
        return new Circle(origin, radius);
    }
}