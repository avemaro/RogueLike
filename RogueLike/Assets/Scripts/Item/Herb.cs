using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : Item {
    static readonly List<char> IDs = new List<char>()
    { '草', '眼', '薬'};
    public static new Herb Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Herb(floor, cell, data);
    }

    protected Herb(Floor floor, Cell cell, char data) : base(floor, cell, data) {
        this.floor = floor;
        Position = cell;
        ID = data;
    }

    protected override void Work(Player player, Stuff stuff) {
        if (ID == '眼')
            foreach (var trap in floor.Traps)
                trap.isVisible = true;

        if (ID == '草') {
            var enemy = floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
            if (enemy == null) return;
            enemy.IsAttacked(player);
        }
    }

    public override bool Use(Player player) {
        player.Items.Remove(this);
        Work(player, null);
        return true;
    }
}