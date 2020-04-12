using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPrinterBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public int width = 15;
    public int height = 11;

    Floor floor;
    Text text;

    void Awake() {
        if (gameManager.floorPrefab != null) {
            text = Instantiate(gameManager.floorPrefab, transform);
            floor = FloorMaker.Create(text.text);
        } else {
            text = gameObject.GetComponent<Text>();
            floor = FloorMaker.Create();
        }
        gameManager.floor = floor;
        gameManager.floorPrinter = this;
    }

    void Update() {
        Show();
    }

    public void Show() {
        text.text = floor.printer.GetText(width, height);
    }
}
