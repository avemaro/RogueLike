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

    readonly Brain brain;

    protected Enemy(Floor floor, Cell cell, char data) {
        Floor = floor;
        floor.Enemies.Add(this);
        Position = cell;
        ID = data;
        brain = new Brain(floor, this);
    }

    public void Work() {
        if (IsState(State.Confusion)) {
            var directions = DirectionExtend.AllCases();
            var toward = directions[Random.Range(0, directions.Count())];
            direction = toward;
            if (IsRegalMove()) Move(direction);
        } else
        {
            brain.Work();
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
            if (to == brain.Target.Position) return brain.Target.IsAttacked(this);
            if (Floor.GetPiece(to) != null) return Floor.GetPiece(to).IsAttacked(this);
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