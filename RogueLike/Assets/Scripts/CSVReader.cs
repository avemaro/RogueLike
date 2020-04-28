using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset ItemDatabase;
    public TextAsset EnemyDatabase;
    public TextAsset KobamiDatabase;

    List<string[]> csvData = new List<string[]>(); // CSVの中身を入れるリスト;

    // Start is called before the first frame update
    void Awake()
    {
        StringReader reader = new StringReader(ItemDatabase.text);

        while (reader.Peek() != -1) {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvData.Add(line.Split(',')); // , 区切りでリストに追加
        }

        foreach (var line in csvData) {
            var intList = new List<int>();
            for (var i = 3; i < line.Length; i++) {
                intList.Add(int.Parse(line[i]));
            }
            ItemData.AddData(line[0], line[1], line[2], intList.ToArray());
        }

        reader = new StringReader(EnemyDatabase.text);
        csvData = new List<string[]>();

        while (reader.Peek() != -1) {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvData.Add(line.Split(',')); // , 区切りでリストに追加
        }

        foreach (var line in csvData) {
            var intList = new List<int>();
            for (var i = 2; i < line.Length; i++) {
                intList.Add(int.Parse(line[i]));
            }
            EnemyData.AddData(line[0].ToCharArray()[0], line[1], intList.ToArray());
        }

        reader = new StringReader(KobamiDatabase.text);
        csvData = new List<string[]>();

        while (reader.Peek() != -1) {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            csvData.Add(line.Split(',')); // , 区切りでリストに追加
        }

        foreach (var line in csvData) {
            var intList = new List<int>();
            for (var i = 0; i < line.Length; i++) {
                intList.Add(int.Parse(line[i]));
            }
            EnemyData.AddDistribution(intList.ToArray());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}