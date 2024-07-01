namespace Arcade.RPG.Lib.Geometry.Collisions;

using Arcade.RPG.Lib.Geometry.Shapes;

public abstract class CollisionDetector {
    public static bool Check(Shape shape1, Shape shape2) {
        if(shape1 is Circle circle1) {
            return CollideCircle.With(circle1, shape2);
        } else if(shape1 is Rectangle rectangle1) {
            return CollideRectangle.With(rectangle1, shape2);
        } else {
            return false;
        }
    }
}