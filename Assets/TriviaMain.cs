using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

public class TriviaMain : MonoBehaviour
{
    public List<TriviaWords> words = new List<TriviaWords>();
    public List<TriviaButton> buttons = new List<TriviaButton>();
    public int count = 0;
    public string word  = "AA";

    void Start()
    {
        count = 0;
    }

    
    void Update()
    {
        
    }
    public void ButtonsController(string value, Button btn) 
    {
            words.ToArray()[count].last = true;
            words.ToArray()[count].originalButton = btn;
            words.ToArray()[count].text.text = value;        
            if (count != 0) 
            {
                words.ToArray()[count - 1].last = false;
            }
            count++;
            CheckWord();
    }
    public void WordController() 
    {
        count--;
        words.ToArray()[count].last = false;
        words.ToArray()[count].originalButton.interactable = true;
        words.ToArray()[count].text.text = "";
        if (count != 0)
        {
            words.ToArray()[count - 1].last = true;
        }
        
    }    
    public void CheckWord() 
    {
        string formWord = "";
        foreach (TriviaWords tr in words) 
        {
            formWord += tr.text.text;
        }
        if (formWord == word) 
        {
            Destroy(gameObject);
        }
    }
}
