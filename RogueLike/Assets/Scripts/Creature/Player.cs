
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public List<Item> Items { get; private set; } = new List<Item>();
    public Equipment weapon;
    public Cell Front { get { return Position.Next(direction); } }

    public Player(Floor floor) {
        this.floor = floor;
        direction = Direction.down;
        HP = 10;
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
        Debug.Log("ATTACK");
        weapon.Attack();
        floor.Work();
        return true;
    }

    bool PickUp() {
        var item = floor.GetItem(Position.x, Position.y);
        floor.Remove(item);
        if (item == null) return false;
        if (item.ID == 'Ｇ') return true;
        Items.Add(item);
        return true;
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
        weapon.Equip();
    }

    Item GetItem(int index) {
        if (index > Items.Count - 1) return null;
        return Items[index];
    }

    public void Spawn(Chess type) {
        var piece = new Piece(floor, Front);
        floor.Pieces.Add(piece);
    }
}