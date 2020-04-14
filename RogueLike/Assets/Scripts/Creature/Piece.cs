using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Creature {
    readonly Player player;

    public Piece(Floor floor, Cell position) {
        this.Floor = floor;
        player = floor.Player;
        Position = position;
        ID = 'Ｐ';
        HP = 10;
    }

    public override bool Move(Direction direction) {
        this.direction = direction;

        if (!IsRegalMove()) {
            Position = Position.Next(direction);
            Floor.Remove(this);
            return false;
        }

        if (!Floor.Pieces.Contains(this))
            Floor.Pieces.Add(this);

        return base.Move(direction);
    }

    protected override bool IsRegalMove() {
        var to = Position.Next(direction);
        if (to is null) return false;
        if (!IsAbleToGo(to)) return false;
        return true;
    }

    protected override bool IsBlockingCreatrue(Cell to) {
        return Floor.GetEnemy(to) != null;
    }

    public override bool Attack() {
        var to = RightFront;
        if (Floor.GetEnemy(to) != null) return Floor.GetEnemy(to).IsAttacked(this);
        to = LeftFront;
        if (Floor.GetEnemy(to) != null) return Floor.GetEnemy(to).IsAttacked(this);
        to = RightBack;
        if (Floor.GetEnemy(to) != null) return Floor.GetEnemy(to).IsAttacked(this);
        to = LeftBack;
        if (Floor.GetEnemy(to) != null) return Floor.GetEnemy(to).IsAttacked(this);
        return false;
    }

    public override bool IsAttacked(IAttacker attacker) {
        base.IsAttacked(attacker);
        if (HP > 0) return true;
        player.Pieces.Remove(this);
        return true;
    }
}
