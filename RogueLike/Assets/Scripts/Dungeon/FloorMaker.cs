using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static readonly int width = 50;
    static readonly int height = 30;

    public static Floor NextFloor(Floor floor) {
        var nextFloor = new Floor(width, height);
        new Room(nextFloor, (0, 0), (width - 1, height - 1));

        for (var x = 0; x < width; x++)
            if (nextFloor.GetTerrain(x, 0) == TerrainType.land ||
                nextFloor.GetTerrain(x, height - 1) == TerrainType.land) {
                for (var y = 0; y < height; y++)
                    if (SetWall(x, 1, y, 0)) break;
                for (var y = 0; y < height; y++)
                    if (SetWall(x, 1, height - y - 1, 0)) break;
            }

        for (var y = 0; y < height; y++)
            if (nextFloor.GetTerrain(0, y) == TerrainType.land ||
                nextFloor.GetTerrain(width - 1, y) == TerrainType.land) {
                for (var x = 0; x < width; x++)
                    if (SetWall(x, 0, y, 1)) break;
                for (var x = 0; x < width; x++)
                    if (SetWall(width - x - 1, 0, y, 1)) break;
            }

        bool SetWall(int x, int dx, int y, int dy) {
            if (nextFloor.GetTerrain(x - dx, y - dy) == TerrainType.land ||
                nextFloor.GetTerrain(x + dx, y + dy) == TerrainType.land)
                return true;
            nextFloor.SetTerrain(x, y, TerrainType.wall);
            return false;
        }

        nextFloor.Player.Position = nextFloor.GetPosition(TerrainType.land);
        nextFloor.StairPosition = nextFloor.GetPosition(TerrainType.land);


        for (var i = 0; i < 10; i++) {
            var position = nextFloor.GetPosition(TerrainType.land);
            var enemy = Enemy.Create(floor, position, '武');
            nextFloor.Enemies.Add(enemy);
        }

        for (var i = 0; i < 10; i++) {
            var position = nextFloor.GetPosition(TerrainType.land);
            var item = Item.Create(floor, position, '真');
            nextFloor.Items.Add(item);
        }

        return nextFloor;
    }

    public static Floor Create() {
        var floor = new Floor(width, height);
        new Room(floor, (0, 0), (width - 1, height - 1));

        for (var x = 0; x < width; x++)
            if (floor.GetTerrain(x, 0) == TerrainType.land ||
                floor.GetTerrain(x, height - 1) == TerrainType.land) {
                for (var y = 0; y < height; y++)
                    if (SetWall(x, 1, y, 0)) break;
                for (var y = 0; y < height; y++)
                    if (SetWall(x, 1, height - y - 1, 0)) break;
            }

        for (var y = 0; y < height; y++)
            if (floor.GetTerrain(0, y) == TerrainType.land ||
                floor.GetTerrain(width - 1, y) == TerrainType.land) {
                for (var x = 0; x < width; x++)
                    if (SetWall(x, 0, y, 1)) break;
                for (var x = 0; x < width; x++)
                    if (SetWall(width - x - 1, 0, y, 1)) break;
            }

        bool SetWall(int x, int dx, int y, int dy) {
            if (floor.GetTerrain(x - dx, y - dy) == TerrainType.land ||
                floor.GetTerrain(x + dx, y + dy) == TerrainType.land)
                return true;
            floor.SetTerrain(x, y, TerrainType.wall);
            return false;
        }

        floor.Player.Position = floor.GetPosition(TerrainType.land);
        floor.StairPosition = floor.GetPosition(TerrainType.land);


        for (var i = 0; i < 10; i++) {
            var position = floor.GetPosition(TerrainType.land);
            var enemy = Enemy.Create(floor, position, '武');
            floor.Enemies.Add(enemy);
        }

        for (var i = 0; i < 10; i++) {
            var position = floor.GetPosition(TerrainType.land);
            var item = Item.Create(floor, position, '真');
            floor.Items.Add(item);
        }

        return floor;
    }

    public static Floor Create(string[] floorData) {
        var floor = new Floor(width, height);
        FloorInit(floor, floorData);
        return floor;
    }

    public static Floor Create(string text) {
        var floor = new Floor(width, height);
        List<string> floorData = new List<string>();

        var str = "";
        foreach (var c in text) {
            if (c == '\n') {
                floorData.Add(str);
                str = "";
                continue;
            }
            str += c;
        }
        floorData.Add(str);

        FloorInit(floor, floorData.ToArray());
        return floor;
    }

    static void FloorInit(Floor floor, string[] floorData) {
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                if (y > floorData.Length - 1) continue;
                if (x > floorData[y].ToCharArray().Length - 1) continue;

                var cell = new Cell(x, y);
                var data = floorData[y].ToCharArray()[x];
                TerrainType terrain = TerrainTypeExtend.GetTrrainType(data);
                floor.SetTerrain(x, y, terrain);

                if (data == '試') floor.Player.Position = cell;
                if (data == '階') floor.StairPosition = cell;

                var stuff = Stuff.Create(floor, cell, data);
                if (stuff is Item) floor.Items.Add((Item)stuff);
                if (stuff is Enemy) floor.Enemies.Add((Enemy)stuff);
                if (stuff is Trap) floor.Traps.Add((Trap)stuff);
            }
        }
    }
}