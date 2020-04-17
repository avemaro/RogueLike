using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drag : Item {
    readonly int HP_MaxHP;
    readonly int HP;
    readonly int MaxHP;
    readonly int Satiation;
    readonly int M_MaxSatiation;
    readonly int MaxSatiation;

    public Drag(Floor floor, Cell cell, string name, params int[] specs): base(floor, cell, name)
    {
        Image = '薬';
        HP = specs[0];
        HP_MaxHP = specs[1];
        MaxHP = specs[2];
        Satiation = specs[3];
        M_MaxSatiation = specs[4];
        MaxSatiation = specs[5];
    }

    public Drag(Floor floor, Cell cell, string name, int HP, int HP_MaxHP, int MaxHP,
        int Satiation, int M_MaxSatiation, int MaxSatiation): base(floor, cell, name) {
        Image = '薬';
        this.HP = HP;
        this.HP_MaxHP = HP_MaxHP;
        this.MaxHP = MaxHP;
        this.Satiation = Satiation;
        this.M_MaxSatiation = M_MaxSatiation;
        this.MaxSatiation = MaxSatiation;
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        player.Satiation += 5;
        if (player.HP == player.MaxHP) player.MaxHP += HP_MaxHP;
        player.HP += HP;
        player.MaxHP += MaxHP;
        player.Satiation += Satiation;
        player.MaxSatiation += MaxSatiation;
    }
}

public class EyewashHerb : Drag {
    public EyewashHerb(Floor floor, Cell cell, string name): base(floor, cell, name, 0, 0, 0, 0, 0, 0) {
    }

    public override void Work(Player player) {
        player.Items.Remove(this);
        player.Satiation += 5;

        foreach (var trap in player.Floor.Traps)
            trap.isVisible = true;
    }
}

public class DragonHerb : Drag {
    public DragonHerb(Floor floor, Cell cell, string name): base(floor, cell, name, 0, 0, 0, 0, 0, 0) {
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