using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    readonly Floor floor;
    readonly Cell startPoint;
    readonly Cell endPoint;
    int Width { get => endPoint.x - startPoint.x +  1; }
    int Height { get => endPoint.y - startPoint.y + 1; }
    readonly int wallThickness;
    readonly Direction[] neighbor;

    public Room(Floor floor, (int x, int y) start, (int x, int y) end, params Direction[] neighbor) {
        this.floor = floor;
        startPoint = new Cell(start.x, start.y);
        endPoint = new Cell(end.x, end.y);
        
        wallThickness = Random.Range(3, 6);
        this.neighbor = neighbor;

        CreateLand();
        CreateExit();
    }

    public bool Contains(Cell position) {
        var fromStartPoint = position - startPoint;
        if (fromStartPoint.x < 0 || fromStartPoint.y < 0) return false;
        var toEndPoint = endPoint - position;
        if (toEndPoint.x < 0 || toEndPoint.y < 0) return false;
        return true;
    }

    void CreateLand() {
        for (var dx = wallThickness; dx < Width - wallThickness; dx++) {
            for (var dy = wallThickness; dy < Height - wallThickness; dy++) {
                var x = startPoint.x + dx;
                var y = startPoint.y + dy;
                floor.SetTerrain(x, y, TerrainType.land);
            }
        }
    }

    void CreateExit() {
        if (neighbor.Length == 0) return;

        int x = startPoint.x + Random.Range(wallThickness, Width - wallThickness);
        int y = 0;
        if (neighbor[0] == Direction.down)
            y = endPoint.y;
        if (neighbor[0] == Direction.up)
            y = startPoint.y;
        var position = new Cell(x, y);
        for (var i = 0; i < wallThickness; i++) {
            floor.SetTerrain(position.x, position.y, TerrainType.land);
            position = position.Next(neighbor[0].Reverse());
        }

        if (neighbor.Length < 2) return;

        y = startPoint.y + Random.Range(wallThickness, Height - wallThickness);
        if (neighbor[1] == Direction.right)
            x = endPoint.x;
        if (neighbor[1] == Direction.left)
            x = startPoint.x;
        position = new Cell(x, y);
        for (var i = 0; i < wallThickness; i++) {
            floor.SetTerrain(position.x, position.y, TerrainType.land);
            position = position.Next(neighbor[1].Reverse());
        }

    }
}