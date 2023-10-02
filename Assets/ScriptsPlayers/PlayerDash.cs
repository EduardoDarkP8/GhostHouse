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
        if (player.pv.IsMine && (bool)PhotonNetwork.CurrentRoom.CustomProperties["StartMatch"])
        {
            if (player.buttons[0].click && time >= targetTime && player.plState != playerStates.Stunned)
            {
                player.playAudio(1);
                time = 0;
                StartCoroutine(Dash());
                player.pv.RPC("canNotFight",RpcTarget.All);
            }
            else if (time <= targetTime)
            {
                time += Time.deltaTime;
            }
        }
    }
	private void FixedUpdate()
	{
        if (player.pv.IsMine && (bool)PhotonNetwork.CurrentRoom.CustomProperties["StartMatch"]) {
            if (player.plState == playerStates.Dash && player.buttons[0].click)
            {
                player.rg.AddForce(player.playerBody.transform.forward * player.dashForce, ForceMode.Impulse);
            }
            else if (player.plState == playerStates.Dash && !player.buttons[0].click)
            {
                player.plState = playerStates.Stand;
                player.playerBody.GetComponent<Collider>().enabled = true;
            }
            else if (player.plState == playerStates.Fight) 
            {
                player.playerBody.GetComponent<Collider>().enabled = true;
            }
            if (player.inLimit)
            {
                player.plState = playerStates.Stand;
                player.playerBody.GetComponent<Collider>().enabled = true;
                player.inLimit = false;
            }
        }
	}
    IEnumerator Dash() 
    {
        player.playerBody.GetComponent<Collider>().enabled = false;
        player.triviaManager.colliderTipe.GetComponent<Collider>().enabled = false;
        player.plState = playerStates.Dash;
        yield return new WaitForSeconds(0.5f);
		if (player.plState == playerStates.Dash && player.buttons[0].click) 
        {
            player.plState = playerStates.Stand;
            player.playerBody.GetComponent<Collider>().enabled = true;
            
        }
    }
    [PunRPC]
    public void canNotFight() 
    {
        player.canFightTime = 0f;
        player.canFight = false;
    }
}
