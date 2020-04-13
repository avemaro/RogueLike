
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public int Satiation { get { return (int)satiation; } }
    float satiation = 100f;

    public List<Item> Items { get; private set; } = new List<Item>();
    public Equipment weapon;

    public List<Piece> Pieces { get; private set; } = new List<Piece>();

    public Player(Floor floor) {
        this.floor = floor;
        direction = Direction.down;
        HP = 10;
        weapon = Equipment.Create(floor, Position, '拳');
    }

    void PassTurn() {
        satiation -= 0.1f;
        floor.Work();
    }

    public override bool Move(Direction direction) {
        if (!base.Move(direction)) return false;
        PickUp();
        foreach (var piece in Pieces)
            piece.Move(direction);
        PassTurn();
        return true;
    }

    public override bool Attack() {
        if (state == State.Dead) return false;
        Debug.Log("ATTACK");
        weapon.Attack();
        foreach (var piece in floor.Pieces)
            piece.Attack();
        PassTurn();
        return true;
    }

    public void Use(int index) {
        var item = GetItem(index);
        if (item == null) return;
        item.Use(this);
        PassTurn();
    }

    public void Throw(int index) {
        var item = GetItem(index);
        if (item == null) return;
        item.Throw(this);
        PassTurn();
    }

    public void Equip(int index) {
        var item = GetItem(index);
        if (!(item is Equipment)) return;
        weapon = (Equipment)item;
        weapon.Equip();
        PassTurn();
    }

    bool PickUp() {
        var item = floor.GetItem(Position.x, Position.y);
        floor.Remove(item);
        if (item == null) return false;
        if (item.ID == 'Ｇ') return true;
        Items.Add(item);
        return true;
    }

    Item GetItem(int index) {
        if (index > Items.Count - 1) return null;
        return Items[index];
    }

    protected override bool IsBlockingCreatrue(Cell to) {
        return floor.GetEnemy(to) != null;
    }

    public Piece Spawn(Chess type) {
        var piece = new Piece(floor, Front);
        if (!piece.IsAbleToGo(Front)) return null;
        Pieces.Add(piece);
        floor.Pieces.Add(piece);
        return piece;
    }

    public void StorePieces() {
        Pieces = new List<Piece>();
    }
}