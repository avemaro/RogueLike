using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData {
    static public (string name, int prob)[] weapons = {
        ("SpikedClub", 72), ("Glaive", 57), ("Katana", 44), ("Doutanuki", 14),
        ("SacredSickle", 9), ("Kamaitachi", 8), ("PickAxe", 43), ("DrainBuster", 9)
    };
    static public (string name, int prob)[] shields = {
        ("LeatherShield", 28), ("BronzeShield", 86),
        ("WoodenShield", 42), ("IronShield", 43),
        ("HeavilyArmedShield", 29), ("ThiefSealShield", 28)
    };
    static public (string name, int prob)[] arrows = {
        ("WoodArrow**", 256)
    };
    static public (string name, int prob)[] foods = {
        ("RiceBall", 102), ("BigRiceBall", 31), ("RottenRiceBall", 103),
        ("HugeRiceBall", 20)
    };
    static public (string name, int prob)[] bracelets = {
        ("BraceletOfDiscount**", 23), ("BraceletOfRustProof**", 47),
        ("BraceletOfCurseProof**", 46), ("BraceletOfLongThrow**", 47),
        ("BraceletOfClairvoyance**", 46), ("BraceletOfConfusionProof**", 47),
    };
    static public (string name, int prob)[] scrolls = {
        ("ScrollOfIdentify**", 72), ("ScrollOfLight**", 30),
        ("ScrollOfPotEnlarging**", 11), ("ScrollOfWindCutter", 30),
        ("ScrollOfEmergency**", 21), ("ScrollOfDeepSleep", 20),
        ("ScrollOfPowerUp**", 21), ("ScrollOfBigRoom**", 10),
        ("ScrollOfConfusion", 21), ("ScrollOfWhitePaper**", 20),
    };
    static public (string name, int prob)[] wands = {
        ("WandOfBlowAway", 51), ("WandOfUnhappiness", 26),
        ("WandOfScapegoat", 25), ("WandOfPlaceSwitching", 52),
        ("WandOfBinding", 51), ("WandOfTemporaryAvoid", 25),
        ("WandOfPainSharing", 26)
    };
    static public (string name, int prob)[] drags = {
        ("MedicinalHerb", 51), ("OtogiriHerb", 51),
        ("LifeHerb", 25), ("StomachEnlargingSeed", 26),
        ("EyewashHerb", 64), ("DragonHerb", 39),
    };
    static public (string name, int prob)[] pots = {
        ("PotOfStorage**", 81), ("PotOfHideout**", 24),
        ("PotOfIdentify**", 35), ("PotOfBackMassage**", 35),
        ("PotOfStoreroom**", 23), ("PotOfConversion**", 23),
        ("PotOfSynthesys**", 12), ("PotOfStealSeal", 23),
    };

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
        //Debug.Log(found);
        return new ItemData(found.type, found.name, found.spec);
    }

    #region InitData
    static void InitWeapon() {
        AddData(ItemType.weapon, "SpikedClub", 2);
        AddData(ItemType.weapon, "Glaive", 4);
        AddData(ItemType.weapon, "Katana", 6);
        AddData(ItemType.weapon, "Doutanuki", 8);
        AddData(ItemType.weapon, "SacredSickle", 4);
        AddData(ItemType.weapon, "DrainBuster", 5);
    }
    static void InitShield() {
        AddData(ItemType.shield, "LeatherShield", 2);
        AddData(ItemType.shield, "BronzeShield", 4);
        AddData(ItemType.shield, "WoodenShield", 3);
        AddData(ItemType.shield, "IronShield", 7);
        AddData(ItemType.shield, "HeavilyArmedShield", 10);
        AddData(ItemType.shield, "ThiefSealShield", 3);
    }
    static void InitBracelet() {
        AddData(ItemType.bracelet, "BraceletOfDiscount**");
        AddData(ItemType.bracelet, "BraceletOfRustProof**");
        AddData(ItemType.bracelet, "BraceletOfCurseProof**");
        AddData(ItemType.bracelet, "BraceletOfLongThrow**");
        AddData(ItemType.bracelet, "BraceletOfClairvoyance**");
        AddData(ItemType.bracelet, "BraceletOfConfusionProof**");
    }
    static void InitDrag() {
        AddData(ItemType.drag, "MedicinalHerb", 25, 1, 0, 0, 0, 0);
        AddData(ItemType.drag, "OtogiriHerb", 100, 2, 0, 0, 0, 0);
        AddData(ItemType.drag, "LifeHerb", 0, 0, 5, 0, 0, 0);
        AddData(ItemType.drag, "StomachEnlargingSeed", 0, 0, 0, 0, 0, 10);
    }
    static void InitFood() {
        AddData(ItemType.food, "RiceBall", 0, 0, 0, 50, 1, 0);
        AddData(ItemType.food, "BigRiceBall", 0, 0, 0, 100, 2, 0);
        AddData(ItemType.food, "RottenRiceBall", 0, 0, 0, 30, 0, 0);
        AddData(ItemType.food, "HugeRiceBall", 0, 0, 0, 999, 0, 5);
    }
    static void InitWand() {
        AddData(ItemType.wand, "WandOfBlowAway");
        AddData(ItemType.wand, "WandOfUnhappiness");
        AddData(ItemType.wand, "WandOfPlaceSwitching");
        AddData(ItemType.wand, "WandOfTemporaryAvoid");
    }
    static void InitArrow() {
        AddData(ItemType.arrow, "WoodArrow**", 5);
    }
    static void InitScroll() {
        AddData(ItemType.scroll, "ScrollOfIdentify**");
        AddData(ItemType.scroll, "ScrollOfLight**");
        AddData(ItemType.scroll, "ScrollOfPotEnlarging**");
        AddData(ItemType.scroll, "ScrollOfWindCutter");
        AddData(ItemType.scroll, "ScrollOfEmergency**");
        AddData(ItemType.scroll, "ScrollOfDeepSleep");
        AddData(ItemType.scroll, "ScrollOfPowerUp**");
        AddData(ItemType.scroll, "ScrollOfBigRoom**");
        AddData(ItemType.scroll, "ScrollOfConfusion");
        AddData(ItemType.scroll, "ScrollOfWhitePaper**");
    }
    static void InitPot() {
        AddData(ItemType.pot, "PotOfStorage**");
        AddData(ItemType.pot, "PotOfHideout**");
        AddData(ItemType.pot, "PotOfIdentify**");
        AddData(ItemType.pot, "PotOfBackMassage**");
        AddData(ItemType.pot, "PotOfStoreroom**");
        AddData(ItemType.pot, "PotOfConversion**");
        AddData(ItemType.pot, "PotOfSynthesys**");
        AddData(ItemType.pot, "PotOfStealSeal");
    }

    public static void InitData() {
        InitWeapon();
        InitShield();
        InitBracelet();
        InitDrag();
        InitFood();
        InitWand();
        InitArrow();
        InitScroll();
        InitPot();
    }
    #endregion


    public static void AddData(ItemType type, string name, params int[] spec) {
        data.Add((type, name, spec));
    }
}

public enum ItemType {
    none, weapon, shield, bracelet, arrow, food, scroll, wand, drag, pot
}