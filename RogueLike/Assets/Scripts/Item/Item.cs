using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Stuff, IEquatable<Item>, IAttacker {
    static readonly List<char> IDs = new List<char>()
    { 'Ｇ'};
    public new static Item Create(Floor floor, Cell cell, char data) {
        var equipment = Equipment.Create(floor, cell, data);
        if (equipment != null) return equipment;
        var scroll = Scroll.Create(floor, cell, data);
        if (scroll != null) return scroll;
        var wand = Wand.Create(floor, cell, data);
        if (wand != null) return wand;
        var pot = Pot.Create(floor, cell, data);
        if (pot != null) return pot;
        var herb = Herb.Create(floor, cell, data);
        if (herb != null) return herb;

        if (!IDs.Contains(data)) return null;
        return new Item(floor, cell, data);
    }

    protected Item(Floor floor, Cell cell, char data) {
        this.floor = floor;
        Position = cell;
        ID = data;
    }

    protected virtual void Work(Player player, Stuff stuff) {

    }

    public virtual bool Use(Player player) {
        return true;
    }

    public virtual bool Throw(Player player) {
        var enemy = floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return true;
        Work(player, enemy);
        return true;
    }

    public bool Equals(Item other) {
        return base.Equals(other);
    }

    public virtual bool Attack() {
        throw new NotImplementedException();
    }

    public bool IsAttacked(IAttacker attacker) {
        throw new NotImplementedException();
    }

    public override string ToString() {
        return ID.ToString();
    }
}