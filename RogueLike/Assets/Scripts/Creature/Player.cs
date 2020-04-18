
using System.Collections.Generic;
using UnityEngine;

public class Player: Creature {
    public override int Level { get => base.Level;
        set {
            if (value > base.Level) MaxHP += 4;
            if (value < base.Level) MaxHP -= 4;
            base.Level = value;
        }
    }

    public override int HP {
        get => base.HP;
        set {
            if (value < base.HP)
                foreach (var enemy in Floor.Enemies)
                    if (enemy.IsState(State.PainSharing)) enemy.HP -= base.HP - value;
            base.HP = value;
        }
    }
    public int BasicAP { get {
            if (Level == 1) return 5;
            if (Level == 2) return 7;
            return 9;
        }
    }
    public int strength = 8;
    public override int AP =>
        BasicAP + Mathf.RoundToInt(BasicAP * (weapon.AP + strength - 8) / 16.0f);
    public override int DP { get => base.DP + shield.DP; }
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
    public override int Exp { get => base.Exp;
        set {
            base.Exp = value;
            if (value < 10) Level = 1;
            if (value >= 10) Level = 2;
            if (value >= 30) Level = 3;
            if (value >= 60) Level = 4;
        }
    }


    public List<Item> Items { get; private set; } = new List<Item>();
    public Weapon weapon;
    public Shield shield;

    public List<Piece> Pieces { get; private set; } = new List<Piece>();

    public Player(Floor floor) {
        Floor = floor;
        direction = Direction.down;
        MaxHP = 15;
        HP = MaxHP;
        weapon = Weapon.Create(floor, Position, '拳');
        shield = new NullShiled(floor, Position, 0, "");
        //Items.Add(ItemMaker.Create(Floor, Position, "ScrollOfWindCutter"));;
    }

    public Player(Floor floor, int MaxHP) {
        Floor = floor;
        direction = Direction.down;
        this.MaxHP = MaxHP;
        HP = MaxHP;
        weapon = Weapon.Create(floor, Position, '拳');
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
        //if (!(item is Weapon)) return;
        Debug.Log(item);

        if (item is Weapon)
        {
            weapon.Equip();
            if (weapon == item)
            {
                weapon = Weapon.Create(Floor, Position, '拳');
            }
            else
            {
                weapon = (Weapon)item;
                weapon.Equip();
            }
            PassTurn();
        }

        if (item is Shield)
        {
            shield.Equip();
            if (shield == item)
            {
                shield = new NullShiled(Floor, Position, 0, "");
            }
            else
            {
                shield = (Shield)item;
                shield.Equip();
            }
        }

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