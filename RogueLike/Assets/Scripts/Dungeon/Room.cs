using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    Cell startPoint;
    Cell endPoint;

    public Room((int x, int y) start, (int x, int y) end) {
        startPoint = new Cell(start.x, start.y);
        endPoint = new Cell(end.x, end.y);
    }

    public bool Contains(Cell position) {
        var fromStartPoint = position - startPoint;
        if (fromStartPoint.x < 0 || fromStartPoint.y < 0) return false;
        var toEndPoint = endPoint - position;
        if (toEndPoint.x < 0 || toEndPoint.y < 0) return false;
        return true;
    }
}