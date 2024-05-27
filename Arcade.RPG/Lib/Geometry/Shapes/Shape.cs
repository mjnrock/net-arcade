namespace Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public class Shape(Vector2 origin) {
    public Vector2 Origin { get; set; } = origin;

    public float X {
        get => this.Origin.X;
        set => this.Origin = new Vector2(value, this.Origin.Y);
    }

    public float Y {
        get => this.Origin.Y;
        set => this.Origin = new Vector2(this.Origin.X, value);
    }

    public static Shape Create(Vector2 origin) {
        return new Shape(origin);
    }
}