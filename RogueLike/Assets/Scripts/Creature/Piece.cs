using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Creature {
    public Piece(Floor floor, Cell position) {
        this.floor = floor;
        Position = position;
        ID = 'P';
        HP = 10;
    }

    public override bool Move(Direction direction) {
        this.direction = direction;

        if (!IsRegalMove()) {
            Debug.Log("NotRegal");
            Position = Position.Next(direction);
            floor.Remove(this);
            return false;
        }

        if (!floor.Pieces.Contains(this))
            floor.Pieces.Add(this);

        return base.Move(direction);
    }

    protected override bool IsRegalMove() {
        var to = Position.Next(direction);
        if (to is null) return false;
        if (!IsAbleToGo(to)) return false;

        return true;
    }

    protected override bool IsAbleToGo(Cell to) {
        if (floor.GetTerrainCell(to) is null) return false;
        if (!canGo.Contains(floor.GetTerrain(to))) return false;
        if (floor.GetEnemy(to) != null) return false;
        Debug.Log("IsAbleToGo");
        return true;
    }

    public override bool Attack() {
        var to = RightFront;
        if (floor.GetEnemy(to) != null) return floor.GetEnemy(to).IsAttacked(this);
        to = LeftFront;
        if (floor.GetEnemy(to) != null) return floor.GetEnemy(to).IsAttacked(this);
        to = RightBack;
        if (floor.GetEnemy(to) != null) return floor.GetEnemy(to).IsAttacked(this);
        to = LeftBack;
        if (floor.GetEnemy(to) != null) return floor.GetEnemy(to).IsAttacked(this);
        return false;
    }
}
