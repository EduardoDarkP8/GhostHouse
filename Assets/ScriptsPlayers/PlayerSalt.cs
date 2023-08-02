using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSalt : MonoBehaviour
{
    public PlayerSettings player;
    public Transform saltPoint;
    [SerializeField] GameObject Salt; 
    // Update is called once per frame
    void Update()
    {
        
    }
	private void FixedUpdate()
	{
		
	}
    IEnumerator Dash()
    {
        player.playerBody.GetComponent<Collider>().enabled = false;
        player.plState = playerStates.Dash;
        yield return new WaitForSeconds(0.5f);
        if (player.plState == playerStates.Dash && Input.GetButton("Jump"))
        {
            player.plState = playerStates.Stand;
            player.playerBody.GetComponent<Collider>().enabled = true;
        }
    }
}
