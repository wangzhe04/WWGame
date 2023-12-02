using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    // Path to the CSV file
    private string csvFilePath = "Assets/Thing.csv";

    // Public list to hold data, accessible from other scripts
    public List<string> words = new List<string>();

    void Start()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        // Check if file exists
        if (File.Exists(csvFilePath))
        {
            // Read file line by line
            string[] lines = File.ReadAllLines(csvFilePath);
            foreach (string line in lines)
            {
                // Split line by commas and add words to the list
                string[] wordsInLine = line.Split(',');
                foreach (string word in wordsInLine)
                {
                    words.Add(word);
                }
            }

            // Debug output to show words read
            foreach (string word in words)
            {
                Debug.Log(word);
            }
        }
        else
        {
            Debug.LogError("CSV file not found at " + csvFilePath);
        }
    }
}
