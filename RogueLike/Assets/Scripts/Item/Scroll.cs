using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : Item {
    public Scroll(Floor floor, Cell cell, string name): base(floor, cell, name) {
        Image = '巻';
        AP = 10;
    }

    protected override void Work(Player player, Stuff stuff) {
        var enemy = (Enemy)stuff;
        if (Name == "ScrollOfDeepSleep**")
            enemy.states.Add((State.Sleep, 5));
        if (Name == "ScrollOfWindCutter")
            enemy.IsAttacked(this);
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        foreach (var enemy in Floor.GetEnemies(player.Room))
            Work(player, enemy);
    }
}