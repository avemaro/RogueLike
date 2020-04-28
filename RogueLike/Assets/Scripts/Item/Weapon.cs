using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Equipment {
    static readonly List<char> IDs = new List<char>() { '拳' };
    public new static Weapon Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Weapon(floor, cell, data);
    }

    protected List<Direction> directions = new List<Direction>();

    protected Weapon(Floor floor, Cell cell, char data): base(floor, cell, data) {
        Floor = floor;
        Position = cell;
        ID = data;
        Name = "拳";
    }

    public Weapon(Floor floor, Cell cell, int AP, string name, params Direction[] directions): base(floor, cell, name) {
        Image = "武";
        Name = name;
        this.AP = AP;
        this.directions = new List<Direction>(directions);
    }



    public override bool Attack() {
        var player = Floor.Player;

        var towards = new List<Direction>();
        if (directions.Count != 0) {
            towards = new List<Direction>(player.direction.Forwards());
        } else {
            towards.Add(player.direction);

            if (player.direction.IsDiagonal()) {
                var forwards = player.Position.Next(player.direction.Forwards());
                if (Floor.GetTerrain(forwards).Contains(TerrainType.wall))
                    return false;
            }

        }

        var cells = player.Position.Next(towards.ToArray());

        foreach (var to in cells) {
            var enemy = Floor.GetEnemy(to);
            if (enemy != null) enemy.IsAttacked(player);

            var cell = Floor.GetTerrainCell(to);
            if (!(cell is null)) cell.IsAttacked(player);
        }

        return true;
    }
}

public class NullWeapon : Weapon {
    public NullWeapon(Floor floor, Cell cell, string name) : base(floor, cell, 0, name) {
    }
}

public class PickAxe : Weapon {
    public PickAxe(Floor floor, Cell cell, int AP, string name, params Direction[] directions) : base(floor, cell, AP, name, directions) {
        Image = "武";
        Name = name;
        ID = 'つ';
    }

    public override bool Attack() {
        var to = Floor.Player.Front;
        if (Floor.GetTerrain(to) == TerrainType.wall)
            Floor.SetTerrain(to.x, to.y, TerrainType.land);

        return base.Attack();
    }
}

public class Arrow : Weapon {
    public Arrow(Floor floor, Cell cell, int AP, string name, params Direction[] directions) : base(floor, cell, AP, name, directions) {
        ID = '矢';
        Image = "矢";
        Name = name;
    }

    public override bool Attack() {
        return false;
    }
}