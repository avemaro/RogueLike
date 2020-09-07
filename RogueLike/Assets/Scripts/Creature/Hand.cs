using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand {
    readonly Player player;
    readonly Floor Floor;

    public Weapon weapon;
    public Shield shield;

    public Hand(Player player, Floor floor) {
        this.player = player;
        Floor = floor;

        weapon = new NullWeapon(floor, player.Position, "");
        shield = new NullShiled(floor, player.Position, "");
    }

    public bool Attack() {
        if (player.IsState(State.Dead)) return false;
        weapon.Attack();
        player.PassTurn();

        var effectPosition = player.Position + player.direction.GetValue();
        EffectPrinter.AddEffect(effectPosition.x, effectPosition.y, 'ド');

        return true;
    }

    public void Use(int index) {
        var item = GetItem(index);
        Use(item);
    }

    public void Put(int index) {
        var item = GetItem(index);
        if (item == null) return;

        if (weapon == item) weapon = new NullWeapon(Floor, player.Position, "");
        if (shield == item) shield = new NullShiled(Floor, player.Position, "");

        item.Put(player);
        player.PassTurn();
    }

    public void Use(Item item) {
        if (item == null) return;
        item.Work(player);
        player.PassTurn();
    }

    public void Throw(int index) {
        var item = GetItem(index);
        if (item == null) return;

        if (weapon == item) weapon = new NullWeapon(Floor, player.Position, "");
        if (shield == item) shield = new NullShiled(Floor, player.Position, "");

        item.Throw(player);
        player.PassTurn();
    }

    public void Equip(int index) {
        var item = GetItem(index);
        Equip(item);
    }

    public void Equip(Item item) {
        Debug.Log(item);

        if (item is Weapon) {
            weapon.Equip();
            if (weapon == item)
                weapon = new NullWeapon(Floor, player.Position, "");
            else {
                weapon = (Weapon)item;
                weapon.Equip();
            }
            player.PassTurn();
        }

        if (item is Shield) {
            shield.Equip();
            if (shield == item)
                shield = new NullShiled(Floor, player.Position, "");
            else {
                shield = (Shield)item;
                shield.Equip();
            }
        }

    }

    public bool PickUp() {
        var item = Floor.GetItem(player.Position.x, player.Position.y);
        Floor.Remove(item);
        if (item == null) return false;
        if (item.ID == 'Ｇ') return true;
        player.Items.Add(item);
        return true;
    }

    Item GetItem(int index) {
        if (index > player.Items.Count - 1) return null;
        return player.Items[index];
    }

}