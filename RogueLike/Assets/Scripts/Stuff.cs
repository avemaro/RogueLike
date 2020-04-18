using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stuff {
    public char Image { get; protected set; }
    public string Name { get; protected set; }
    public virtual int Exp { get; set; }

    public Floor Floor { get; protected set; }
    public Cell Position { get; set; }
    public char ID { get; set; }
    public bool isVisible = true;

    public static Stuff Create(Floor floor, Cell cell, char data) {
        var item = Item.Create(floor, cell, data);
        if (item != null) return item;
        var enemy = Enemy.Create(floor, cell, data);
        if (enemy != null) return enemy;
        var trap = Trap.Create(floor, cell, data);
        if (trap != null) return trap;
        return null;
    }

    public void Fly(Direction direction) {
        Debug.Log("Fly to " + direction);
        var nextCell = Floor.GetTerrainCell(Position);
        while (true) {
            nextCell = nextCell.Next(direction);
            if (nextCell.type != TerrainType.land &&
                nextCell.type != TerrainType.water) break;
            Position = nextCell;
        }
    }
}
