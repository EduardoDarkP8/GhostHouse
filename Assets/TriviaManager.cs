using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public class TriviaManager : MonoBehaviour
{
    public TextAsset wordsJSON;
    string conteudo;
    public Dictionary<string, List<string>> dictionary = new Dictionary<string, List<string>>();

    // Start is called before the first frame update
    void Start()
    {
        conteudo = wordsJSON.text;
        Debug.Log("JSON Content: " + conteudo); // Verifique se o conteúdo está correto
        dictionary = JsonUtility.FromJson<Dictionary<string, List<string>>>(conteudo);

        foreach (var kvp in dictionary)
        {
            Debug.Log("Número de letras: " + kvp.Key);
            foreach (var word in kvp.Value)
            {
                Debug.Log("Palavra: " + word);
            }
        }
    }
    void Update()
    {
        
    }
}
