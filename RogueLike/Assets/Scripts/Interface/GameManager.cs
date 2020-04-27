using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Floor floor;
    public Text floorPrefab;
    public MapPrinter mapPrinter;
    public FloorPrinterBehaviour floorPrinter;
    public EffectPrinterBehaviour effectPrinter;
    public BagPrinter bagPrinter;
    public PlayerBehaviour playerBehaviour;

    // Start is called before the first frame update
    void Start() {
        bagPrinter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetFloorActive(bool isActive) {
        floorPrinter.gameObject.SetActive(isActive);
    }

    public void ToggleMap() {
        if (mapPrinter.gameObject.activeInHierarchy) {
            mapPrinter.gameObject.SetActive(false);
            SetFloorActive(true);
        } else {
            mapPrinter.gameObject.SetActive(true);
            SetFloorActive(false);
        }
    }

    public void DismissMap() {
        mapPrinter.gameObject.SetActive(false);
    }
}
