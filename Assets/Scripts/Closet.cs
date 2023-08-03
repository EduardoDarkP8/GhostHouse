using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    
    public List<GameObject> players = new List<GameObject>();
    public bool used;
    void Start()
    {
            
    }

    void Update()
    {
        
    }
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.transform.parent.gameObject.tag == "Survival" && Input.GetButtonDown("Fire1")) 
        {
            other.gameObject.transform.parent.gameObject.GetComponent<PlayerHide>().hide(transform);
        }
	}


}
