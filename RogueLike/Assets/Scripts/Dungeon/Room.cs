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
        startPoint = new Cell(start.x, start.y);
        endPoint = new Cell(end.x, end.y);

        var divisions = AllDivisions;
        if (Width < WidthLimit) divisions.Remove(Division.vertical);
        if (Height < HeightLimit) divisions.Remove(Division.horizonal);

        var divRatio = Random.Range(divRatioMin, divRatioMax);
        var division = divisions[Random.Range(0, divisions.Count)];
        switch (division) {
            case Division.vertical:
                var startV0 = startPoint.Tuple;
                var endV0 = (startPoint.x + Mathf.RoundToInt(Width / divRatio) - 1, endPoint.y);
                var directions = new List<Direction>(neighbors) {
                    Direction.right
                };
                new Room(floor, startV0, endV0, directions.ToArray());

                var startV1 = (startPoint.x + Mathf.RoundToInt(Width / divRatio), startPoint.y);
                var endV1 = endPoint.Tuple;
                directions = new List<Direction>(neighbors) {
                    Direction.left
                };
                new Room(floor, startV1, endV1, directions.ToArray());

                for (var y = startPoint.y; y < endPoint.y + 1; y++)
                    floor.SetTerrain(endV0.Item1, y, TerrainType.land);

                return;
            case Division.horizonal:
                var startH0 = startPoint.Tuple;
                var endH0 = (endPoint.x, startPoint.y + Height / 2 - 1);
                directions = new List<Direction>(neighbors) {
                    Direction.down
                };
                new Room(floor, startH0, endH0, directions.ToArray());

                var startH1 = (startPoint.x, startPoint.y + Height / 2);
                var endH1 = endPoint.Tuple;
                directions = new List<Direction>(neighbors) {
                    Direction.up
                };
                new Room(floor, startH1, endH1, directions.ToArray());

                for (var x = startPoint.x; x < endPoint.x + 1; x++)
                    floor.SetTerrain(x, endH0.Item2, TerrainType.land);

                return;

        }
        wallThickness = Random.Range(wallMin, wallMax);
        this.neighbors = neighbors;
        CreateLand();
        CreateExit();
        floor.Rooms.Add(this);
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
            if (neighbor == Direction.up || neighbor == Direction.down) {
                x = startPoint.x + Random.Range(wallThickness, Width - wallThickness);
                if (neighbor == Direction.up)
                    y = startPoint.y;
                if (neighbor == Direction.down)
                    y = endPoint.y;
                var position = new Cell(x, y);
                for (var i = 0; i < wallThickness; i++) {
                    floor.SetTerrain(position.x, position.y, TerrainType.land);
                    position = position.Next(neighbor.Reverse());
                }
            }

            if (neighbor == Direction.left || neighbor == Direction.right) {
                y = startPoint.y + Random.Range(wallThickness, Height - wallThickness);
                if (neighbor == Direction.right)
                    x = endPoint.x;
                if (neighbor == Direction.left)
                    x = startPoint.x;
                var position = new Cell(x, y);
                for (var i = 0; i < wallThickness; i++) {
                    floor.SetTerrain(position.x, position.y, TerrainType.land);
                    position = position.Next(neighbor.Reverse());
                }
            }
        }
    }

    enum Division {
        none, vertical, horizonal
    }

    readonly List<Division> AllDivisions = new List<Division>()
    { Division.none, Division.vertical, Division.horizonal };
}