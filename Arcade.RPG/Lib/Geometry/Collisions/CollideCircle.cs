namespace Arcade.RPG.Lib.Geometry.Collisions;

using Arcade.RPG.Lib.Geometry.Shapes;

using Microsoft.Xna.Framework;

public abstract class CollideCircle {
    public static bool WithLine(Circle circle, Line line) {
        return CollideLine.WithCircle(line, circle);
    }

    public static bool WithCircle(Circle circle1, Circle circle2) {
        float distance = Vector2.Distance(circle1.Origin, circle2.Origin);
        return distance <= (circle1.Radius + circle2.Radius);
    }

    public static bool WithEllipse(Circle circle, Ellipse ellipse) {
        return ellipse.Contains(circle.Origin) || ellipse.Contains(new Vector2(circle.Origin.X + circle.Radius, circle.Origin.Y)) || ellipse.Contains(new Vector2(circle.Origin.X - circle.Radius, circle.Origin.Y)) || ellipse.Contains(new Vector2(circle.Origin.X, circle.Origin.Y + circle.Radius)) || ellipse.Contains(new Vector2(circle.Origin.X, circle.Origin.Y - circle.Radius));
    }

    public static bool WithPolygon(Circle circle, Polygon polygon) {
        foreach(var vertex in polygon.Vertices) {
            if(Vector2.Distance(circle.Origin, vertex) <= circle.Radius) {
                return true;
            }
        }
        return false;
    }

    public static bool WithTriangle(Circle circle, Triangle triangle) {
        return WithPolygon(circle, triangle);
    }

    public static bool WithRectangle(Circle circle, Shapes.Rectangle rectangle) {
        return WithPolygon(circle, rectangle);
    }

    public static bool With(Circle circle1, Shape shape) {
        if(shape is Circle circle2) {
            return WithCircle(circle1, circle2);
        } else if(shape is Ellipse ellipse) {
            return WithEllipse(circle1, ellipse);
        } else if(shape is Polygon polygon) {
            return WithPolygon(circle1, polygon);
        } else if(shape is Triangle triangle) {
            return WithTriangle(circle1, triangle);
        } else if(shape is Shapes.Rectangle rectangle) {
            return WithRectangle(circle1, rectangle);
        } else if(shape is Line line) {
            return WithLine(circle1, line);
        } else {
            return false;
        }
    }
}