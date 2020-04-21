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
    public BagPrinter bagPrinter;
    public PlayerBehaviour playerBehaviour;
    public GameObject effects;

    // Start is called before the first frame update
    void Start() {
        bagPrinter.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ShowMap() {
        if (mapPrinter.gameObject.activeInHierarchy) {
            mapPrinter.gameObject.SetActive(false);
            floorPrinter.gameObject.SetActive(true);
        } else {
            mapPrinter.gameObject.SetActive(true);
            floorPrinter.gameObject.SetActive(false);
        }
    }
}
