using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriviaWords : MonoBehaviour
{
    public TriviaMain main;
    public Button originalButton;
    public bool last;
    public Button button;
    public Text text;
    void Start()
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
