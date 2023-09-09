using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerDash : MonoBehaviour
{
    public PlayerSettings player;
    public float time= 10f, targetTime = 10f;
    
    void Update()
    {
        if (player.pv.IsMine)
        {
            if (Input.GetButtonDown("Jump") && time >= targetTime)
            {
                time = 0;
                StartCoroutine(Dash());
            }
            else if (time <= targetTime)
            {
                time += Time.deltaTime;
            }
        }
    }
	private void FixedUpdate()
	{
        if (player.pv.IsMine) {
            if (player.plState == playerStates.Dash && Input.GetButton("Jump"))
            {
                player.rg.AddForce(player.playerBody.transform.forward * player.dashForce, ForceMode.Impulse);
            }
            else if (player.plState == playerStates.Dash && !Input.GetButton("Jump"))
            {
                player.plState = playerStates.Stand;
                player.playerBody.GetComponent<Collider>().enabled = true;
            }
        }
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
