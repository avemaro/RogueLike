using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Stuff, IEquatable<Item>, IAttacker {
    static readonly List<char> IDs = new List<char>()
    { 'Ｇ'};
    public new static Item Create(Floor floor, Cell cell, char data) {
        if (data == '草') return ItemMaker.Create(floor, cell, "DragonHerb");
        if (data == '眼') return ItemMaker.Create(floor, cell, "EyewashHerb");
        if (data == '薬') return ItemMaker.Create(floor, cell, "MedicinalHerb");
        if (data == '吹') return ItemMaker.Create(floor, cell, "WandOfBlowAway");
        if (data == '杖') return ItemMaker.Create(floor, cell, "WandOfPlaceSwitching");
        if (data == '縛') return ItemMaker.Create(floor, cell, "WandOfBinding");
        if (data == '杖') return ItemMaker.Create(floor, cell, "WandOfPlaceSwitching");
        if (data == '縛') return ItemMaker.Create(floor, cell, "WandOfBinding");
        if (data == '吹') return ItemMaker.Create(floor, cell, "WandOfBlowAway");

        if (data == '巻') return ItemMaker.Create(floor, cell, "ScrollOfIdentify**");
        if (data == '眠') return ItemMaker.Create(floor, cell, "ScrollOfDeepSleep**");
        if (data == '真') return ItemMaker.Create(floor, cell, "ScrollOfWindCutter**");

        if (data == 'ト') return ItemMaker.Create(floor, cell, "PotOfStealSeal");

        var equipment = Equipment.Create(floor, cell, data);
        if (equipment != null) return equipment;

        if (!IDs.Contains(data)) return null;
        return new Item(floor, cell, data);
    }

    protected Item(Floor floor, Cell cell, char data) {
        Floor = floor;
        Position = cell;
        ID = data;
    }

    public Item(Floor floor, Cell cell, string name) {
        Floor = floor;
        Position = cell;
        Name = name;
    }

    protected virtual void Work(Player player, Stuff stuff) {

    }

    public virtual void Work(Player player) {
        return;
    }

    public virtual bool Throw(Player player) {
        var enemy = Floor.GetEnemy(player.Position, player.direction,
            new List<TerrainType>() { TerrainType.wall, TerrainType.breakableWall });
        if (enemy == null) return true;
        Work(player, enemy);
        return true;
    }

    public bool Equals(Item other) {
        return base.Equals(other);
    }

    public virtual bool Attack() {
        throw new NotImplementedException();
    }

    public bool IsAttacked(IAttacker attacker) {
        throw new NotImplementedException();
    }

    public override string ToString(){
        return Name;
    }
}