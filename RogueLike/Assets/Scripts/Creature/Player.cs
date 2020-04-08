
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public List<Item> Items { get; private set; } = new List<Item>();
    public Equipment weapon;
    public Room Room {
        get {
            return floor.GetRoom(Position);
        }
    }

    public Player(Floor floor) {
        this.floor = floor;
        weapon = Equipment.Create(floor, Position, '拳');
    }

    public override bool Move(Direction direction) {
        if (!base.Move(direction)) return false;
        PickUp();
        floor.Work();
        return true;
    }

    public override bool Attack() {
        if (state == State.Dead) return false;
        return weapon.Attack();
    }

    bool PickUp() {
        var item = floor.GetItem(Position.x, Position.y);
        floor.Remove(item);
        if (item != null) Items.Add(item);
        return item != null;
    }

    public void Use(int index) {
        var item = GetItem(index);
        if (item == null) return;
        item.Use(this);
        floor.Work();
    }

    public void Throw(int index) {
        var item = GetItem(index);
        if (item == null) return;
        item.Throw(this);
        floor.Work();
    }

    public void Equip(int index) {
        var item = GetItem(index);
        if (!(item is Equipment)) return;
        weapon = (Equipment)item;
    }

    Item GetItem(int index) {
        if (index > Items.Count - 1) return null;
        return Items[index];
    }
}