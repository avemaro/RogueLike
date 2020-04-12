using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : Creature {
    public Piece(Floor floor, Cell position) {
        this.floor = floor;
        Position = position;
        ID = 'P';
    }

    public override bool Attack() {
        throw new System.NotImplementedException();
    }
}
