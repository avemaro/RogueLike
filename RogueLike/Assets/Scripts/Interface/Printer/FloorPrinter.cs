﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPrinter {
    readonly Floor floor;
    int width = 11;
    int height = 9;

    public FloorPrinter(Floor floor) {
        this.floor = floor;
    }

    public string[] GetSrroundings() {
        var center = floor.Player.Position;

        List<string> floorText = new List<string>();
        for (var y = -height / 2; y < height / 2 + 1; y++) {
            var line = "";
            for (var x = -width / 2; x < width / 2 + 1; x++) {
                var position = center + (x, y);
                string data = GetString(position.x, position.y);
                line += data;
            }
            floorText.Add(line);
        }

        return floorText.ToArray();
    }

    public string GetText(int width, int height) {
        this.width = width;
        this.height = height;

        var strings = GetSrroundings();
        var text = "";
        foreach (var str in strings) {
            text += str;
            text += "\n";
        }
        return text;
    }

    string GetString(int x, int y) {
        string data = "◆";
        if (x < 0 || y < 0 |
            x >= floor.floorSize.x || y >= floor.floorSize.y) return data;
            data = floor.GetTerrain(x, y).GetString();
        if (floor.StairPosition == (x, y)) data = "階";
        var stuff = floor.GetStuff(x, y);
        if (stuff != null) {
            data = stuff.Image;
            //Debug.Log(data);
            //if (stuff is Enemy)
            //    if (((Enemy)stuff).IsFoundTarget)
            //        data = "<color=#ff0000>" + data + "</color>";

            if (!stuff.isVisible) data = "　";
        }
        if (floor.Player.Position == (x, y)) {
            switch (floor.Player.direction) {
                case Direction.up: return "┻";
                case Direction.upRight: return "┗";
                case Direction.right: return "┣";
                case Direction.downRight: return "┏";
                case Direction.down: return "┳";
                case Direction.downLeft: return "┓";
                case Direction.left: return "┫";
                case Direction.upLeft: return "┛";
            }
        }
        return data;
    }

    public List<string> GetMap() {
        var floorData = new List<string>();
        for (var y = 0; y < floor.floorSize.y; y++) {
            var str = "";
            for (var x = 0; x < floor.floorSize.x; x++)
                str += GetString(x, y);
            floorData.Add(str);
            Debug.Log(str);
        }
        return floorData;
    }
}