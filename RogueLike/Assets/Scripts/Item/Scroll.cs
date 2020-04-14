﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : Item {
    static readonly List<char> IDs = new List<char>()
    { '巻', '眠', '真' };
    public static new Scroll Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Scroll(floor, cell, data);
    }

    protected Scroll(Floor floor, Cell cell, char data): base(floor, cell, data) {
        this.Floor = floor;
        Position = cell;
        ID = data;
    }

    protected override void Work(Player player, Stuff stuff) {
        var enemy = (Enemy)stuff;
        if (ID == '眠')
            enemy.states.Add((State.Sleep, 5));
        if (ID == '真')
            enemy.IsAttacked(this);
    }

    public override void Work(Player player) {
        player.Items.Remove(this);

        foreach (var enemy in Floor.GetEnemies(player.Room))
            Work(player, enemy);
    }
}