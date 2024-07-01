namespace Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public class Line : Shape {
    public Vector2 End { get; set; }

    public Line(Vector2 origin, Vector2 end) : base(origin) {
        End = end;
    }

    public float Length {
        get => Vector2.Distance(Origin, End);
    }
    public float Slope {
        get => (End.Y - Origin.Y) / (End.X - Origin.X);
    }
    public float YIntercept {
        get => Origin.Y - Slope * Origin.X;
    }

    public override bool Contains(Vector2 point) {
        return point.Y == Slope * point.X + YIntercept;
    }

    public static Line Create(Vector2 origin, Vector2 end) {
        return new Line(origin, end);
    }
}