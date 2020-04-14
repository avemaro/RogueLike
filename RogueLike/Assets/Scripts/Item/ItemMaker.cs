﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemMaker {
    public static Item Create(Floor floor, Cell cell, string name) {
        if (name == "MedicinalHerb") return new Drag(floor, cell, 25, 1, 0, 0);
        if (name == "OtogiriHerb") return new Drag(floor, cell, 100, 2, 0, 0);
        if (name == "LifeHerb") return new Drag(floor, cell, 0, 0, 5, 0);
        if (name == "StomachEnlargingSeed") return new Drag(floor, cell, 0, 0, 0, 10);
        if (name == "EyewashHerb") return new EyewashHerb(floor, cell);
        if (name == "DragonHerb") return new DragonHerb(floor, cell);

        if (name == "WandOfBlowAway") return Wand.Create(floor, cell, '吹');
        if (name == "WandOfUnhappiness") return Wand.Create(floor, cell, '不');

        return null;
    }

    public static Item Create(string name) {
        var floor = FloorMaker.Create();
        var cell = new Cell(0, 0);
        return Create(floor, cell, name);
    }
}