using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : Creature {
    static readonly List<char> IDs = new List<char>() { 'マ', 'ギ', '武', 'ク' };
    public new static Enemy Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        if (data == 'ク') return new Bowboy(floor, cell, data);
        return new Enemy(floor, cell, data);
    }

    public Brain Brain { get; private set; }
    public bool IsFoundTarget { get => Floor.Player.Position == Brain.Destination; }

    public override string Image { get {
            if (IsFoundTarget) return "<color=#ff0000>" + ID.ToString() + "</color>";
            return ID.ToString();
        } }

    protected Enemy(Floor floor, Cell cell, char data) {
        //Image = "敵";
        Floor = floor;
        floor.Enemies.Add(this);
        Position = cell;
        ID = data;
        Brain = new Brain(floor, this);
    }

    public Enemy(Floor floor, Cell cell, string name, char ID, params int[] spec) {
        this.ID = ID;
        Floor = floor;
        floor.Enemies.Add(this);
        Position = cell;
        Brain = new Brain(floor, this);

        MaxHP = spec[0];
        HP = spec[0];
        Exp = spec[1];
        AP = spec[2];
        DP = spec[3];
    }

    public void Work() {
        if (IsState(State.Confusion)) {
            var directions = DirectionExtend.AllCases();
            var toward = directions[Random.Range(0, directions.Count())];
            direction = toward;
            if (IsRegalMove()) Move(direction);
        } else
        {
            Brain.Work();
        }

        for (var i = 0; i < states.Count; i++)
        {
            states[i] = (states[i].Item1, states[i].Item2 - 1);
        }
        states.RemoveAll(state => state.Item2 <= 0);
    }

    public override bool Attack() {
        foreach (var direction in DirectionExtend.AllCases()) {
            var to = Position.Next(direction);
            //Debug.Log(to);

            if (direction.IsDiagonal()) {
                var forwards = Position.Next(direction.Forwards());
                if (Floor.GetTerrain(forwards).Contains(TerrainType.wall))
                    continue;
            }

            if (to == Brain.Target.Position) {
                //Debug.Log("EnemyAttack");
                return Brain.Target.IsAttacked(this);
            }
        }
       return false;
    }
}

public class Bowboy : Enemy {
    public Bowboy(Floor floor, Cell cell, char data): base(floor, cell, data) {
    }

    public override bool Attack() {
        var nextCell = Position;
        while (true) {
            nextCell = nextCell.Next(direction);
            if (nextCell == Floor.Player.Position) return Floor.Player.IsAttacked(this);
            if (Floor.GetTerrain(nextCell) == TerrainType.wall) return false;
        }
    }
}