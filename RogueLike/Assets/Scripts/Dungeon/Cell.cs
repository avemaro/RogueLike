﻿using System;
using System.Collections.Generic;

public class Cell : IEquatable<Cell>, IEquatable<(int x, int y)> {
    public int x;
    public int y;

    public (int x, int y) Tuple {
        get { return (x, y); }
    }


    public Cell(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public List<Cell> Around {
        get {
            var cells = new List<Cell>();
            foreach (var direction in DirectionExtend.AllCases())
                cells.Add(Next(direction));
            return cells;
        }
    }

    public virtual Cell Next(Direction direction) {
        return new Cell(x + direction.GetValue().x, y + direction.GetValue().y);
    }

    public List<Cell> Next(params Direction[] directions) {
        var cells = new List<Cell>();
        foreach (var direction in directions)
            cells.Add(Next(direction));
        return cells;
    }

    public Direction Direction {
        get {
            var dx = 0;
            if (x > 0) dx = 1;
            if (x < 0) dx = -1;
            var dy = 0;
            if (y > 0) dy = 1;
            if (y < 0) dy = -1;

            if (dx == 0 && dy == -1) return Direction.up;
            if (dx == 1 && dy == -1) return Direction.upRight;
            if (dx == 1 && dy == 0) return Direction.right;
            if (dx == 1 && dy == 1) return Direction.downRight;
            if (dx == 0 && dy == 1) return Direction.down;
            if (dx == -1 && dy == 1) return Direction.downLeft;
            if (dx == -1 && dy == 0) return Direction.left;
            if (dx == -1 && dy == -1) return Direction.upLeft;
            return Direction.up;
        }
    }

    public static List<Cell> GetCells(Cell from, Cell to, Direction direction) {
        var cells = new List<Cell>();
        var nextCell = from;
        for (var i = 0; i < 10; i++) {
            cells.Add(nextCell);
            if (nextCell == to) return cells;
            nextCell = nextCell.Next(direction);
        }
        return cells;
    }


    public bool Equals(Cell other) {
        if (other is null) return false;
        return other.x == x && other.y == y;
    }

    public bool Equals((int x, int y) other) {
        return other.x == x && other.y == y;
    }

    public static bool operator ==(Cell lhs, Cell rhs) {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Cell lhs, Cell rhs) {
        return !lhs.Equals(rhs);
    }

    public static bool operator ==(Cell lhs, (int x, int y) rhs) {
        return lhs.x == rhs.x && lhs.y == rhs.y;
    }

    public static bool operator !=(Cell lhs, (int x, int y) rhs) {
        return lhs.x != rhs.x || lhs.y != rhs.y;
    }

    public override bool Equals(object obj) {
        return base.Equals(obj);
    }

    public static Cell operator +(Cell lhs, Cell rhs) {
        return new Cell(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static Cell operator +(Cell lhs, (int x, int y) rhs) {
        return new Cell(lhs.x + rhs.x, lhs.y + rhs.y);
    }

    public static Cell operator -(Cell lhs, Cell rhs) {
        return new Cell(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public static Cell operator -(Cell lhs, (int x, int y) rhs) {
        return new Cell(lhs.x - rhs.x, lhs.y - rhs.y);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public override string ToString() {
        return "(" + x + ", " + y + ")";
    }
}
