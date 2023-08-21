using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaLetters : MonoBehaviour
{
    public TriviaMain main;
    public Button originalButton;
    public bool last;
    public Button button;
    public Text text;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Click);
        text = transform.Find("Letra").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
		if (last) 
        {
            button.interactable = true;
        }
		else 
        {
            button.interactable = false;
        }
    }
    void Click() 
    {
        main.WordController();
    }
}
