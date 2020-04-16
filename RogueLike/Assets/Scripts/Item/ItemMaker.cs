using UnityEngine;

public static class ItemMaker {
    static readonly (string name, int prob)[] items = {
        ("weapon", 24), ("shield", 25), ("arrow", 12), ("riceBall", 13), ("bracelet", 8),
        ("scroll", 63), ("wand", 20), ("herb", 68), ("pot", 23)
    };

    static readonly (string name, int prob)[] weapons = {
        ("SpikedClub", 72), ("Glaive", 57), ("Katana", 44), ("Doutanuki", 14),
        ("SacredSickle", 9), ("Kamaitachi", 8), ("PickAxe", 43), ("DrainBuster", 9)
    };
    static readonly (string name, int prob)[] shield = {
        ("LeatherShield**", 28), ("BronzeShield**", 86),
        ("WoodenShield**", 42), ("IronShield**", 43),
        ("HeavilyArmedShield**", 29), ("ThiefSealShield**", 28)
    };
    static readonly (string name, int prob)[] arrows = {
        ("WoodArrow**", 256)
    };
    static readonly (string name, int prob)[] riceBall = {
        ("RiceBall**", 102), ("BigRiceBall**", 31), ("RottenRiceBall**", 103),
        ("HugeRiceBall**", 20)
    };
    static readonly (string name, int prob)[] bracelet = {
        ("BraceletOfDiscount**", 23), ("BraceletOfRustProof**", 47),
        ("BraceletOfCurseProof**", 46), ("BraceletOfLongThrow**", 47),
        ("BraceletOfClairvoyance**", 46), ("BraceletOfConfusionProof**", 47),
    };
    static readonly (string name, int prob)[] scroll = {
        ("ScrollOfIdentify**", 72), ("ScrollOfLight**", 30),
        ("ScrollOfPotEnlarging**", 11), ("ScrollOfWindCutter**", 30),
        ("ScrollOfEmergency**", 21), ("ScrollOfDeepSleep**", 20),
        ("ScrollOfPowerUp**", 21), ("ScrollOfBigRoom**", 10),
        ("ScrollOfConfusion**", 21), ("ScrollOfWhitePaper**", 20),
    };
    static readonly (string name, int prob)[] wand = {
        ("WandOfBlowAway", 51), ("WandOfUnhappiness", 26),
        ("WandOfScapegoat", 25), ("WandOfPlaceSwitching", 52),
        ("WandOfBinding", 51), ("WandOfTemporaryAvoid", 25),
        ("WandOfPainSharing", 26)
    };
    static readonly (string name, int prob)[] herb = {
        ("MedicinalHerb", 51), ("OtogiriHerb", 51),
        ("LifeHerb", 25), ("StomachEnlargingSeed", 26),
        ("EyewashHerb", 64), ("DragonHerb", 39),
    };
    static readonly (string name, int prob)[] pot = {
        ("PotOfStorage**", 81), ("PotOfHideout**", 24),
        ("PotOfIdentify**", 35), ("PotOfBackMassage**", 35),
        ("PotOfStoreroom**", 23), ("PotOfConversion**", 23),
        ("PotOfSynthesys**", 12), ("PotOfStealSeal**", 23),
    };

    public static Item PopItem(Floor floor, Cell cell) {
        var rand = Random.Range(0, 256);
        var accum = 0;
        var type = "";
        foreach (var (name, prob) in items) {
            accum += prob;
            if (rand < accum) {
                type = name;
                break;
            }
        }
        Debug.Log(rand);
        Debug.Log(type);
        switch (type) {
            case "weapon": return SelectItem(floor, cell, weapons);
            case "shield": return SelectItem(floor, cell, shield);
            case "arrow": return SelectItem(floor, cell, arrows);
            case "riceBall": return SelectItem(floor, cell, riceBall);
            case "bracelet": return SelectItem(floor, cell, bracelet);
            case "scroll": return SelectItem(floor, cell, scroll);
            case "wand": return SelectItem(floor, cell, wand);
            case "herb": return SelectItem(floor, cell, herb);
            default: return SelectItem(floor, cell, pot);
        }
    }

    static Item SelectItem(Floor floor, Cell cell, (string naem, int prob)[] probs) {
        var rand = Random.Range(0, 256);
        var accum = 0;
        foreach (var (name, prob) in probs) {
            accum += prob;
            if (rand < accum) return Create(floor, cell, name);
        }
        throw new System.Exception();
    }

