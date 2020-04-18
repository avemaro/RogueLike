using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain {
    readonly Floor floor;
    readonly Enemy enemy;
    public Creature Target { get; private set; }
    public Cell Destination { get; private set; }

    public Brain(Floor floor, Enemy enemy) {
        this.floor = floor;
        this.enemy = enemy;
    }

    public void Work() {
        if (enemy.IsState(State.Dead)) return;
        if (enemy.IsState(State.Sleep)) return;
        if (enemy.IsState(State.Bind)) return;

        SetDestination();
        if (Destination is null) return;

        var difference = Destination - enemy.Position;
        enemy.direction = difference.Direction;
        if (enemy.Attack()) return;
        if (enemy.ID == 'マ' || enemy.ID == 'ギ') return;
        DecideMove();
    }

    void SetDestination() {
        Target = floor.Player;
        foreach (var enemy in floor.Enemies)
            if (enemy.IsState(State.Scapegoat)) Target = enemy;

        if (enemy.Room is null) return;
        if (enemy.Room != Target.Room) return;
        Destination = Target.Position;
    }

    void DecideMove() {
        //Debug.Log("FindWay");
        var difference = Destination - enemy.Position;
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