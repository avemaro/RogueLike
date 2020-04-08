using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMaker {
    static int width = 30;
    static int height = 30;

public static Floor Create(int roomCount) {
        var data = new List<string>();
        for (var y = 0; y < height; y++) {
            var line = "";
            for (var x = 0; x < width; x++) {
                if (x == 2 && y == 1) line += "試";
                else if (x == 3 && y == 2) line += "階";
                else line += "◆";
            }
            data.Add(line);
        }

        return new Floor(data.ToArray());;
    }

    //public static Floor Create(string[] data) {
    //    var floor = new Floor(data);
    //    floor.Rooms.Add(new Room((2, 1), (8, 9)));
    //    floor.Rooms.Add(new Room((15, 1), (22, 9)));

    //    for (var y = 0; y < floor.floorSize.y; y++) {
    //        for (var x = 0; x < floor.floorSize.x; x++) {
    //            var cell = floor.GetTerrainCell(new Cell(x, y));
    //            if (cell.type == TerrainType.wall ||
    //                cell.type == TerrainType.breakableWall) continue;
    //            if (cell.Next(Direction.upLeft))
    //        }
    //    }

    //    return floor;
    //}

    //public static Floor Create() {
        
    //}
}
