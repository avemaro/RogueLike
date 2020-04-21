using System.Collections.Generic;
using UnityEngine;

public static class ItemMaker {
    static readonly (ItemType type, int prob)[] items = {
        (ItemType.weapon, 24), (ItemType.shield, 25), (ItemType.arrow, 12),
        (ItemType.food, 13), (ItemType.bracelet, 8),
        (ItemType.scroll, 63), (ItemType.wand, 20), (ItemType.drag, 68), (ItemType.pot, 23)
    };

    public static Item PopItem(Floor floor, Cell cell) {
        var rand = Random.Range(0, 256);
        var accum = 0;
        ItemType type = ItemType.drag;
        foreach (var (name, prob) in items) {
            accum += prob;
            if (rand < accum) {
                type = name;
                break;
            }
        }
        switch (type) {
            case ItemType.weapon: return SelectItem(floor, cell, ItemData.weapons);
            case ItemType.shield: return SelectItem(floor, cell, ItemData.shields);
            case ItemType.arrow: return SelectItem(floor, cell, ItemData.arrows);
            case ItemType.food: return SelectItem(floor, cell, ItemData.foods);
            case ItemType.bracelet: return SelectItem(floor, cell, ItemData.bracelets);
            case ItemType.scroll: return SelectItem(floor, cell, ItemData.scrolls);
            case ItemType.wand: return SelectItem(floor, cell, ItemData.wands);
            case ItemType.drag: return SelectItem(floor, cell, ItemData.drags);
            default: return SelectItem(floor, cell, ItemData.pots);
        }
    }

    static Item SelectItem(Floor floor, Cell cell, List<(string name, int prob)> probs) {
        var rand = Random.Range(0, 256);
        var accum = 0;
        foreach (var (name, prob) in probs) {
            accum += prob;
            if (rand < accum) return Create(floor, cell, name);
        }
        throw new System.Exception(probs[0].name + probs[0].prob);
    }

    public static Item Create(Floor floor, Cell cell, string name) {
        if (name == "EyewashHerb") return new EyewashHerb(floor, cell, name);
        if (name == "DragonHerb") return new DragonHerb(floor, cell, name);

        if (name == "WandOfScapegoat") return new Wand(floor, cell, name, (State.Confusion, 50), (State.Scapegoat, 50));
        if (name == "WandOfBinding") return new Wand(floor, cell, name, (State.Bind, 9999));
        if (name == "WandOfPainSharing") return new Wand(floor, cell, name, (State.PainSharing, 9999));

        if (name == "Kamaitachi")
            return new Weapon(floor, cell, 3, name, Direction.upLeft, Direction.upRight);
        if (name == "PickAxe")
            return new PickAxe(floor, cell, 1, name);

        var data = ItemData.GetData(name);
        switch (data.Type) {
            case ItemType.weapon:
                return new Weapon(floor, cell, data.Spec[0], name);
            case ItemType.shield:
                return new Shield(floor, cell, data.Spec[1], name);
            case ItemType.arrow:
                return new Arrow(floor, cell, data.Spec[0], name); ;
            case ItemType.food:
                return new Drag(floor, cell, name, data.Spec);
            case ItemType.bracelet:
                return new Bracelet(floor, cell, name);
            case ItemType.scroll:
                return new Scroll(floor, cell, name); ;
            case ItemType.wand:
                return new Wand(floor, cell, name);
            case ItemType.drag:
                return new Drag(floor, cell, name, data.Spec);
            case ItemType.pot:
                return new Pot(floor, cell, name);
            default:
                break;
        }
        


        throw new System.Exception(name);
    }

    public static Item Create(string name) {
        var floor = FloorMaker.Create();
        var cell = new Cell(0, 0);
        return Create(floor, cell, name);
    }
}