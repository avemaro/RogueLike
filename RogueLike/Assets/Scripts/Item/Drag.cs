using System.Collections.Generic;
using UnityEngine;

public class Drag : Item {
    readonly int HP_MaxHP;
    readonly int HP;

    public Drag(Floor floor, Cell cell, int HP, int HP_MaxHP) {
        Floor = floor;
        Position = cell;
        this.HP = HP;
        this.HP_MaxHP = HP_MaxHP;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        if (player.HP == player.MaxHP) player.MaxHP += HP_MaxHP;
        player.HP += HP;
    }
}

public class EyewashHerb : Item {
    public EyewashHerb(Floor floor, Cell cell) {
        Floor = floor;
        Position = cell;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        foreach (var trap in player.Floor.Traps)
            trap.isVisible = true;
    }
}

public class DragonHerb : Item {
    public DragonHerb(Floor floor, Cell cell) {
        Floor = floor;
        Position = cell;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        var enemy = Floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return;
        enemy.IsAttacked(player);
    }
}