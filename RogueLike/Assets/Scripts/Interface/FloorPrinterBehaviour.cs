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
        Debug.Log(floor);
        if (gameManager.floorPrefab != null) {
            text = Instantiate(gameManager.floorPrefab, transform);
            floor = FloorMaker.Create(text.text);
            floor.Rooms.Add(new Room(floor, (0, 0), (100, 100)));
        } else {
            text = gameObject.GetComponent<Text>();
            floor = FloorMaker.Create();
        }
        Debug.Log(floor);
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
