using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCell : Cell {
    readonly Floor floor;
    public TerrainType type;

    public TerrainCell(Floor floor, int x, int y, TerrainType type): base(x, y){
        this.floor = floor;
        this.type = type;
    }

    public TerrainCell(Floor floor, Cell cell, TerrainType type) : base(cell.x, cell.y) {
        this.floor = floor;
        this.type = type;
    }

    new public List<TerrainCell> Around {
        get {
            var cells = new List<TerrainCell>();
            foreach (var direction in DirectionExtend.AllCases())
                cells.Add(Next(direction));
            return cells;
        }
    }
    new public TerrainCell Next(Direction direction) {
        var next = base.Next(direction);
        return floor.GetTerrainCell(next);
    }

    public bool IsAttacked(IAttacker attacker) {
        var hasAttacked = false;
        if (type == TerrainType.breakableWall) {
            type = TerrainType.land;
            hasAttacked = true;
            foreach (var cell in Around) {
                if (cell.type != TerrainType.breakableWall) continue;
                cell.IsAttacked(attacker);
            }
        }
        if (hasAttacked) return true;

        if (attacker.ID == 'つ') {
            if (type == TerrainType.wall) {
                type = TerrainType.land;
                hasAttacked = true;
            }
        }
        return hasAttacked;
    }
}