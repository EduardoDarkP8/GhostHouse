using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSalt : MonoBehaviour
{
    public PlayerSettings player;
    public Transform saltPoint;
    public float time = 10f, targetTime = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && time >= targetTime)
        {
            time = 0;
            StartCoroutine(placeSalt());
        }
        else if (time <= targetTime)
        {
            time += Time.deltaTime;
        }
    }
	private void FixedUpdate()
	{
		
	}
    IEnumerator placeSalt()
    {
        player.playerBody.GetComponent<Collider>().enabled = false;
        player.plState = playerStates.Salt;
        yield return new WaitForSeconds(0.3f);
        Instantiate(player.salt,player.saltPoint.position,Quaternion.identity);
        player.plState = playerStates.Stand;
        player.playerBody.GetComponent<Collider>().enabled = true;  
    }
}
