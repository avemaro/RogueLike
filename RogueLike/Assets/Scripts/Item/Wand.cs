using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : Item {
    protected int durability = 3;
    (State, int)[] states;

    public Wand(Floor floor, Cell cell, string name, params (State, int)[] states) : base(floor, cell, name) {
        Floor = floor;
        Position = cell;
        this.states = states;
        Image = '杖';
        Name = name;
    }

    protected override void Work(Player player, Stuff stuff) {
        if (!(stuff is Enemy)) return;
        var enemy = (Enemy)stuff;

        if (Name == "WandOfPlaceSwitching") {
            var temp = player.Position;
            player.Position = enemy.Position;
            enemy.Position = temp;
        }
        if (Name == "WandOfBlowAway")
            enemy.Fly(player.direction);
        if (Name == "WandOfUnhappiness")
            enemy.Level -= 1;
        if (Name == "WandOfTemporaryAvoid") {
            enemy.states.Add((State.Bind, 9999));
            if (Floor.GetCreature(Floor.StairPosition) == null)
                enemy.Position = Floor.StairPosition;
        }
        if (Name == "WandOfPainSharing") {
            enemy.states.Add((State.PainSharing, 9999));
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

        return;
    }
}