using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {
    static readonly int WidthLimit = 30;
    static readonly int HeightLimit = 30;
    static readonly int wallMin = 2;
    static readonly int wallMax = 4;

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

        var divisions = AllDivisions;
        if (Width < WidthLimit) divisions.Remove(Division.vertical);
        if (Height < HeightLimit) divisions.Remove(Division.horizonal);

        var divRatio = Random.Range(2f, 4f);
        var division = divisions[Random.Range(0, divisions.Count)];
        switch (division) {
            case Division.none:
                Debug.Log("none");
                break;
            case Division.vertical:
                Debug.Log("vertical");
                var startV0 = startPoint.Tuple;
                var endV0 = (startPoint.x + Mathf.RoundToInt(Width / divRatio) - 1, endPoint.y);
                var directions = new List<Direction>(neighbor) {
                    Direction.right
                };
                new Room(floor, startV0, endV0, directions.ToArray());
                //new Room(floor, startV0, endV0, Direction.right);

                var startV1 = (startPoint.x + Mathf.RoundToInt(Width / divRatio), startPoint.y);
                var endV1 = endPoint.Tuple;
                directions = new List<Direction>(neighbor) {
                    Direction.left
                };
                new Room(floor, startV1, endV1, directions.ToArray());
                //new Room(floor, startV1, endV1,  Direction.left);
                return;
            case Division.horizonal:
                Debug.Log("horizonal");
                var startH0 = startPoint.Tuple;
                var endH0 = (endPoint.x, startPoint.y + Height / 2 - 1);
                directions = new List<Direction>(neighbor) {
                    Direction.down
                };
                new Room(floor, startH0, endH0, directions.ToArray());
                //new Room(floor, startH0, endH0, Direction.down);

                var startH1 = (startPoint.x, startPoint.y + Height / 2);
                var endH1 = endPoint.Tuple;
                directions = new List<Direction>(neighbor) {
                    Direction.up
                };
                new Room(floor, startH1, endH1, directions.ToArray());
                //new Room(floor, startH1, endH1, Direction.up);
                return;

        }
        wallThickness = Random.Range(wallMin, wallMax);
        this.neighbor = neighbor;
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
        if (neighbor.Length == 0) return;

        int x = 0;
        int y = 0;
        if (neighbor[0] == Direction.up || neighbor[0] == Direction.down) {
            x = startPoint.x + Random.Range(wallThickness, Width - wallThickness);
            if (neighbor[0] == Direction.up)
                y = startPoint.y;
            if (neighbor[0] == Direction.down)
                y = endPoint.y;
            var position = new Cell(x, y);
            for (var i = 0; i < wallThickness; i++) {
                floor.SetTerrain(position.x, position.y, TerrainType.land);
                position = position.Next(neighbor[0].Reverse());
            }
        }

        if (neighbor[0] == Direction.left || neighbor[0] == Direction.right) {
            y = startPoint.y + Random.Range(wallThickness, Height - wallThickness);
            if (neighbor[0] == Direction.right)
                x = endPoint.x;
            if (neighbor[0] == Direction.left)
                x = startPoint.x;
            var position = new Cell(x, y);
            for (var i = 0; i < wallThickness; i++) {
                floor.SetTerrain(position.x, position.y, TerrainType.land);
                position = position.Next(neighbor[0].Reverse());
            }
        }

        //int x = startPoint.x + Random.Range(wallThickness, Width - wallThickness);
        //int y = 0;
        //if (neighbor[0] == Direction.down)
        //    y = endPoint.y;
        //if (neighbor[0] == Direction.up)
        //    y = startPoint.y;
        //var position = new Cell(x, y);
        //for (var i = 0; i < wallThickness; i++) {
        //    floor.SetTerrain(position.x, position.y, TerrainType.land);
        //    position = position.Next(neighbor[0].Reverse());
        //}

        //if (neighbor.Length < 2) return;

        //y = startPoint.y + Random.Range(wallThickness, Height - wallThickness);
        //if (neighbor[1] == Direction.right)
        //    x = endPoint.x;
        //if (neighbor[1] == Direction.left)
        //    x = startPoint.x;
        //position = new Cell(x, y);
        //for (var i = 0; i < wallThickness; i++) {
        //    floor.SetTerrain(position.x, position.y, TerrainType.land);
        //    position = position.Next(neighbor[1].Reverse());
        //}
    }

    enum Division {
        none, vertical, horizonal
    }

    readonly List<Division> AllDivisions = new List<Division>()
    { Division.none, Division.vertical, Division.horizonal };
}