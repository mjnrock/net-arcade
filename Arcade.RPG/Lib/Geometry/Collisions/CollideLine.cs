namespace Arcade.RPG.Lib.Geometry.Collisions;

using System;

using Arcade.RPG.Lib.Geometry.Shapes;

public abstract class CollideLine {
    public static bool WithLine(Line line1, Line line2) {
        float x1 = line1.Origin.X;
        float y1 = line1.Origin.Y;
        float x2 = line1.End.X;
        float y2 = line1.End.Y;
        float x3 = line2.Origin.X;
        float y3 = line2.Origin.Y;
        float x4 = line2.End.X;
        float y4 = line2.End.Y;

        float denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if(denominator == 0) {
            return false;
        }

        float x = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
        float y = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

        return x >= Math.Min(x1, x2) && x <= Math.Max(x1, x2) && y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2) && x >= Math.Min(x3, x4) && x <= Math.Max(x3, x4) && y >= Math.Min(y3, y4) && y <= Math.Max(y3, y4);
    }

    public static bool WithCircle(Line line, Circle circle) {
        return circle.Contains(line.Origin) || circle.Contains(line.End);
    }

    public static bool WithEllipse(Line line, Ellipse ellipse) {
        return ellipse.Contains(line.Origin) || ellipse.Contains(line.End);
    }

    public static bool WithPolygon(Line line, Polygon polygon) {
        for(int i = 0; i < polygon.Vertices.Count; i++) {
            if(WithLine(line, Line.Create(polygon.Vertices[i], polygon.Vertices[(i + 1) % polygon.Vertices.Count]))) {
                return true;
            }
        }

        return false;
    }

    public static bool WithTriangle(Line line, Triangle triangle) {
        return triangle.Contains(line.Origin) || triangle.Contains(line.End) || WithLine(line, Line.Create(triangle.Vertices[0], triangle.Vertices[1])) || WithLine(line, Line.Create(triangle.Vertices[1], triangle.Vertices[2])) || WithLine(line, Line.Create(triangle.Vertices[2], triangle.Vertices[0]));
    }

    public static bool WithRectangle(Line line, Rectangle rectangle) {
        return rectangle.Contains(line.Origin) || rectangle.Contains(line.End) || WithLine(line, Line.Create(rectangle.Vertices[0], rectangle.Vertices[1])) || WithLine(line, Line.Create(rectangle.Vertices[1], rectangle.Vertices[2])) || WithLine(line, Line.Create(rectangle.Vertices[2], rectangle.Vertices[3])) || WithLine(line, Line.Create(rectangle.Vertices[3], rectangle.Vertices[0]));
    }

    public static bool With(Line line1, Shape shape) {
        if(shape is Line line2) {
            return WithLine(line1, line2);
        } else if(shape is Circle circle) {
            return WithCircle(line1, circle);
        } else if(shape is Ellipse ellipse) {
            return WithEllipse(line1, ellipse);
        } else if(shape is Polygon polygon) {
            return WithPolygon(line1, polygon);
        } else if(shape is Triangle triangle) {
            return WithTriangle(line1, triangle);
        } else if(shape is Rectangle rectangle) {
            return WithRectangle(line1, rectangle);
        } else {
            return false;
        }
    }
}