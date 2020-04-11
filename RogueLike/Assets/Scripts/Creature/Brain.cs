using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain {
    readonly Floor floor;
    readonly Enemy enemy;
    Cell destination;

    public Brain(Floor floor, Enemy enemy) {
        this.floor = floor;
        this.enemy = enemy;
    }

    public void Work() {
        if (enemy.state != State.Alive) return;

        SetDestination();
        if (destination is null) return;

        var difference = destination - enemy.Position;
        enemy.direction = difference.Direction;
        if (enemy.Attack()) return;
        if (enemy.ID == 'マ' || enemy.ID == 'ギ') return;
        DecideMove();
    }

    void SetDestination() {
        var room = floor.GetRoom(enemy.Position);
        var playerRoom = floor.GetRoom(floor.Player.Position);

        if (room != playerRoom) return;
        destination = floor.Player.Position;
    }

    void DecideMove() {
        //Debug.Log("FindWay");
        var difference = destination - enemy.Position;
        var direction = difference.Direction;
        //Debug.Log(direction);
        if (enemy.Move(direction)) return;

        if (!direction.IsDiagonal()) {
            if (enemy.Move(enemy.direction.TurnLeft())) return;
            if (enemy.Move(enemy.direction.TurnRight())) return;
        }

        //Debug.Log("x: " + difference.x + ", y: " + difference.y);

        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y)) {
            if (difference.x > 0 && enemy.Move(Direction.right)) return;
            if (difference.y > 0 && enemy.Move(Direction.down)) return;
            if (enemy.Move(Direction.left)) return;
            if (enemy.Move(Direction.up)) return;
        } else {
            if (difference.y > 0 && enemy.Move(Direction.down)) return;
            if (difference.x > 0 && enemy.Move(Direction.right)) return;
            if (enemy.Move(Direction.up)) return;
            if (enemy.Move(Direction.left)) return;
        }

    }

}