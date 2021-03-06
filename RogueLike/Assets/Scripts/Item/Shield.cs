﻿using UnityEngine;
using System.Collections;

public class Shield : Equipment
{
    public int DP { get; }

    public Shield(Floor floor, Cell cell, int DP, string name) : base(floor, cell, name)
    {
        Image = "防";
        this.DP = DP;
    }

}

public class NullShiled: Shield
{
    public NullShiled(Floor floor, Cell cell, string name) : base(floor, cell, 0, name)
    {
    }
}