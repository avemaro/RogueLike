using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public GameManager gameManager;
    Player player;

    [SerializeField] int Level;
    [SerializeField] int Exp;
    [SerializeField] string HP;
    [SerializeField] int AP;
    [SerializeField] int DP;
    [SerializeField] string Satiation;

    // Start is called before the first frame update
    void Start() {
        player = gameManager.floor.Player;
    }

    // Update is called once per frame
    void Update() {
        Level = player.Level;
        Exp = player.Exp;
        HP = player.HP.ToString() + " / " + player.MaxHP.ToString();
        AP = player.AP;
        DP = player.DP;
        Satiation = player.Satiation.ToString() + " / " + player.MaxSatiation.ToString();
    }
}
