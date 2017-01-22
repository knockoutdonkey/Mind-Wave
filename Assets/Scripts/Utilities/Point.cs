using UnityEngine;

public struct Point {
    public int X;
    public int Y;

    public Point(int x, int y) {
        X = x;
        Y = y;
    }

    public Point(Vector3 position) {
        X = Mathf.RoundToInt(position.x);
        Y = Mathf.RoundToInt(position.y);
    }

    public Point(Vector3 position , int xOffset, int yOffset)
    {
        X = Mathf.RoundToInt(position.x);
        Y = Mathf.RoundToInt(position.y);
        X += xOffset;
        Y += yOffset;
    }

    public string GetCoord() {
        return X + " " + Y;
    }
    
    public override string ToString() {
        return "Point: " + GetCoord();
    }

    public override bool Equals(object obj) {
        var otherPoint = (Point)obj;
        return otherPoint.X == X & otherPoint.Y == Y;
    }

    public override int GetHashCode() {
        return X*10000 + Y;
    }
}
