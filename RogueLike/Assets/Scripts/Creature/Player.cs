﻿
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public int Satiation { get { return Mathf.CeilToInt(satiation/10.0f); } }
    int satiation = 1000;

    public List<Item> Items { get; private set; } = new List<Item>();
    public Equipment weapon;

    public List<Piece> Pieces { get; private set; } = new List<Piece>();

    public Player(Floor floor) {
        this.Floor = floor;
        direction = Direction.down;
        MaxHP = 10;
        HP = 10;
        weapon = Equipment.Create(floor, Position, '拳');
    }

    public Player(Floor floor, int MaxHP) {
        this.Floor = floor;
        direction = Direction.down;
        this.MaxHP = MaxHP;
        HP = MaxHP;
        weapon = Equipment.Create(floor, Position, '拳');
    }

    void PassTurn() {
        if (Satiation > 0) satiation--;
        else HP--;
        Floor.Work();
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
        foreach (var piece in Floor.Pieces)
            piece.Attack();
        PassTurn();
        return true;
    }

    public void Use(int index) {
        var item = GetItem(index);
        if (item == null) return;
        item.Work(this);
        PassTurn();
    }

    public void Use(Item item) {
        item.Work(this);
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
        var item = Floor.GetItem(Position.x, Position.y);
        Floor.Remove(item);
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
        return Floor.GetEnemy(to) != null;
    }

    public Piece Spawn(Chess type) {
        var piece = new Piece(Floor, Front);
        if (!piece.IsAbleToGo(Front)) return null;
        Pieces.Add(piece);
        Floor.Pieces.Add(piece);
        return piece;
    }

    public void StorePieces() {
        Pieces = new List<Piece>();
    }
}