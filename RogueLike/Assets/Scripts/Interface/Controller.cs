using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    static readonly (KeyCode key, Direction direction)[] keyCodes = {
        (KeyCode.W, Direction.up), (KeyCode.E, Direction.upRight),
        (KeyCode.D, Direction.right), (KeyCode.C, Direction.downRight),
        (KeyCode.X, Direction.down), (KeyCode.Z, Direction.downLeft),
        (KeyCode.A, Direction.left), (KeyCode.Q, Direction.upLeft)
    };

    PlayerBehaviour playerBehaviour;
    public GameManager gameManager;


    public float timeOut　= 0.1f;
    private float timeElapsed;

    // Start is called before the first frame update
    void Start()
    {
        playerBehaviour = gameManager.playerBehaviour;
    }

    void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut) {
            ProcessInput2();
            timeElapsed = 0.0f;
        }

        ProcessInput();
    }


    // Update is called once per frame
    void ProcessInput()
    {
        if (gameManager.bagPrinter.IsVisible) return;

        foreach (var (key, direction) in keyCodes)
            if (Input.GetKeyUp(key))
                playerBehaviour.Move(direction);

        if (Input.GetKeyUp(KeyCode.S))
            playerBehaviour.Attack();

        if (Input.GetKeyUp(KeyCode.M))
            gameManager.ToggleMap();

        if (Input.GetKeyUp(KeyCode.I))
            gameManager.bagPrinter.gameObject.SetActive(true);

    }

    void ProcessInput2() {
        if (gameManager.bagPrinter.IsVisible) return;

        if (!Input.GetKey(KeyCode.Space)) return;

        foreach (var (key, direction) in keyCodes)
            if (Input.GetKey(key))
                playerBehaviour.Move(direction);

        if (Input.GetKey(KeyCode.S))
            playerBehaviour.Attack();

    }
}
