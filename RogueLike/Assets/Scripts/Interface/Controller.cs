using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static readonly (KeyCode, Direction)[] keyCodes = {
        (KeyCode.W, Direction.up), (KeyCode.E, Direction.upRight),
        (KeyCode.D, Direction.right), (KeyCode.C, Direction.downRight),
        (KeyCode.X, Direction.down), (KeyCode.Z, Direction.downLeft),
        (KeyCode.A, Direction.left), (KeyCode.Q, Direction.upLeft)
    };

    Floor floor;
    Player player;
    public GameManager gameManager;

    KeyCode keyInput;

    public float timeOut　= 0.1f;
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

        if (Input.GetKeyUp(KeyCode.I))
            gameManager.bagPrinter.gameObject.SetActive(true);
        //gameManager.bagPrinter.ToggleVisible();

        //foreach (var keycode in keyCodes)
        //    if (Input.GetKeyUp(keycode.Item1))
        //        keyInput = keycode.Item1;
    }


    // Update is called once per frame
    void ProcessInput()
    {
        if (gameManager.bagPrinter.IsVisible) return;

        //foreach (var keycode in keyCodes)
        //    if (keycode.Item1 == keyInput)
        //        player.Move(keycode.Item2);
        //keyInput = KeyCode.F1;
        //if (Input.GetKey(keycode.Item1))
        //player.Move(keycode.Item2);

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
        if (Input.GetKey(KeyCode.P)) {
            player.Spawn(Chess.Pawn);
        }
    }
}
