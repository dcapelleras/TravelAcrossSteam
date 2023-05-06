using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public string csvFileName;
    private Dictionary<int, string[]> csvData = new Dictionary<int, string[]>();

    void Start()
    {
        string filePath = Application.dataPath + "/" + csvFileName;
        StreamReader streamReader = new StreamReader(filePath);
        int lineNumber = 0;

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            string[] values = line.Split(',');
            csvData.Add(lineNumber, values);
            lineNumber++;
        }

        streamReader.Close();
    }
}