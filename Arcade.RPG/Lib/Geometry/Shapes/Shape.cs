namespace Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public class Shape(Vector2 origin) {
    public Vector2 Origin { get; set; } = origin;

    public float X {
        get => Origin.X;
        set => Origin = new Vector2(value, Origin.Y);
    }

    public float Y {
        get => Origin.Y;
        set => Origin = new Vector2(Origin.X, value);
    }

    public virtual bool Contains(Vector2 point) {
        if(point == Origin) {
            return true;
        }

        return false;
    }

    public static Shape Create(Vector2 origin) {
        return new Shape(origin);
    }
}