    public static Item Create(Floor floor, Cell cell, string name) {
        if (name == "MedicinalHerb") return new Drag(floor, cell, 25, 1, 0, 0, name);
        if (name == "OtogiriHerb") return new Drag(floor, cell, 100, 2, 0, 0, name);
        if (name == "LifeHerb") return new Drag(floor, cell, 0, 0, 5, 0, name);
        if (name == "StomachEnlargingSeed") return new Drag(floor, cell, 0, 0, 0, 10, name);
        if (name == "EyewashHerb") return new EyewashHerb(floor, cell, name);
        if (name == "DragonHerb") return new DragonHerb(floor, cell, name);
        
        if (name == "WandOfBlowAway") return new Wand(floor, cell, '吹', name);
        if (name == "WandOfUnhappiness") return new Wand(floor, cell, '不', name);
        if (name == "WandOfScapegoat") return new Wand(floor, cell, name, (State.Confusion, 50), (State.Scapegoat, 50));
        if (name == "WandOfPlaceSwitching") return new Wand(floor, cell, '杖', name);
        if (name == "WandOfBinding") return new Wand(floor, cell, name, (State.Bind, 9999));
        if (name == "WandOfTemporaryAvoid") return new Wand(floor, cell, '一', name);
        if (name == "WandOfPainSharing") return new Wand(floor, cell, name, (State.PainSharing, 9999));

        if (name == "SpikedClub") return new Equipment(floor, cell, 2, name);
        if (name == "Glaive") return new Equipment(floor, cell, 4, name);
        if (name == "Katana") return new Equipment(floor, cell, 6, name);
        if (name == "Doutanuki") return new Equipment(floor, cell, 8, name);
        if (name == "SacredSickle") return new Equipment(floor, cell, 4, name);
        if (name == "Kamaitachi")
            return new Equipment(floor, cell, 3, name, Direction.upLeft, Direction.upRight);
        if (name == "PickAxe")
            return new PickAxe(floor, cell, 1, name);
        if (name == "DrainBuster") return new Equipment(floor, cell, 5, name);

        if (name == "WoodArrow**") return new Arrow(floor, cell, 5, name);

        if (name == "LeatherShield**") return new Equipment(floor, cell, 0, name);
        if (name == "BronzeShield**") return new Equipment(floor, cell, 0, name);
        if (name == "WoodenShield**") return new Equipment(floor, cell, 0, name);
        if (name == "IronShield**") return new Equipment(floor, cell, 0, name);
        if (name == "HeavilyArmedShield**") return new Equipment(floor, cell, 0, name);
        if (name == "ThiefSealShield**") return new Equipment(floor, cell, 0, name);

        if (name == "RiceBall**") return new Drag(floor, cell, 0, 0, 0, 0, name);
        if (name == "BigRiceBall**") return new Drag(floor, cell, 0, 0, 0, 0, name);
        if (name == "RottenRiceBall**") return new Drag(floor, cell, 0, 0, 0, 0, name);
        if (name == "HugeRiceBall**") return new Drag(floor, cell, 0, 0, 0, 0, name);


        if (name == "ScrollOfIdentify**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfLight**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfPotEnlarging**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfWindCutter**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfEmergency**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfDeepSleep**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfPowerUp**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfBigRoom**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfConfusion**") return new Scroll(floor, cell, name);
        if (name == "ScrollOfWhitePaper**") return new Scroll(floor, cell, name);

        if (name == "PotOfStorage**") return new Pot(floor, cell, name);
        if (name == "PotOfHideout**") return new Pot(floor, cell, name);
        if (name == "PotOfIdentify**") return new Pot(floor, cell, name);
        if (name == "PotOfBackMassage**") return new Pot(floor, cell, name);
        if (name == "PotOfStoreroom**") return new Pot(floor, cell, name);
        if (name == "PotOfConversion**") return new Pot(floor, cell, name);
        if (name == "PotOfSynthesys**") return new Pot(floor, cell, name);
        if (name == "PotOfStealSeal**") return new Pot(floor, cell, name);

        if (name == "BraceletOfDiscount**") return new Equipment(floor, cell, 0, name);
        if (name == "BraceletOfRustProof**") return new Equipment(floor, cell, 0, name);
        if (name == "BraceletOfCurseProof**") return new Equipment(floor, cell, 0, name);
        if (name == "BraceletOfLongThrow**") return new Equipment(floor, cell, 0,  name);
        if (name == "BraceletOfClairvoyance**") return new Equipment(floor, cell, 0, name);
        if (name == "BraceletOfConfusionProof**") return new Equipment(floor, cell, 0, name);

        throw new System.Exception(name);
    }

    public static Item Create(string name) {
        var floor = FloorMaker.Create();
        var cell = new Cell(0, 0);
        return Create(floor, cell, name);
    }
}