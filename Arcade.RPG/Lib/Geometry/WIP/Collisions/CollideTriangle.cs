using Arcade.RPG.Lib.Geometry.Shapes;

namespace Arcade.RPG.Lib.Geometry.Collisions;

public class CollideTriangle {
    public static bool WithLine(Triangle triangle, Line line) {
        return CollideLine.WithTriangle(line, triangle);
    }

    public static bool WithCircle(Triangle triangle, Circle circle) {
        return CollideCircle.WithTriangle(circle, triangle);
    }

    public static bool WithEllipse(Triangle triangle, Ellipse ellipse) {
        return CollideEllipse.WithTriangle(ellipse, triangle);
    }

    public static bool WithPolygon(Triangle triangle, Polygon polygon) {
        return CollidePolygon.WithTriangle(polygon, triangle);
    }

    public static bool WithTriangle(Triangle triangle1, Triangle triangle2) {
        foreach(var vertex in triangle1.Vertices) {
            if(triangle2.Contains(vertex)) {
                return true;
            }
        }
        return false;
    }

    public static bool WithRectangle(Triangle triangle, Rectangle rectangle) {
        return CollideRectangle.WithTriangle(rectangle, triangle);
    }

    public static bool With(Triangle triangle1, Shape shape) {
        if(shape is Circle circle) {
            return WithCircle(triangle1, circle);
        } else if(shape is Ellipse ellipse) {
            return WithEllipse(triangle1, ellipse);
        } else if(shape is Triangle triangle2) {
            return WithTriangle(triangle1, triangle2);
        } else if(shape is Rectangle rectangle) {
            return WithRectangle(triangle1, rectangle);
        } else if(shape is Polygon polygon) {
            return WithPolygon(triangle1, polygon);
        } else if(shape is Line line) {
            return WithLine(triangle1, line);
        } else {
            return false;
        }
    }
}