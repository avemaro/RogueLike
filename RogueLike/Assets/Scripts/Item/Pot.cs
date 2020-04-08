using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Item {
    static readonly List<char> IDs = new List<char>()
    { 'ト' };
    public static new Pot Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Pot(floor, cell, data);
    }

    readonly List<Item> contents = new List<Item>();

    protected Pot(Floor floor, Cell cell, char data) : base(floor, cell, data) {
        this.floor = floor;
        Position = cell;
        ID = data;
    }

    public override bool Use(Player player) {
        if (ID == 'ト') {
            var nextCell = floor.GetTerrainCell(player.Position);

            while (true) {
                nextCell = nextCell.Next(player.direction);
                if (nextCell.type != TerrainType.land &&
                    nextCell.type != TerrainType.water) break;
                if (floor.GetEnemy(nextCell.x, nextCell.y) != null) break;
                var item = floor.GetItem(nextCell.x, nextCell.y);
                if (item == null) continue;
                contents.Add(item);
                floor.Remove(item);
                break;
            }
        }
        return true;
    }

    public override bool Throw(Player player) {
        player.Items.Remove(this);

        var nextCell = floor.GetTerrainCell(player.Position);
        while (true) {
            nextCell = nextCell.Next(player.direction);

            if (nextCell.type == TerrainType.wall ||
                nextCell.type == TerrainType.breakableWall)
                break;

            contents[0].Position = nextCell;
        }
        floor.Items.Add(contents[0]);

        return true;
    }
}