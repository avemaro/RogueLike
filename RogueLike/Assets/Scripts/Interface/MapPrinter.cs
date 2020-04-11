using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPrinter : MonoBehaviour
{
    public GameManager gameManager;

    Floor floor;
    Text text;

    // Start is called before the first frame update
    void Start() {
        text = gameObject.GetComponent<Text>();
        floor = gameManager.floor;
        gameManager.mapPrinter = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        text.text = "";
        foreach (var mapLine in floor.printer.GetMap()) {
            text.text += mapLine;
            text.text += "\n";
        }
    }
}
