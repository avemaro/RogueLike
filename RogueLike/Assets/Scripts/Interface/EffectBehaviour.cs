using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBehaviour : MonoBehaviour
{
    public Direction direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;
        var x = 0f;
        var y = 0f;
        switch (direction) {
            case Direction.up:
                y = 0.03f;
                break;
            case Direction.upRight:
                x = 0.03f;
                y = 0.03f;
                break;
            case Direction.right:
                x = 0.03f;
                break;
            case Direction.downRight:
                x = 0.03f;
                y = -0.03f;
                break;
            case Direction.down:
                y = -0.03f;
                break;
            case Direction.downLeft:
                x = -0.03f;
                y = -0.03f;
                break;
            case Direction.left:
                x = -0.03f;
                break;
            case Direction.upLeft:
                x = -0.03f;
                y = 0.03f;
                break;
        }

        transform.position = new Vector3(position.x + x, position.y + y);

        //if (direction == Direction.down)
        //    transform.position = new Vector3(position.x, position.y - 0.03f);
        //else
        //    transform.position = new Vector3(position.x + 0.03f, position.y);
    }
}
