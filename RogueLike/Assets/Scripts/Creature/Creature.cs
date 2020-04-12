using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : Stuff, IAttacker {
    public Direction direction;
    public State state;
    public int HP = 1;
    public Room Room { get { return floor.GetRoom(Position); } }

    protected List<TerrainType> canGo = new List<TerrainType>() { TerrainType.land };

    public abstract bool Attack();

    public virtual bool IsAttacked(IAttacker attacker) {
        HP--;
        if (HP <= 0) state = State.Dead;
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

    protected bool IsRegalMove() {
        var to = Position.Next(direction);
        if (to is null) return false;
        if (!IsAbleToGo(to)) return false;

        if (!direction.IsDiagonal()) return true;

        var forwards = Position.Next(direction.Forwards());
        if (floor.GetTerrain(forwards).Contains(TerrainType.wall)) return false;

        return true;
    }

    protected virtual bool IsAbleToGo(Cell to) {
        if (floor.GetTerrainCell(to) is null) return false;
        if (!canGo.Contains(floor.GetTerrain(to))) return false;
        if (floor.GetCreature(to) != null) return false;
        return true;
    }

    public void Fly(Direction direction) {
        var nextCell = floor.GetTerrainCell(Position);
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
            var x = Random.Range(0, floor.floorSize.x);
            var y = Random.Range(0, floor.floorSize.y);
            to = new Cell(x, y);
            if (IsAbleToGo(to)) break;
        }
        Position = to;
    }
}
