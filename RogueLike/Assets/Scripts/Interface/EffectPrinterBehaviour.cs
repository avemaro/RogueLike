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

    private void Start() {
        text = gameObject.GetComponent<Text>();
        gameManager.effectPrinter = this;
    }

    private void Update() {
        text.text = effectPrinter.GetText(width, height);
    }
}
