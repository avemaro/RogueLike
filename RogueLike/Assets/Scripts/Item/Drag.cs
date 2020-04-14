﻿using System.Collections.Generic;
using UnityEngine;

public class Drag : Item {
    readonly int HP_MaxHP;
    readonly int HP;
    readonly int MaxHP;
    readonly int MaxSatiation;

    public Drag(Floor floor, Cell cell, int HP, int HP_MaxHP, int MaxHP, int MaxSatiation) {
        Floor = floor;
        Position = cell;
        this.HP = HP;
        this.HP_MaxHP = HP_MaxHP;
        this.MaxHP = MaxHP;
        this.MaxSatiation = MaxSatiation;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        player.Satiation += 5;
        if (player.HP == player.MaxHP) player.MaxHP += HP_MaxHP;
        player.HP += HP;
        player.MaxHP += MaxHP;
        player.MaxSatiation += MaxSatiation;
    }
}

public class EyewashHerb : Item {
    public EyewashHerb(Floor floor, Cell cell) {
        Floor = floor;
        Position = cell;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);
        player.Satiation += 5;

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
        player.Satiation += 5;

        var enemy = Floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return;
        enemy.IsAttacked(player);
    }
}