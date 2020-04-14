using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {
    static readonly List<char> IDs = new List<char>() { '拳', 'つ', '透' };
    public new static Equipment Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Equipment(floor, cell, data);
    }

    protected Equipment(Floor floor, Cell cell, char data): base(floor, cell, data) {
        this.Floor = floor;
        Position = cell;
        ID = data;
    }

    bool isEquiped = false;

    public void Equip() {
        isEquiped = true;
    }

    public override bool Attack() {
        var player = Floor.Player;
        var to = player.Position.Next(player.direction);

        var enemy = Floor.GetEnemy(to);
        if (enemy != null) return enemy.IsAttacked(this);

        var cell = Floor.GetTerrainCell(to);
        if (cell is null) return false;
        return cell.IsAttacked(this);
    }

    public override string ToString() {
        var appendix = "";
        if (isEquiped) appendix = "E";
        return base.ToString() + appendix;
    }
}
