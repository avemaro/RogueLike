using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPrinter {
    readonly Floor floor;
    readonly int width = 11;
    readonly int height = 9;

    public FloorPrinter(Floor floor) {
        this.floor = floor;
    }

    public string[] GetStrings() {
        var center = floor.Player.Position;

        List<string> floorText = new List<string>();
        for (var y = -height / 2; y < height / 2 + 1; y++) {
            var line = "";
            for (var x = -width / 2; x < width / 2 + 1; x++) {
                var position = center + (x, y);
                char data = GetChar(position.x, position.y);
                line += data;
            }
            floorText.Add(line);
            //Debug.Log(line);
        }

        return floorText.ToArray();
    }

    public string GetText() {
        var strings = GetStrings();
        var text = "";
        foreach (var str in strings) {
            text += str;
            text += "\n";
        }
        return text;
    }

    char GetChar(int x, int y) {
        char data = '◆';
        if (x > 0 && y > 0 &&
            x < floor.floorSize.x && y < floor.floorSize.y) {
            data = (char)floor.GetTerrain(x, y);
            if (floor.StairPosition == (x, y)) data = '階';
            var stuff = floor.GetStuff(x, y);
            if (stuff != null) {
                data = stuff.ID;
                if (!stuff.isVisible) data = '　';
                if (stuff is Enemy) {
                    if (((Enemy)stuff).state == State.Dead) data = '　';
                }
            }
            if (floor.Player.Position == (x, y)) data = '試';
        }
        return data;
    }

    public List<string> GetString() {
        var floorData = new List<string>();
        for (var y = 0; y < floor.floorSize.y; y++) {
            var str = "";
            for (var x = 0; x < floor.floorSize.x; x++) {
                char data = (char)floor.GetTerrain(x, y);

                if (floor.StairPosition == (x, y)) data = '階';
                var stuff = floor.GetStuff(x, y);
                if (stuff != null) {
                    data = stuff.ID;
                    if (!stuff.isVisible) data = '　';
                    if (stuff is Enemy) {
                        if (((Enemy)stuff).state == State.Dead) data = '　';
                    }
                }
                if (floor.Player.Position == (x, y)) data = '試';
                str += data;
            }
            floorData.Add(str);
            Debug.Log(str);
        }
        return floorData;
    }
}