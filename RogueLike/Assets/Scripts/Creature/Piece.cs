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
            Position = Position.Next(direction);
            floor.Remove(this);
            return false;
        }

        if (!floor.Pieces.Contains(this))
            floor.Pieces.Add(this);

        return base.Move(direction);
    }

    public override bool Attack() {
        throw new System.NotImplementedException();
    }
}
