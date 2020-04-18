using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {
    public Enemy enemy;
    [SerializeField] char ID;
    [SerializeField] int HP;
    [SerializeField] int AP;
    [SerializeField] int DP;
    [SerializeField] string destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        ID = enemy.ID;
        HP = enemy.HP;
        AP = enemy.AP;
        DP = enemy.DP;
        var dest = enemy.Brain.Destination;
        if (dest is null) return;
        Debug.Log(dest);
        destination = "x: " + dest.x.ToString() + ", y: " + dest.y.ToString();
        if (enemy.IsState(State.Dead)) Destroy(gameObject);
    }
}
