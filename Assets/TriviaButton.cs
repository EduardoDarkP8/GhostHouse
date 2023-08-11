using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriviaButton : MonoBehaviour
{
    public TriviaMain main;
    public Text text;
    public Button button;
    public List<string> strings = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
        button = GetComponent<Button>();
        text = transform.Find("Letra").GetComponent<Text>();

    }
    void Click()
    {
        button.interactable = false;
        main.ButtonsController(text.text,button);
    }
}
