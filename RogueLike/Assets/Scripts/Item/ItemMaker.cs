using System;
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
        
        if (name == "WandOfBlowAway") return new Wand(floor, cell, '吹');
        if (name == "WandOfUnhappiness") return new Wand(floor, cell, '不');
        if (name == "WandOfScapegoat") return new Wand(floor, cell, (State.Confusion, 50), (State.Scapegoat, 50));
        if (name == "WandOfPlaceSwitching") return new Wand(floor, cell, '杖');
        if (name == "WandOfBinding") return new Wand(floor, cell, (State.Bind, 9999));
        if (name == "WandOfTemporaryAvoid") return new Wand(floor, cell, '一');
        if (name == "WandOfPainSharing") return new Wand(floor, cell, (State.PainSharing, 9999));

        if (name == "SpikedClub") return new Equipment(floor, cell, 2);
        if (name == "Glaive") return new Equipment(floor, cell, 4);
        if (name == "Katana") return new Equipment(floor, cell, 6);
        if (name == "Doutanuki") return new Equipment(floor, cell, 8);
        if (name == "Kamaitachi")
            return new Equipment(floor, cell, 3, Direction.upLeft, Direction.upRight);

        return null;
    }

    public static Item Create(string name) {
        var floor = FloorMaker.Create();
        var cell = new Cell(0, 0);
        return Create(floor, cell, name);
    }
}