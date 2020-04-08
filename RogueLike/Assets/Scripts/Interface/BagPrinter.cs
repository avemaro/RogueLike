using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPrinter : MonoBehaviour
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
        text.text = "";
        foreach (var item in player.Items) {
            text.text += item.ToString();
            text.text += "\n";
        }
    }
}
