using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData {
    public ItemType Type { get; private set; }
    public string Name { get; private set; }
    public int[] Spec;

    private ItemData(ItemType type, string name, params int[] spec) {
        Type = type;
        Name = name;
        Spec = spec;
    }

    static List<(ItemType type, string name, int[] spec)> data = new List<(ItemType type, string name, int[] spec)>();

    public static ItemData GetData(string name) {
        var found = data.Find(d => d.name == name);
        return new ItemData(found.type, found.name, found.spec);
    }

    public static void InitData() {
        AddData(ItemType.weapon, "SpikedClub", 2);
        AddData(ItemType.weapon, "Glaive", 4);
        AddData(ItemType.weapon, "Katana", 6);
        AddData(ItemType.weapon, "Doutanuki", 8);
        AddData(ItemType.weapon, "SacredSickle", 4);
        AddData(ItemType.weapon, "DrainBuster", 5);

        AddData(ItemType.shield, "LeatherShield", 2);
        AddData(ItemType.shield, "BronzeShield", 4);
        AddData(ItemType.shield, "WoodenShield", 3);
        AddData(ItemType.shield, "IronShield", 7);
        AddData(ItemType.shield, "HeavilyArmedShield", 10);
        AddData(ItemType.shield, "ThiefSealShield", 3);

        AddData(ItemType.drag, "MedicinalHerb", 25, 1, 0, 0, 0, 0);
        AddData(ItemType.drag, "OtogiriHerb", 100, 2, 0, 0, 0, 0);
        AddData(ItemType.drag, "LifeHerb", 0, 0, 5, 0, 0, 0);
        AddData(ItemType.drag, "StomachEnlargingSeed", 0, 0, 0, 0, 0, 10);

        AddData(ItemType.riceBall, "RiceBall", 0, 0, 0, 50, 1, 0);
        AddData(ItemType.riceBall, "BigRiceBall", 0, 0, 0, 100, 2, 0);
        AddData(ItemType.riceBall, "RottenRiceBall", 0, 0, 0, 30, 0, 0);
        AddData(ItemType.riceBall, "HugeRiceBall", 0, 0, 0, 999, 0, 5);

        AddData(ItemType.wand, "WandOfBlowAway");
        AddData(ItemType.wand, "WandOfUnhappiness");
        AddData(ItemType.wand, "WandOfPlaceSwitching");
        AddData(ItemType.wand, "WandOfTemporaryAvoid");

        AddData(ItemType.arrow, "WoodArrow**", 5);

        AddData(ItemType.scroll, "ScrollOfIdentify**");
        AddData(ItemType.scroll, "ScrollOfLight**");
        AddData(ItemType.scroll, "ScrollOfPotEnlarging**");
        AddData(ItemType.scroll, "ScrollOfWindCutter");
        AddData(ItemType.scroll, "ScrollOfEmergency**");
        AddData(ItemType.scroll, "ScrollOfDeepSleep**");
        AddData(ItemType.scroll, "ScrollOfPowerUp**");
        AddData(ItemType.scroll, "ScrollOfBigRoom**");
        AddData(ItemType.scroll, "ScrollOfConfusion**");
        AddData(ItemType.scroll, "ScrollOfWhitePaper**");

        AddData(ItemType.pot, "PotOfStorage**");
        AddData(ItemType.pot, "PotOfHideout**");
        AddData(ItemType.pot, "PotOfIdentify**");
        AddData(ItemType.pot, "PotOfBackMassage**");
        AddData(ItemType.pot, "PotOfStoreroom**");
        AddData(ItemType.pot, "PotOfConversion**");
        AddData(ItemType.pot, "PotOfSynthesys**");
        AddData(ItemType.pot, "PotOfStealSeal");
    }

    public static void AddData(ItemType type, string name, params int[] spec) {
        data.Add((type, name, spec));
    }
}

public enum ItemType {
    none, weapon, shield, arrow, riceBall, bracelet, scroll, wand, drag, pot
}