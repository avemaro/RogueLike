using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Item {
    static readonly List<char> IDs = new List<char>()
    { '杖', '縛', '吹' };
    public static new Wand Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Wand(floor, cell, data, "");
    }

    protected int durability = 3;
    (State, int)[] states;

    public Wand(Floor floor, Cell cell, char data, string name) : base(floor, cell, data) {
        Floor = floor;
        Position = cell;
        ID = data;
        Image = '杖';
        Name = name;
    }

    public Wand(Floor floor, Cell cell, string name, params (State, int)[] states) {
        Floor = floor;
        Position = cell;
        this.states = states;
        Image = '杖';
        Name = name;
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
        if (ID == '不')
            enemy.Level -= 1;
        if (ID == '一') {
            enemy.states.Add((State.Bind, 9999));
            if (Floor.GetCreature(Floor.StairPosition) == null)
                enemy.Position = Floor.StairPosition;
        }

        if (states == null) return;
        foreach (var state in states)
            enemy.states.Add(state);

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