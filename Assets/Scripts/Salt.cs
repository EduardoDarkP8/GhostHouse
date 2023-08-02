using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : MonoBehaviour
{
    public GameObject gm;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ghost")
		{
            gm = other.gameObject.transform.parent.gameObject;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(velocityDegree(gm.GetComponent<PlayerSettings>()));
		}
	}
    IEnumerator velocityDegree(PlayerSettings player) 
    {
        float velocity = player.speed;
        player.speed /= 5;
        yield return new WaitForSeconds(2f);
        player.speed = velocity;
        Destroy(gameObject);
    }
}
