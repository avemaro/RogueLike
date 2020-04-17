using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bracelet: Equipment
{
    public Bracelet(Floor floor, Cell cell, string name) : base(floor, cell, name)
    {
        Image = '腕';
    }
}