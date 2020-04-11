using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    static readonly int WidthLimit = 30;
    static readonly int HeightLimit = 30;
    static readonly int wallMin = 4;
    static readonly int wallMax = 5;
    static readonly float divRatioMin = 2f;
    static readonly float divRatioMax = 4f;

    readonly Floor floor;
    readonly Cell startPoint;
    readonly Cell endPoint;
    int Width { get => endPoint.x - startPoint.x +  1; }
    int Height { get => endPoint.y - startPoint.y + 1; }
    readonly int wallThickness;
    readonly Direction[] neighbors;

    public Room(Floor floor, (int x, int y) start, (int x, int y) end, params Direction[] neighbors) {
        this.floor = floor;
        this.neighbors = neighbors;
        startPoint = new Cell(start.x, start.y);
        endPoint = new Cell(end.x, end.y);

        var divisions = AllDivisions;
        if (Width < WidthLimit) divisions.Remove(Division.vertical);
        if (Height < HeightLimit) divisions.Remove(Division.horizonal);

        var divRatio = Random.Range(divRatioMin, divRatioMax);
        var division = divisions[Random.Range(0, divisions.Count)];

        switch (division) {
            case Division.vertical:
                var borderX = startPoint.x + Mathf.RoundToInt(Width / divRatio) - 1;
                var endV0 = (borderX, endPoint.y);
                CreateRoom(startPoint.Tuple, endV0, Direction.right);
                var startV1 = (borderX + 1, startPoint.y);
                CreateRoom(startV1, endPoint.Tuple, Direction.left);
                for (var y = startPoint.y; y < endPoint.y + 1; y++)
                    floor.SetTerrain(borderX, y, TerrainType.land);
                return;

            case Division.horizonal:
                var borderY = startPoint.y + Mathf.RoundToInt(Height / divRatio) - 1;
                var endH0 = (endPoint.x, borderY);
                CreateRoom(startPoint.Tuple, endH0, Direction.down);
                var startH1 = (startPoint.x, borderY);
                CreateRoom(startH1, endPoint.Tuple, Direction.up);
                for (var x = startPoint.x; x < endPoint.x + 1; x++)
                    floor.SetTerrain(x, borderY, TerrainType.land);
                return;

        }
        wallThickness = Random.Range(wallMin, wallMax);
        CreateLand();
        CreateExit();
        floor.Rooms.Add(this);
    }

    void CreateRoom((int, int) start0, (int, int) end0, Direction direction) {
        var directions = new List<Direction>(neighbors) { direction };
        new Room(floor, start0, end0, directions.ToArray());
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
        foreach (var neighbor in neighbors) {
            int x = 0;
            int y = 0;
            int dx = Random.Range(wallThickness, Width - wallThickness);
            int dy = Random.Range(wallThickness, Height - wallThickness);
            switch (neighbor) {
                case Direction.up:
                    x = startPoint.x + dx;
                    y = startPoint.y;
                    break;
                case Direction.down:
                    x = startPoint.x + dx;
                    y = endPoint.y;
                    break;
                case Direction.left:
                    x = startPoint.x;
                    y = startPoint.y + dy;
                    break;
                case Direction.right:
                    x = endPoint.x;
                    y = startPoint.y + dy;
                    break;
            }

            var position = new Cell(x, y);
            for (var i = 0; i < wallThickness; i++) {
                floor.SetTerrain(position.x, position.y, TerrainType.land);
                position = position.Next(neighbor.Reverse());
            }
        }
    }

    enum Division {
        none, vertical, horizonal
    }

    readonly List<Division> AllDivisions = new List<Division>()
    { Division.none, Division.vertical, Division.horizonal };
}