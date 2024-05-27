namespace Arcade.RPG.Lib.Geometry.Collisions;

using Arcade.RPG.Lib.Geometry.Shapes;

public abstract class CollisionDetector {
    public static bool Check(Shape shape1, Shape shape2) {
        if(shape1 is Circle circle1) {
            return CollideCircle.With(circle1, shape2);
        } else if(shape1 is Ellipse ellipse) {
            return CollideEllipse.With(ellipse, shape2);
        } else if(shape1 is Polygon polygon) {
            return CollidePolygon.With(polygon, shape2);
        } else if(shape1 is Triangle triangle) {
            return CollideTriangle.With(triangle, shape2);
        } else if(shape1 is Rectangle rectangle) {
            return CollideRectangle.With(rectangle, shape2);
        } else if(shape1 is Line line) {
            return CollideLine.With(line, shape2);
        } else {
            return false;
        }
    }
}