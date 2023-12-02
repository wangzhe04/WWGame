using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class WordDisplay : MonoBehaviour
{
    public CSVReader csvReader;
    public TextMeshPro displayText;
    public GameObject[] letterPrefabs;
    private Dictionary<char, GameObject> letterPrefabMap = new Dictionary<char, GameObject>();

    public Vector3 startPosition; // Public property to set start position in Unity Editor
    public float spacing = 0.1f;

    void Awake()
    {
        InitializePrefabMap();
    }

    IEnumerator Start()
    {
        yield return new WaitUntil(() => csvReader.words.Count > 0);

        if (csvReader.words.Count > 0)
        {
            UpdateWordDisplay(csvReader.words[0]);
        }
        else
        {
            Debug.LogError("Word list is empty.");
        }
    }

    public void OnNextWord()
    {
        if (csvReader.words.Count > 0)
        {
            int currentIndex = (csvReader.words.IndexOf(displayText.text) + 1) % csvReader.words.Count;
            UpdateWordDisplay(csvReader.words[currentIndex]);
        }
    }

    void InitializePrefabMap()
    {
        char[] letters = { 'T', 'H', 'I', 'N', 'G' };
        for (int i = 0; i < letters.Length; i++)
        {
            letterPrefabMap[letters[i]] = letterPrefabs[i];
        }
    }

    // void UpdateWordDisplay(string word)
    // {
    //     foreach (Transform child in transform)
    //     {
    //         Destroy(child.gameObject);
    //     }

    //     Vector3 nextPosition = startPosition; // Use the startPosition

    //     foreach (char c in word.ToUpper())
    //     {
    //         GameObject letterPrefab = GetPrefabForLetter(c);
    //         if (letterPrefab != null)
    //         {
    //             GameObject letter = Instantiate(letterPrefab, nextPosition, Quaternion.identity, this.transform);
    //             nextPosition.x += spacing;
    //         }
    //     }

    //     displayText.text = word;
    // }

void UpdateWordDisplay(string word)
{
    // Clear old letters
    foreach (Transform child in transform)
    {
        Destroy(child.gameObject);
    }

    float totalWidth = CalculateTotalWidth(word);

    // Adjust the starting position for center alignment
    Vector3 nextPosition = new Vector3(-totalWidth / 2, startPosition.y, startPosition.z);

    foreach (char c in word.ToUpper())
    {
        GameObject letterPrefab = GetPrefabForLetter(c);
        if (letterPrefab != null)
        {
            GameObject letter = Instantiate(letterPrefab, nextPosition, Quaternion.identity, this.transform);
            // Adjust next position based on the width of the current letter
            float letterWidth = letter.GetComponent<Renderer>().bounds.size.x;
            nextPosition.x += letterWidth / 2; // Half the width of the current letter

            // Instantiate the letter, then add the other half of its width plus the spacing
            nextPosition.x += letterWidth / 2 + spacing;
        }
    }

    displayText.text = word;
}

float CalculateTotalWidth(string word)
{
    float width = 0f;
    foreach (char c in word.ToUpper())
    {
        if (letterPrefabMap.TryGetValue(c, out GameObject prefab))
        {
            // Include each letter's width and spacing
            width += prefab.GetComponent<Renderer>().bounds.size.x + spacing;
        }
    }
    // Adjust for the final spacing
    return width - spacing;
}

    GameObject GetPrefabForLetter(char letter)
    {
        if (letterPrefabMap.TryGetValue(letter, out GameObject prefab))
        {
            return prefab;
        }
        return null;
    }
}
