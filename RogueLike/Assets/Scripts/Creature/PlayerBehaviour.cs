using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public GameManager gameManager;
    Player player;

    [SerializeField] int HP;
    [SerializeField] int AP;
    [SerializeField] int DP;
    [SerializeField] int Satiation;

    // Start is called before the first frame update
    void Start() {
        player = gameManager.floor.Player;
    }

    // Update is called once per frame
    void Update() {
        HP = player.HP;
        AP = player.AP;
        DP = player.DP;
        Satiation = player.Satiation;
    }
}
