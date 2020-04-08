using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorPrinterBehaviour : MonoBehaviour
{
    Floor floor;
    public Text floorPrefab;
    Text text;

    void Start() {
        text = Instantiate(floorPrefab, transform);

        floor = new Floor(text.text);
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.W)) {
            floor.Player.Move(Direction.up);
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            floor.Player.Move(Direction.upRight);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            floor.Player.Move(Direction.right);
        }
        if (Input.GetKeyUp(KeyCode.C)) {
            floor.Player.Move(Direction.downRight);
        }
        if (Input.GetKeyUp(KeyCode.X)) {
            floor.Player.Move(Direction.down);
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            floor.Player.Move(Direction.downLeft);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            floor.Player.Move(Direction.left);
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            floor.Player.Move(Direction.upLeft);
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            floor.Player.Attack();
        }

        Show();
    }

    public void Show() {
        text.text = floor.printer.GetText();
    }
}
