using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vencer_ou_Perder : MonoBehaviour
{
    public GameObject contador, letras, ganhar, perder;
    float timerTirarHud, tempoTirarHud = 3;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            AtivarTrivia();
        }
        if (venceu || perdeu)
        {
            timerTirarHud += Time.deltaTime;
            print(timerTirarHud);
        }
        if(timerTirarHud >= tempoTirarHud)
        {
            venceu = false;
            perdeu = false;
            timerTirarHud = 0;
            AcabarTrivia();
        }
    }
    bool isActive = false;
    void AtivarTrivia()
    {
        isActive = true;
        contador.SetActive(isActive);
        letras.SetActive(isActive);
        ganhar.SetActive(isActive);
        perder.SetActive(isActive);
    }
    void AcabarTrivia()
    {
        isActive = false;
        contador.SetActive(isActive);
        letras.SetActive(isActive);
        ganhar.SetActive(isActive);
        perder.SetActive(isActive); 
        contador.GetComponent<TMPro.TextMeshProUGUI>().text = "15";
        letras.GetComponent<TMPro.TextMeshProUGUI>().text = "_ _ _ _";
    }
    bool venceu = false;
    public void Vencer()
    {
        venceu = true;
        letras.GetComponent<TMPro.TextMeshProUGUI>().text = "Á G U A";
    }
    bool perdeu = false;
    public void Perder()
    {
        perdeu = true;
        contador.GetComponent<TMPro.TextMeshProUGUI>().text = " ";
        letras.GetComponent<TMPro.TextMeshProUGUI>().text = "Você Morreu";
    }
}
