namespace Arcade.RPG.Lib.Geometry.Collisions;

using Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public abstract class CollideCircle {
    public static bool WithCircle(Circle circle1, Circle circle2) {
        float distance = Vector2.Distance(circle1.Origin, circle2.Origin);
        return distance <= (circle1.Radius + circle2.Radius);
    }

    public static bool WithRectangle(Circle circle, Shapes.Rectangle rectangle) {
        return CollideRectangle.WithCircle(rectangle, circle);
    }

    public static bool With(Circle circle, Shape shape) {
        if(shape is Circle circle2) {
            return WithCircle(circle, circle2);
        } else if(shape is Shapes.Rectangle rectangle) {
            return WithRectangle(circle, rectangle);
        } else {
            return false;
        }
    }
}