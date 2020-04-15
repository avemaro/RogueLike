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

    public Wand(Floor floor, Cell cell, char data) : base(floor, cell, data) {
        this.Floor = floor;
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
            enemy.states.Add((State.Bind, 9999));
        if (ID == '不')
            enemy.Level -= 1;
        if (ID == '身') {
            enemy.states.Add((State.Confusion, 50));
            enemy.states.Add((State.Scapegoat, 50));
        }
        if (ID == '一') {
            enemy.states.Add((State.Bind, 9999));
            if (Floor.GetCreature(Floor.StairPosition) == null)
                enemy.Position = Floor.StairPosition;
        }
        if (ID == '痛')
            enemy.states.Add((State.PainSharing, 9999));

    }

    public override void Work(Player player) {
        durability--;
        if (durability <= 0) player.Items.Remove(this);

        var enemy = Floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return;
        Work(player, enemy);
    }
}