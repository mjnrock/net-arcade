using Arcade.RPG.Lib.Geometry.Shapes;

namespace Arcade.RPG.Lib.Geometry.Collisions;

public class CollideRectangle {
    public static bool WithLine(Rectangle rectangle, Line line) {
        return CollideLine.WithRectangle(line, rectangle);
    }

    public static bool WithCircle(Rectangle rectangle, Circle circle) {
        return CollideCircle.WithRectangle(circle, rectangle);
    }

    public static bool WithEllipse(Rectangle rectangle, Ellipse ellipse) {
        return CollideEllipse.WithRectangle(ellipse, rectangle);
    }

    public static bool WithPolygon(Rectangle rectangle, Polygon polygon) {
        return CollidePolygon.WithRectangle(polygon, rectangle);
    }

    public static bool WithTriangle(Rectangle rectangle, Triangle triangle) {
        return CollideTriangle.WithRectangle(triangle, rectangle);
    }

    public static bool WithRectangle(Rectangle rectangle1, Rectangle rectangle2) {
        return rectangle1.Contains(rectangle2.TopLeft) || rectangle1.Contains(rectangle2.TopRight) || rectangle1.Contains(rectangle2.BottomLeft) || rectangle1.Contains(rectangle2.BottomRight);
    }

    public static bool With(Rectangle rectangle1, Shape shape) {
        if(shape is Circle circle) {
            return WithCircle(rectangle1, circle);
        } else if(shape is Ellipse ellipse) {
            return WithEllipse(rectangle1, ellipse);
        } else if(shape is Polygon polygon) {
            return WithPolygon(rectangle1, polygon);
        } else if(shape is Triangle triangle) {
            return WithTriangle(rectangle1, triangle);
        } else if(shape is Rectangle rectangle2) {
            return WithRectangle(rectangle1, rectangle2);
        } else if(shape is Line line) {
            return WithLine(rectangle1, line);
        } else {
            return false;
        }
    }
}