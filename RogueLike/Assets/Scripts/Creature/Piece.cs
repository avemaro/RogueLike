using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Creature {
    public Piece(Floor floor, Cell position) {
        this.floor = floor;
        Position = position;
        ID = 'P';
    }

    public override bool Move(Direction direction) {
        this.direction = direction;

        if (IsRegalMove()) {
            if (!floor.Pieces.Contains(this))
                floor.Pieces.Add(this);
            base.Move(direction);
            return true;
        }
        Position = Position.Next(direction);
        floor.Remove(this);
        return false;
    }

    public override bool Attack() {
        throw new System.NotImplementedException();
    }
}
