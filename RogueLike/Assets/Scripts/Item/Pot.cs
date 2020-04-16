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
        Floor = floor;
        Position = cell;
        ID = data;
    }

    public Pot(Floor floor, Cell cell, string name) {
        Floor = floor;
        Position = cell;
        Image = '壺';
        Name = name;
    }

    public override void Work(Player player) {
        if (ID == 'ト') {
            var nextCell = Floor.GetTerrainCell(player.Position);

            while (true) {
                nextCell = nextCell.Next(player.direction);
                if (nextCell.type != TerrainType.land &&
                    nextCell.type != TerrainType.water) break;
                if (Floor.GetEnemy(nextCell.x, nextCell.y) != null) break;
                var item = Floor.GetItem(nextCell.x, nextCell.y);
                if (item == null) continue;
                contents.Add(item);
                Floor.Remove(item);
                break;
            }
        }
    }

    public override bool Throw(Player player) {
        player.Items.Remove(this);

        var nextCell = Floor.GetTerrainCell(player.Position);
        while (true) {
            nextCell = nextCell.Next(player.direction);

            if (nextCell.type == TerrainType.wall ||
                nextCell.type == TerrainType.breakableWall)
                break;

            contents[0].Position = nextCell;
        }
        Floor.Items.Add(contents[0]);

        return true;
    }
}