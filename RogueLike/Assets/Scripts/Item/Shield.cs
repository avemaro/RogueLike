using UnityEngine;
using System.Collections;

public class Shield : Item
{
    public int DP { get; }
    bool isEquiped = false;

    public Shield(Floor floor, Cell cell, int DP, string name) : base(floor, cell, name)
    {
        Image = '防';
        Name = name;
        this.DP = DP;
    }

    public void Equip()
    {
        isEquiped = !isEquiped;
    }
    public override string ToString()
    {
        var appendix = "";
        if (isEquiped) appendix = "#";
        return base.ToString() + appendix;
    }
}