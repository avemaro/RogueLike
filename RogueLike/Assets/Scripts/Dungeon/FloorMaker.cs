using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static int width = 50;
    static int height = 50;

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

        while (true) {
            var x = Random.Range(0, width - 1);
            var y = Random.Range(0, height - 1);
            if (floor.GetTerrain(x, y) == TerrainType.wall) continue;
            floor.Player.Position = new Cell(x, y);
            break;
        }
        while (true) {
            var x = Random.Range(0, width - 1);
            var y = Random.Range(0, height - 1);
            if (floor.GetTerrain(x, y) == TerrainType.wall) continue;
            floor.StairPosition = new Cell(x, y);
            break;
        }

        return floor;
    }
}
