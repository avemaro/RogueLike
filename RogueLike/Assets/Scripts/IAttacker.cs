using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttacker {
    char ID { get; set; }

    bool Attack();
    bool IsAttacked(IAttacker attacker);
}