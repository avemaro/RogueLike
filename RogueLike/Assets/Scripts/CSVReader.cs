using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset equipmentData; // CSVファイル
    public TextAsset affectParameterData;
    public TextAsset otherData;

    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    // Start is called before the first frame update
    void Awake()
    {
        //csvFile = Resources.Load("testCSV") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(equipmentData.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        ItemType type = ItemType.weapon;
        foreach (var line in csvDatas) {
            if (line[0] == "weapon") type = ItemType.weapon;
            if (line[0] == "shield") type = ItemType.shield;
            if (line[0] == "bracelet") type = ItemType.bracelet;
            if (line[0] == "arrow") type = ItemType.arrow;
            ItemData.AddData(type, line[1], int.Parse(line[2]), int.Parse(line[3]),
                0, 0, int.Parse(line[6]));
        }


        csvDatas = new List<string[]>();
        reader = new StringReader(affectParameterData.text);
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        foreach (var line in csvDatas) {
            if (line[0] == "drag") type = ItemType.drag;
            if (line[0] == "food") type = ItemType.food;
            ItemData.AddData(type, line[1], int.Parse(line[2]), int.Parse(line[3]),
                int.Parse(line[4]), int.Parse(line[5]), int.Parse(line[6]),
                int.Parse(line[7]), int.Parse(line[8]));
        }

        csvDatas = new List<string[]>();
        reader = new StringReader(otherData.text);
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvDatas.Add(line.Split(',')); // , 区切りでリストに追加
        }

        foreach (var line in csvDatas) {
            if (line[0] == "wand") type = ItemType.wand;
            if (line[0] == "scroll") type = ItemType.scroll;
            if (line[0] == "pot") type = ItemType.pot;
            //Debug.Log(line[1]);
            //Debug.Log(line[2]);
            ItemData.AddData(type, line[1], int.Parse(line[2]));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}