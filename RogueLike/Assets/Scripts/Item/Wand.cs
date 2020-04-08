using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Item {
    static readonly List<char> IDs = new List<char>()
    { '杖', '縛', '吹' };
    public static new Wand Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Wand(floor, cell, data);
    }

    protected int durability = 3;

    protected Wand(Floor floor, Cell cell, char data) : base(floor, cell, data) {
        this.floor = floor;
        Position = cell;
        ID = data;
    }

    protected override void Work(Player player, Stuff stuff) {
        if (!(stuff is Enemy)) return;
        var enemy = (Enemy)stuff;

        if (ID == '杖') {
            var temp = player.Position;
            player.Position = enemy.Position;
            enemy.Position = temp;
        }
        if (ID == '吹')
            enemy.Fly(player.direction);
        if (ID == '縛')
            enemy.state = State.Sleep;
    }

    public override bool Use(Player player) {
        durability--;
        if (durability <= 0) player.Items.Remove(this);

        var enemy = floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return true;
        Work(player, enemy);

        return true;
    }
}