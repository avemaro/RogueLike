﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Item {
    protected bool isEquiped = false;

    public void Equip()
    {
        isEquiped = !isEquiped;
    }

    protected Equipment(Floor floor, Cell cell, char data) : base(floor, cell, data)
    {}

    public Equipment(Floor floor, Cell cell, string name) : base(floor, cell, name)
    {}

    public override string ToString() {
        var appendix = "";
        if (isEquiped) appendix = "#";
        return base.ToString() + appendix;
    }

    public override bool Throw(Player player) {
        isEquiped = false;
        return base.Throw(player);
    }
}