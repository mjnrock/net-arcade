namespace Arcade.RPG.Lib.Geometry.Collisions;

using Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public class CollideEllipse {
    public static bool WithLine(Ellipse ellipse, Line line) {
        return CollideLine.WithEllipse(line, ellipse);
    }

    public static bool WithCircle(Ellipse ellipse, Circle circle) {
        return CollideCircle.WithEllipse(circle, ellipse);
    }

    public static bool WithEllipse(Ellipse ellipse1, Ellipse ellipse2) {
        float distance = Vector2.Distance(ellipse1.Origin, ellipse2.Origin);
        return distance <= (ellipse1.RadiusX + ellipse2.RadiusX) && distance <= (ellipse1.RadiusY + ellipse2.RadiusY);
    }

    public static bool WithPolygon(Ellipse ellipse, Polygon polygon) {
        foreach(var vertex in polygon.Vertices) {
            if(ellipse.Contains(vertex)) {
                return true;
            }
        }
        return false;
    }

    public static bool WithTriangle(Ellipse ellipse, Triangle triangle) {
        return CollideTriangle.WithEllipse(triangle, ellipse);
    }

    public static bool WithRectangle(Ellipse ellipse, Shapes.Rectangle rectangle) {
        return CollideRectangle.WithEllipse(rectangle, ellipse);
    }

    public static bool With(Ellipse ellipse1, Shape shape) {
        if(shape is Circle circle) {
            return WithCircle(ellipse1, circle);
        } else if(shape is Ellipse ellipse2) {
            return WithEllipse(ellipse1, ellipse2);
        } else if(shape is Polygon polygon) {
            return WithPolygon(ellipse1, polygon);
        } else if(shape is Triangle triangle) {
            return WithTriangle(ellipse1, triangle);
        } else if(shape is Shapes.Rectangle rectangle) {
            return WithRectangle(ellipse1, rectangle);
        } else if(shape is Line line) {
            return WithLine(ellipse1, line);
        } else {
            return false;
        }
    }
}