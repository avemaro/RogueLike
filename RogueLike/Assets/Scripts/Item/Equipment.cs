using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {
    static readonly List<char> IDs = new List<char>() { '拳', 'つ', '透' };
    public new static Equipment Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Equipment(floor, cell, data);
    }

    public int AP { get; private set; } = 0;
    List<Direction> directions = new List<Direction>();

    protected Equipment(Floor floor, Cell cell, char data): base(floor, cell, data) {
        Floor = floor;
        Position = cell;
        ID = data;
    }

    public Equipment(Floor floor, Cell cell, int AP, params Direction[] directions) {
        Floor = floor;
        Position = cell;
        this.AP = AP;
        this.directions = new List<Direction>(directions);
    }

    bool isEquiped = false;

    public void Equip() {
        isEquiped = true;
    }

    public override bool Attack() {
        var player = Floor.Player;

        if (directions.Count != 0) {
            directions = new List<Direction> {
                player.direction,
                player.direction.TurnLeft(),
                player.direction.TurnRight()
            };
        } else
            directions.Add(player.direction);
        foreach (var direction in directions)
            Debug.Log(direction);

        var cells = player.Position.Next(directions.ToArray());

        foreach (var to in cells) {
            var enemy = Floor.GetEnemy(to);
            if (enemy != null) enemy.IsAttacked(this);

            var cell = Floor.GetTerrainCell(to);
            if (!(cell is null)) cell.IsAttacked(this);
        }

        return true;
    }

    public override string ToString() {
        var appendix = "";
        if (isEquiped) appendix = "E";
        return base.ToString() + appendix;
    }
}
