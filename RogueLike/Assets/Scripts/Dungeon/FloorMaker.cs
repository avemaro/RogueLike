using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static int width = 50;
    static int height = 50;

public static Floor Create(int roomCount) {
        var floor = new Floor(width, height);

        new Room(floor, (0, 0), (width - 1, height - 1));

        for (var x = 0; x < width; x++) {
            if (floor.GetTerrain(x, 0) == TerrainType.land) {
                for (var y = 0; y < height; y++) {
                    if (floor.GetTerrain(x - 1, y) == TerrainType.land ||
                        floor.GetTerrain(x + 1, y) == TerrainType.land)
                        break;
                    floor.SetTerrain(x, y, TerrainType.wall);
                }
            }
            if (floor.GetTerrain(x, height-1) == TerrainType.land) {
                for (var y = height - 1; y > 0; y--) {
                    if (floor.GetTerrain(x - 1, y) == TerrainType.land ||
                        floor.GetTerrain(x + 1, y) == TerrainType.land)
                        break;
                    floor.SetTerrain(x, y, TerrainType.wall);
                }
            }
        }

        for (var y = 0; y < height; y++) {
            if (floor.GetTerrain(0, y) == TerrainType.land) {
                for (var x = 0; x < width; x++) {
                    if (floor.GetTerrain(x, y - 1) == TerrainType.land ||
                        floor.GetTerrain(x, y + 1) == TerrainType.land)
                        break;
                    floor.SetTerrain(x, y, TerrainType.wall);
                }
            }
            if (floor.GetTerrain(width - 1, y) == TerrainType.land) {
                for (var x = width - 1; x > 0; x--) {
                    if (floor.GetTerrain(x, y - 1) == TerrainType.land ||
                        floor.GetTerrain(x, y + 1) == TerrainType.land)
                        break;
                    floor.SetTerrain(x, y, TerrainType.wall);
                }
            }
        }

        return floor;
    }
}
