using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Stuff, IAttacker {
    public Direction direction;
    public List<(State, int)> states = new List<(State, int)>();

    public virtual int Level {
        get { return level; }
        set { level = value;
            if (level < 1) level = 1;
        }
    }
    int level = 1;

    public virtual int MaxHP { get; set; } = 1;

    public virtual int HP {
        get { return Mathf.CeilToInt(hp/10.0f); }
        set { hp = value * 10;
            if (hp > MaxHP * 10) hp = MaxHP * 10;
            if (hp <= 0) {
                states.Add((State.Dead, 99));
            }
        }
    }
    protected int hp = 10;
    public virtual int AP { get; set; } = 1;
    public virtual int DP { get; set; } = 0;

    public Room Room { get { return Floor.GetRoom(Position); } }
    public Cell Front { get { return Position.Next(direction); } }
    public Cell Back { get { return Position.Next(direction.Reverse()); } }
    public Cell RightFront { get { return Position.Next(direction.TurnRight()); } }
    public Cell LeftFront { get { return Position.Next(direction.TurnLeft()); } }
    public Cell RightBack { get { return Position.Next(direction.TurnLeft().Reverse()); } }
    public Cell LeftBack { get { return Position.Next(direction.TurnRight().Reverse()); } }


    protected List<TerrainType> blockingTerrain = new List<TerrainType>()
    { TerrainType.wall, TerrainType.water, TerrainType.breakableWall };

    public bool IsState(State state) {
        foreach (var aState in states)
            if (aState.Item1 == state) return true;
        return false;
    }

    public abstract bool Attack();

    public virtual bool IsAttacked(IAttacker attacker) {
        var damage = attacker.AP * Mathf.Pow(15.0f / 16.0f, DP);
        HP -= Mathf.FloorToInt(damage);
        if (HP <= 0) attacker.Exp += Exp;
        return true;
    }

    public virtual bool Move(Direction direction) {
        if (IsState(State.Dead)) return false;

        this.direction = direction;

        if (!IsRegalMove()) return false;
        Position = Position.Next(direction);

        return true;
    }

    public bool Move(List<Direction> directions) {
        var hasMoved = true;
        foreach (var direction in directions)
            if (!Move(direction)) hasMoved = false;
        return hasMoved;
    }

    public bool Move(params int[] indexes) {
        var directions = DirectionExtend.GetDirections(indexes);
        return Move(directions);
    }

    protected virtual bool IsRegalMove() {
        var to = Position.Next(direction);
        if (to is null) return false;
        if (!IsAbleToGo(to)) return false;

        if (!direction.IsDiagonal()) return true;

        var forwards = Position.Next(direction.Forwards());
        if (Floor.GetTerrain(forwards).Contains(TerrainType.wall)) return false;

        return true;
    }

    public virtual bool IsAbleToGo(Cell to) {
        if (Floor.GetTerrainCell(to) is null) return false;
        if (blockingTerrain.Contains(Floor.GetTerrain(to))) return false;
        if (IsBlockingCreatrue(to)) return false;
        return true;
    }

    protected virtual bool IsBlockingCreatrue(Cell to) {
        return Floor.GetCreature(to) != null;
    }

    public void Jump() {
        Cell to = Position;
        for (var i = 0; i < 100; i++) {
            var x = Random.Range(0, Floor.floorSize.x);
            var y = Random.Range(0, Floor.floorSize.y);
            to = new Cell(x, y);
            if (IsAbleToGo(to)) break;
        }
        Position = to;
    }
}
