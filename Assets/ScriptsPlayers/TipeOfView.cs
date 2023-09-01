using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipeOfView : MonoBehaviour
{
    public ViewCharacter viewCharacter;
    public MeshRenderer meshRenderer;
    public bool spotLook;
    public float timer, change = 1.5f;
    void Start()
    {
        if (transform.Find("ViewMesh") != null) 
        {
            viewCharacter = transform.Find("ViewMesh").GetComponent<ViewCharacter>();
        }
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!viewCharacter.pl.pv.IsMine) 
        {
            if (timer >= change)
            {   
                spotLook = false;
            }
            if (!spotLook)
            {
                meshRenderer.enabled = false;
                foreach (Light l in viewCharacter.pl.lights)
                {
                    l.enabled = false;
                }
            }
            else if (spotLook) 
            {
                meshRenderer.enabled = true;
                foreach (Light l in viewCharacter.pl.lights)
                {
                    l.enabled = true;
                }
            }
            timer += Time.deltaTime;
        }
		
    }
    public void TurnOn() 
    {
        spotLook = true;
        timer = 0;
    }
	
}
