using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static int width = 50;
    static int height = 50;

public static Floor Create(int roomCount) {
        var floor = new Floor(width, height);
        if (roomCount == 1) return floor;


        var board = Random.Range(height * 1 / 4, height * 3 / 4);
        var room1 = new Room(floor, (0, 0), (width - 1, board-1), Direction.down);
        var room2 = new Room(floor, (0, board), (width - 1, height - 1), Direction.up);

        floor.Rooms.Add(room1);
        floor.Rooms.Add(room2);

        for (var x = 0; x < width; x++) {
            floor.SetTerrain(x, board, TerrainType.land);
        }

        for (var x = 0; x < width; x++) {
            if (floor.GetTerrain(x, board-1) == TerrainType.land) break;
            if (floor.GetTerrain(x, board+1) == TerrainType.land) break;
            floor.SetTerrain(x, board, TerrainType.wall);
        }

        for (var x = width - 1; x > 0; x--) {
            if (floor.GetTerrain(x, board-1) == TerrainType.land) break;
            if (floor.GetTerrain(x, board+1) == TerrainType.land) break;
            floor.SetTerrain(x, board, TerrainType.wall);
        }

        return floor;
    }
}
