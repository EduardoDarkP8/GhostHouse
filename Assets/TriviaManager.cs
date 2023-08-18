using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System;

public class TriviaManager : MonoBehaviour
{
     public TextAsset wordsJSON;

    [System.Serializable]
    public class WordList 
    {
        public string[] words;
    }

    public WordList wordList = new WordList();
    // Start is called before the first frame update
    void Start()
    {

        // Certifique-se de que wordsJSON tenha sido atribuído antes de usar
        wordList = JsonUtility.FromJson<WordList>(wordsJSON.text);
        if (wordList == null || wordList.words == null)
        {
            Debug.LogError("Erro ao analisar o JSON ou lista de palavras!");
            return;
        }

        Debug.Log("Número de palavras no JSON: " + wordList.words.Length);
        foreach (string word in wordList.words)
        {
            Debug.Log("Word: " + word);


        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
