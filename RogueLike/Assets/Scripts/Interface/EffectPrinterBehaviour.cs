using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectPrinterBehaviour : MonoBehaviour
{
    public GameManager gameManager;
    public int width = 15;
    public int height = 11;

    Text text;
    EffectPrinter effectPrinter;

    public float timeOut = 0.5f;
    private float timeElapsed;

    private void Start() {
        text = gameObject.GetComponent<Text>();
        gameManager.effectPrinter = this;
        effectPrinter = new EffectPrinter(gameManager.floor);
    }

    private void Update() {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut) {
            effectPrinter.ClearEffects();
            timeElapsed = 0.0f;
        }
        text.text = effectPrinter.GetText(width, height);

    }
}
