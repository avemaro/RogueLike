using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static int width = 50;
    static int height = 50;

public static Floor Create(int roomCount) {
        var floor = new Floor(width, height);

        if (roomCount == 1) {
            var room = new Room(floor, (0, 0), (width - 1, height - 1));
            floor.Rooms.Add(room);
            return floor;
        }

        var boardY = 0;
        if (roomCount >= 2) {
            boardY = Random.Range(height * 1 / 4, height * 3 / 4);
            var room0 = new Room(floor, (0, 0), (width - 1, boardY - 1), Direction.down);
            floor.Rooms.Add(room0);

            if (roomCount < 3) {
                var room1 = new Room(floor, (0, boardY), (width - 1, height - 1), Direction.up);
                floor.Rooms.Add(room1);
            }
        }
        if (roomCount >= 3) {
            var boardX = Random.Range(width * 1 / 4, width * 3 / 4);
            var room1 = new Room(floor, (0, boardY), (boardX - 1, height - 1), Direction.up, Direction.right);
            var room2 = new Room(floor, (boardX, boardY), (width - 1, height - 1), Direction.up, Direction.left);

            floor.Rooms.Add(room1);
            floor.Rooms.Add(room2);

            for (var y = boardY; y < height; y++)
                floor.SetTerrain(boardX, y, TerrainType.land);

            for (var y = boardY; y < height; y++) {
                if (floor.GetTerrain(boardX - 1, y) == TerrainType.land) break;
                if (floor.GetTerrain(boardX + 1, y) == TerrainType.land) break;
                floor.SetTerrain(boardX, y, TerrainType.wall);
            }
            for (var y = height - 1; y > boardY; y--) {
                if (floor.GetTerrain(boardX - 1, y) == TerrainType.land) break;
                if (floor.GetTerrain(boardX + 1, y) == TerrainType.land) break;
                floor.SetTerrain(boardX, y, TerrainType.wall);
            }
        }


        //} else {
        //    var boardX = Random.Range(width * 1 / 4, width * 3 / 4);
        //    boardY = Random.Range(height * 1 / 4, height * 3 / 4);
        //    var room01 = new Room(floor, (0, 0), (width - 1, boardY - 1), Direction.down);
        //    var room02 = new Room(floor, (0, boardY), (boardX - 1, height - 1), Direction.up, Direction.right);
        //    var room03 = new Room(floor, (boardX, boardY), (width - 1, height - 1), Direction.up, Direction.left);

        //    floor.Rooms.Add(room01);
        //    floor.Rooms.Add(room02);
        //    floor.Rooms.Add(room03);

        //    for (var y= boardY; y < height; y++)
        //        floor.SetTerrain(boardX, y, TerrainType.land);

        //    for (var y = boardY; y < height; y++) {
        //        if (floor.GetTerrain(boardX - 1, y) == TerrainType.land) break;
        //        if (floor.GetTerrain(boardX + 1, y) == TerrainType.land) break;
        //        floor.SetTerrain(boardX, y, TerrainType.wall);
        //    }
        //    for (var y = height - 1; y > boardY; y--) {
        //        if (floor.GetTerrain(boardX - 1, y) == TerrainType.land) break;
        //        if (floor.GetTerrain(boardX + 1, y) == TerrainType.land) break;
        //        floor.SetTerrain(boardX, y, TerrainType.wall);
        //    }

        //}
        //}

        for (var x = 0; x < width; x++)
            floor.SetTerrain(x, boardY, TerrainType.land);

        for (var x = 0; x < width; x++) {
            var isUpperAisle = floor.GetTerrain(x, boardY - 1) == TerrainType.land;
            var isLowerAisle = floor.GetTerrain(x, boardY + 1) == TerrainType.land;
            if (isUpperAisle || isLowerAisle) break;
            floor.SetTerrain(x, boardY, TerrainType.wall);
        }
        for (var x = width - 1; x > 0; x--) {
            var isUpperAisle = floor.GetTerrain(x, boardY - 1) == TerrainType.land;
            var isLowerAisle = floor.GetTerrain(x, boardY + 1) == TerrainType.land;
            if (isUpperAisle || isLowerAisle) break;
            floor.SetTerrain(x, boardY, TerrainType.wall);
        }


        return floor;
    }
}
