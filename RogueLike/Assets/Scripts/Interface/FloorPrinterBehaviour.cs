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
        text = Instantiate(gameManager.floorPrefab, transform);
        floor = new Floor(text.text);
        gameManager.floor = floor;
    }

    void Update() {
        Show();
    }

    public void Show() {
        text.text = floor.printer.GetText();
    }
}
