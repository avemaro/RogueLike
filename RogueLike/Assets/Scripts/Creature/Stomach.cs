using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach {
    public int MaxSatiation {
        get { return maxSatiation; }
        set {
            maxSatiation = value;
            if (maxSatiation > 200) maxSatiation = 200;
        }
    }
    int maxSatiation = 100;
    public int Satiation {
        get { return Mathf.CeilToInt(satiation / 10.0f); }
        set {
            satiation = value * 10;
            if (satiation > maxSatiation * 10) satiation = maxSatiation * 10;
        }
    }
    int satiation = 1000;

    public void PassTurn(Player player) {
        if (satiation > 0) {
            satiation--;
            if (player.HP < player.MaxHP) player.HP++;
        } else player.HP--;
    }
}