using Arcade.RPG.Lib.Geometry.Shapes;

namespace Arcade.RPG.Lib.Geometry.Collisions;

public class CollidePolygon {
    public static bool WithLine(Polygon polygon, Line line) {
        return CollideLine.WithPolygon(line, polygon);
    }

    public static bool WithCircle(Polygon polygon, Circle circle) {
        return CollideCircle.WithPolygon(circle, polygon);
    }

    public static bool WithEllipse(Polygon polygon, Ellipse ellipse) {
        return CollideEllipse.WithPolygon(ellipse, polygon);
    }

    public static bool WithPolygon(Polygon polygon1, Polygon polygon2) {
        foreach(var vertex in polygon1.Vertices) {
            if(polygon2.Contains(vertex)) {
                return true;
            }
        }
        return false;
    }

    public static bool WithTriangle(Polygon polygon, Triangle triangle) {
        return CollideTriangle.WithPolygon(triangle, polygon);
    }

    public static bool WithRectangle(Polygon polygon, Rectangle rectangle) {
        return CollideRectangle.WithPolygon(rectangle, polygon);
    }

    public static bool With(Polygon polygon1, Shape shape) {
        if(shape is Circle circle) {
            return WithCircle(polygon1, circle);
        } else if(shape is Ellipse ellipse) {
            return WithEllipse(polygon1, ellipse);
        } else if(shape is Triangle triangle) {
            return WithTriangle(polygon1, triangle);
        } else if(shape is Rectangle rectangle) {
            return WithRectangle(polygon1, rectangle);
        } else if(shape is Polygon polygon2) {
            return WithPolygon(polygon1, polygon2);
        } else if(shape is Line line) {
            return WithLine(polygon1, line);
        } else {
            return false;
        }
    }
}