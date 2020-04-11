using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    Floor floor;
    Player player;
    public GameManager gameManager;

    public float timeOut;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        floor = gameManager.floor;
        player = floor.Player;
    }

    void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut) {
            ProcessInput();
            timeElapsed = 0.0f;
        }
    }


    // Update is called once per frame
    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W)) {
            player.Move(Direction.up);
        }
        if (Input.GetKey(KeyCode.E)) {
            player.Move(Direction.upRight);
        }
        if (Input.GetKey(KeyCode.D)) {
            player.Move(Direction.right);
        }
        if (Input.GetKey(KeyCode.C)) {
            player.Move(Direction.downRight);
        }
        if (Input.GetKey(KeyCode.X)) {
            player.Move(Direction.down);
        }
        if (Input.GetKey(KeyCode.Z)) {
            player.Move(Direction.downLeft);
        }
        if (Input.GetKey(KeyCode.A)) {
            player.Move(Direction.left);
        }
        if (Input.GetKey(KeyCode.Q)) {
            player.Move(Direction.upLeft);
        }
        if (Input.GetKey(KeyCode.S)) {
            player.Attack();
        }
        if (Input.GetKey(KeyCode.M)) {
            gameManager.ShowMap();
        }
        if (Input.GetKey(KeyCode.Alpha0)) {
            player.Use(0);
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            player.Use(1);
        }
        if (Input.GetKey(KeyCode.Alpha1)) {
            player.Use(2);
        }
    }
}
