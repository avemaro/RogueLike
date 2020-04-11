using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPrinterBehaviour : MonoBehaviour
{
    public GameManager gameManager;

    Floor floor;
    Text text;

    void Awake() {
        //text = Instantiate(gameManager.floorPrefab, transform);
        text = gameObject.GetComponent<Text>();
        floor = FloorMaker.Create();
        gameManager.floor = floor;
        gameManager.floorPrinter = this;
    }

    void Update() {
        Show();
    }

    public void Show() {
        text.text = floor.printer.GetText();
    }
}
