
using System.Collections.Generic;

public enum Direction {
    up, upRight, right, downRight, down, downLeft, left, upLeft
}

public static class DirectionExtend {
    public static Direction[] AllCases() {
        return new Direction[] { Direction.up, Direction.upRight, Direction.right,
        Direction.downRight, Direction.down, Direction.downLeft, Direction.left,
        Direction.upLeft };
    }

    public static List<Direction> GetDirections(params int[] indexes) {
        var directions = new List<Direction>();
        foreach (var index in indexes)
            directions.Add(AllCases()[index]);
        return directions;
    }

    public static bool IsDiagonal(this Direction direction) {
        switch (direction) {
            case Direction.upRight:
            case Direction.downRight:
            case Direction.downLeft:
            case Direction.upLeft: return true;
            default: return false;
        }
    }

    public static (int x, int y) GetValue(this Direction direction) {
        switch (direction) {
            case Direction.up: return (0, -1);
            case Direction.upRight: return (1, -1);
            case Direction.right: return (1, 0);
            case Direction.downRight: return (1, 1);
            case Direction.down: return (0, 1);
            case Direction.downLeft: return (-1, 1);
            case Direction.left: return (-1, 0);
            case Direction.upLeft: return (-1, -1);
            default: return (0, 0);
        }
    }

    public static Direction Reverse(this Direction direction) {
        switch (direction) {
            case Direction.up: return Direction.down;
            case Direction.upRight: return Direction.downLeft;
            case Direction.right: return Direction.left;
            case Direction.downRight: return Direction.upLeft;
            case Direction.down: return Direction.up;
            case Direction.downLeft: return Direction.upRight;
            case Direction.left: return Direction.right;
            default: return Direction.downRight;
        }
    }

    public static Direction[] Forwards(this Direction direction) {
        switch (direction) {
            case Direction.up:
                return new Direction[] { Direction.upLeft, Direction.up, Direction.upRight };
            case Direction.upRight:
                return new Direction[] { Direction.up, Direction.upRight, Direction.right };
            case Direction.right:
                return new Direction[] { Direction.upRight, Direction.right, Direction.downRight };
            case Direction.downRight:
                return new Direction[] { Direction.right, Direction.downRight, Direction.down };
            case Direction.down:
                return new Direction[] { Direction.downRight, Direction.down, Direction.downLeft };
            case Direction.downLeft:
                return new Direction[] { Direction.down, Direction.downLeft, Direction.left };
            case Direction.left:
                return new Direction[] { Direction.downLeft, Direction.left, Direction.upLeft };
            case Direction.upLeft:
                return new Direction[] { Direction.left, Direction.upLeft, Direction.up };
            default: return new Direction[0];
        }
    }

    public static Direction TurnLeft(this Direction direction) {
        var forwards = direction.Forwards();
        return forwards[0];
    }

    public static Direction TurnRight(this Direction direction) {
        var forwards = direction.Forwards();
        return forwards[2];
    }
}

