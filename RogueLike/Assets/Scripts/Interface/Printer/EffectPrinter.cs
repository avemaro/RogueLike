using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPrinter {
    readonly Floor floor;
    int width = 11;
    int height = 9;

    public EffectPrinter(Floor floor) {
        this.floor = floor;
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

    public string[] GetSrroundings() {
        var center = floor.Player.Position;

        List<string> effectText = new List<string>();
        for (var y = -height / 2; y < height / 2 + 1; y++) {
            var line = "";
            for (var x = -width / 2; x < width / 2 + 1; x++) {
                var position = center + (x, y);
                char data = GetChar(position.x, position.y);
                line += data;
            }
            effectText.Add(line);
        }

        return effectText.ToArray();
    }

    char GetChar(int x, int y) {
        char data = '　';
        if (x < 0 || y < 0 |
            x >= floor.floorSize.x || y >= floor.floorSize.y) return data;
        foreach (var effect in effects)
            if (effect.x == x && effect.y == y) return effect.data;

        return data;
    }

    static List<(int x, int y, char data)> effects = new List<(int x, int y, char data)>();

    public static void AddEffect(int x, int y, char data) {
        effects.Add((x, y, data));
    }

}
