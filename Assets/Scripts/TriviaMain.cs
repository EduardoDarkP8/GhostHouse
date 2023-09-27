using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class TriviaMain : MonoBehaviour
{
    public List<TriviaLetters> letters = new List<TriviaLetters>();
    public List<TriviaButton> buttons = new List<TriviaButton>();
    public int count = 0;
    public string word  = "WORLD";
    public char[] wordArray;
    public string randomizedWord;
    public float time = 10;
    public Text timer;
    public PlayerSettings pl;
    void Start()
    {
        count = 0;
        RandomizeWord();
        SetButtonsLetter();
    }
    void Update()
    {
        time -= Time.deltaTime;
        timer.text = ((int)time).ToString();
        if (pl.gameObject.tag == "Survival" && pl.life <= 0)
        {
            pl.plState = playerStates.Loser;
            Destroy(gameObject);
            pl.change = true;
        }
        if (time < 0 || pl.plState != playerStates.Fight) 
        {
			if (pl.gameObject.tag == "Ghost")
            {
                pl.plState = playerStates.Loser;
            }
            else if (pl.gameObject.tag == "Survival")
            {
                pl.plState = playerStates.Winner;
            }
            Destroy(gameObject);
            pl.change = true;
        }
    }
    public void SetButtonsLetter() 
    {
        int i = 0;
		foreach (TriviaButton btn in buttons) 
        {
            btn.text.text = randomizedWord[i].ToString();
            i++;
        }
    }
    public void RandomizeWord() 
    {
        randomizedWord = word;
        wordArray = randomizedWord.ToCharArray();
        int[] availableIndices = new int[wordArray.Length];
        for (int i = 0; i < availableIndices.Length; i++)
        {
            availableIndices[i] = i;
        }

        for (int j = 0; j < wordArray.Length; j++)
        {
            int randomIndex = Random.Range(j, availableIndices.Length);
            int tempIndex = availableIndices[j];
            availableIndices[j] = availableIndices[randomIndex];
            availableIndices[randomIndex] = tempIndex;

            char character = wordArray[j];
            wordArray[j] = wordArray[randomIndex];
            wordArray[randomIndex] = character;
        }

        randomizedWord = new string(wordArray);

    }
    public void ButtonsController(string value, Button btn) 
    {
            letters.ToArray()[count].last = true;
            letters.ToArray()[count].originalButton = btn;
            letters.ToArray()[count].text.text = value;        
            if (count != 0) 
            {
                letters.ToArray()[count - 1].last = false;
            }
            count++;
            CheckWord();
    }
    public void WordController() 
    {
        count--;
        letters.ToArray()[count].last = false;
        letters.ToArray()[count].originalButton.interactable = true;
        letters.ToArray()[count].text.text = "";
        if (count != 0)
        {
            letters.ToArray()[count - 1].last = true;
        }
        
    }    
    public void CheckWord() 
    {
        string formWord = "";
        foreach (TriviaLetters tr in letters) 
        {
            formWord += tr.text.text;
        }
        if (formWord == word) 
        {
            Destroy(gameObject);
            pl.plState = playerStates.Winner;
            pl.targetPl.plState = playerStates.Loser;
            pl.change = true;
            pl.targetPl.change = true;
			if (pl.gameObject.tag == "Ghost" && pl.plState == playerStates.Winner) 
            {
                pl.targetPl.life = 0;
            }
        }
    }
}
