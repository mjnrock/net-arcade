namespace Arcade.RPG.Lib.Geometry.Collisions;

using System;

using Arcade.RPG.Lib.Geometry.Shapes;

public abstract class CollideRectangle {

    public static bool WithRectangle(Shapes.Rectangle rectangle1, Shapes.Rectangle rectangle2) {
        return rectangle1.X < rectangle2.X + rectangle2.Width &&
               rectangle1.X + rectangle1.Width > rectangle2.X &&
               rectangle1.Y < rectangle2.Y + rectangle2.Height &&
               rectangle1.Y + rectangle1.Height > rectangle2.Y;
    }

    public static bool WithCircle(Shapes.Rectangle rectangle, Circle circle) {
        double distX = Math.Abs(circle.X - rectangle.X - rectangle.Width / 2.0);
        double distY = Math.Abs(circle.Y - rectangle.Y - rectangle.Height / 2.0);

        if(distX > (rectangle.Width / 2.0 + circle.Radius)) { return false; }
        if(distY > (rectangle.Height / 2.0 + circle.Radius)) { return false; }

        if(distX <= (rectangle.Width / 2.0)) { return true; }
        if(distY <= (rectangle.Height / 2.0)) { return true; }

        double dx = distX - rectangle.Width / 2.0;
        double dy = distY - rectangle.Height / 2.0;

        return (dx * dx + dy * dy <= (circle.Radius * circle.Radius));
    }

    public static bool With(Shapes.Rectangle rectangle, Shape shape) {
        if(shape is Circle circle) {
            return WithCircle(rectangle, circle);
        } else if(shape is Shapes.Rectangle rectangle2) {
            return WithRectangle(rectangle, rectangle2);
        } else {
            return false;
        }
    }
}