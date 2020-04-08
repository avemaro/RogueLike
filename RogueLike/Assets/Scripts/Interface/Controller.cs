using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Floor floor;
    Player player;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        floor = gameManager.floor;
        player = floor.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W)) {
            player.Move(Direction.up);
        }
        if (Input.GetKeyUp(KeyCode.E)) {
            player.Move(Direction.upRight);
        }
        if (Input.GetKeyUp(KeyCode.D)) {
            player.Move(Direction.right);
        }
        if (Input.GetKeyUp(KeyCode.C)) {
            player.Move(Direction.downRight);
        }
        if (Input.GetKeyUp(KeyCode.X)) {
            player.Move(Direction.down);
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            player.Move(Direction.downLeft);
        }
        if (Input.GetKeyUp(KeyCode.A)) {
            player.Move(Direction.left);
        }
        if (Input.GetKeyUp(KeyCode.Q)) {
            player.Move(Direction.upLeft);
        }
        if (Input.GetKeyUp(KeyCode.S)) {
            player.Attack();
        }
        if (Input.GetKeyUp(KeyCode.Alpha0)) {
            player.Use(0);
        }
    }
}
