using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPrinter : MonoBehaviour
{
    public GameManager gameManager;
    Player player;

    Text text;

    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<Text>();
        player = gameManager.floor.Player;
    }

    // Update is called once per frame
    void Update() {
        text.text = "Lv." + player.Level + " HP:" + player.HP + "/" + player.MaxHP + " AP:" + player.AP
            + " DP:" + player.DP + "\n";
        text.text += player.Satiation + "/" + player.MaxSatiation + "%  " + gameManager.floor.NumberOfStairs + "F";
    }
}
