using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap: Stuff, IAttacker {
    static readonly List<char> IDs = new List<char>() { '罠', '跳', '爆', '丸' };
    public new static Trap Create(Floor floor, Cell cell, char data) {
        if (!IDs.Contains(data)) return null;
        return new Trap(floor, cell, data);
    }

    bool isStepedOn;
    public int AP { get; set; } = 20;

    private Trap(Floor floor, Cell cell, char data) {
        Image = data.ToString();
        Floor = floor;
        Position = cell;
        ID = data;
        isVisible = false;
    }

    public void Work() {
        var player = Floor.Player;

        if (!isAbleToWork()) return;

        isVisible = true;

        switch (ID) {
            case '罠': player.IsAttacked(this); return;
            case '跳':
                player.Jump();
                return;
            case '爆':
                //player.IsAttacked(this);
                foreach (var cell in Position.Around) {
                    var enemy = Floor.GetEnemy(cell);
                    if (enemy != null) enemy.IsAttacked(this);
                }
                return;
            case '丸':
                player.Fly(player.direction.Reverse());
                return;
        }
    }

    public bool Attack() {
        throw new System.NotImplementedException();
    }

    public bool IsAttacked(IAttacker attacker) {
        throw new System.NotImplementedException();
    }

    bool isAbleToWork() {
        if (Floor.Player.Position != Position) {
            isStepedOn = false;
            return false;
        }
        if (isStepedOn) return false;
        isStepedOn = true;
        return true;
    }
}