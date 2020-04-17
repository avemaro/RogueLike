
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public override int HP {
        get => base.HP;
        set {
            if (value < base.HP)
                foreach (var enemy in Floor.Enemies)
                    if (enemy.IsState(State.PainSharing)) enemy.HP -= base.HP - value;
            base.HP = value;
        }
    }

    public int BasicAP { get; protected set; } = 5;
    public int strength = 8;
    public override int AP =>
        BasicAP + Mathf.RoundToInt(BasicAP * (weapon.AP + strength - 8) / 16.0f);

    public int MaxSatiation {
        get { return maxSatiation; }
        set {
            maxSatiation = value;
            if (maxSatiation > 200) maxSatiation = 200;
        }
    }
    int maxSatiation = 100;
    public int Satiation {
        get { return Mathf.CeilToInt(satiation/10.0f); }
        set {
            satiation += value * 10;
            if (satiation > maxSatiation * 10) satiation = maxSatiation * 10;
        }
    }
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
        Floor = floor;
        direction = Direction.down;
        this.MaxHP = MaxHP;
        HP = MaxHP;
        weapon = Equipment.Create(floor, Position, '拳');
    }

    void PassTurn() {
        if (Satiation > 0) satiation--;
        else HP--;

        for (var i = 0; i < states.Count; i++)
            states[i] = (states[i].Item1, states[i].Item2 - 1);
        states.RemoveAll(state => state.Item2 <= 0);

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
        if (IsState(State.Dead)) return false;
        Debug.Log("ATTACK");
        Debug.Log(weapon.Name);
        weapon.Attack();
        foreach (var piece in Floor.Pieces)
            piece.Attack();
        PassTurn();
        return true;
    }

    public void Use(int index) {
        var item = GetItem(index);
        Use(item);
    }

    public void Use(Item item) {
        if (item == null) return;
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
        Equip(item);
    }

    public void Equip(Item item) {
        if (!(item is Equipment)) return;
        Debug.Log(item);
        weapon.Equip();
        if (weapon == item) {
            weapon = Equipment.Create(Floor, Position, '拳');
        } else {
            weapon = (Equipment)item;
            weapon.Equip();
        }
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