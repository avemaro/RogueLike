using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Item {
    readonly List<Item> contents = new List<Item>();

    public Pot(Floor floor, Cell cell, string name): base(floor, cell, name) {
        Image = "壺";
    }

    public override void Work(Player player) {
        if (Name == "PotOfStealSeal") {
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
        return;
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