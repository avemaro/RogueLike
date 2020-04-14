using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Stuff, IAttacker {
    public Direction direction;
    public State state;

    public int Level {
        get { return level; }
        set { level = value;
            if (level < 1) level = 1;
        }
    }
    int level = 1;

    public int MaxHP = 1;
    public int HP {
        get { return hp; }
        set { hp = value;
            if (hp > MaxHP) hp = MaxHP;
            if (hp <= 0) {
                state = State.Dead;
                Floor.Remove(this);
            }
        }
    }
    int hp = 1;

    public Room Room { get { return Floor.GetRoom(Position); } }
    public Cell Front { get { return Position.Next(direction); } }
    public Cell Back { get { return Position.Next(direction.Reverse()); } }
    public Cell RightFront { get { return Position.Next(direction.TurnRight()); } }
    public Cell LeftFront { get { return Position.Next(direction.TurnLeft()); } }
    public Cell RightBack { get { return Position.Next(direction.TurnLeft().Reverse()); } }
    public Cell LeftBack { get { return Position.Next(direction.TurnRight().Reverse()); } }

    protected List<TerrainType> blockingTerrain = new List<TerrainType>()
    { TerrainType.wall, TerrainType.water, TerrainType.breakableWall };

    public abstract bool Attack();

    public virtual bool IsAttacked(IAttacker attacker) {
        HP--;
        return true;
    }

    public virtual bool Move(Direction direction) {
        if (state == State.Dead) return false;

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

    public void Fly(Direction direction) {
        var nextCell = Floor.GetTerrainCell(Position);
        while (true) {
            nextCell = nextCell.Next(direction);
            if (nextCell.type != TerrainType.land &&
                nextCell.type != TerrainType.water) break;
            Position = nextCell;
        }
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
