
using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature {
    public override int Level { get => base.Level;
        set {
            if (value > base.Level) {
                MaxHP += 4;
                BasicAP += 2;
            }
            if (value < base.Level) {
                MaxHP -= 4;
                BasicAP -= 2;
            }
            base.Level = value;
        }
    }
    public override int Exp {
        get => base.Exp;
        set {
            base.Exp = value;
            Level = value / 20 + 1;
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
    public int BasicAP { get; private set; } = 5;
    public int strength = 8;
    public override int AP =>
        BasicAP + Mathf.RoundToInt(BasicAP * (hand.weapon.AP + strength - 8) / 16.0f);
    public override int DP { get => base.DP + hand.shield.DP; }

    readonly Stomach stomach = new Stomach();
    public int MaxSatiation {
        get => stomach.MaxSatiation; set => stomach.MaxSatiation = value;
    }
    public int Satiation {
        get => stomach.Satiation; set => stomach.Satiation = value;
    }

    public List<Item> Items { get; private set; } = new List<Item>();

    public Hand hand;

    public Player(Floor floor, int MaxHP = 15) {
        Floor = floor;
        direction = Direction.down;
        this.MaxHP = MaxHP;
        HP = MaxHP;

        hand = new Hand(this, floor);
    }

    public void PassTurn() {
        stomach.PassTurn(this);

        for (var i = 0; i < states.Count; i++)
            states[i] = (states[i].Item1, states[i].Item2 - 1);
        states.RemoveAll(state => state.Item2 <= 0);

        Floor.Work();
    }

    public void Look(Direction direction) {
        this.direction = direction;
    }

    public override bool Move(Direction direction) {
        if (!base.Move(direction)) return false;
        hand.PickUp();

        PassTurn();
        return true;
    }

    public override bool Attack() { return hand.Attack(); }
    public void Use(int index) { hand.Use(index); }
    public void Use(Item item) { hand.Use(item); }
    public void Throw(int index) { hand.Use(index); }
    public void Put(int index) { hand.Put(index); }
    public void Equip(int index) { hand.Equip(index); }
}