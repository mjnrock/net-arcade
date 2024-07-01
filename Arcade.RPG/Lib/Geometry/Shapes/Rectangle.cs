namespace Arcade.RPG.Lib.Geometry.Shapes;

using System;

using Microsoft.Xna.Framework;

public class Rectangle : Shape {
    public override float Width { get; set; }
    public override float Height { get; set; }

    public Rectangle(Vector2 origin, float width, float height) : base(origin) {
        this.Width = width;
        this.Height = height;
    }

    public Vector2 TopLeft {
        get => this.Origin;
    }
    public Vector2 TopRight {
        get => new Vector2(this.Origin.X + this.Width, this.Origin.Y);
    }
    public Vector2 BottomLeft {
        get => new Vector2(this.Origin.X, this.Origin.Y + this.Height);
    }
    public Vector2 BottomRight {
        get => new Vector2(this.Origin.X + this.Width, this.Origin.Y + this.Height);
    }

    public float Perimeter {
        get => 2 * (this.Width + this.Height);
        set {
            float ratio = value / this.Perimeter;
            this.Width *= ratio;
            this.Height *= ratio;
        }
    }
    public float Area {
        get => this.Width * this.Height;
        set {
            float ratio = (float)Math.Sqrt(value / this.Area);
            this.Width *= ratio;
            this.Height *= ratio;
        }
    }

    public static Rectangle Create(Vector2 origin, float width, float height) {
        return new Rectangle(origin, width, height);
    }
}