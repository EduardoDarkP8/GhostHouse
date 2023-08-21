using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipeOfView : MonoBehaviour
{
    public ViewCharacter viewCharacter;
    void Start()
    {
        if (transform.Find("ViewMesh") != null) 
        {
            viewCharacter = transform.Find("ViewMesh").GetComponent<ViewCharacter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
