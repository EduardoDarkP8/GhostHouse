using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerSalt : MonoBehaviour
{
    public PlayerSettings player;
    public Transform saltPoint;
    public float time = 10f, targetTime = 10f;
    // Update is called once per frame
    void Update()
    {
		if (player.pv.IsMine) 
        {
            if (Input.GetButtonDown("Jump") && time >= targetTime && player.plState != playerStates.Hidden)
            {
                time = 0;
                player.pv.RPC("NetworkSalt", RpcTarget.All);
            }
            else if (time <= targetTime)
            {
                time += Time.deltaTime;
            }
        }
    }
	private void FixedUpdate()
	{
		
	}
    [PunRPC]
    public void NetworkSalt(PhotonMessageInfo info) 
    {
        StartCoroutine(placeSalt());
    } 

    IEnumerator placeSalt()
    {
            player.playerBody.GetComponent<Collider>().enabled = false;
            player.plState = playerStates.Salt;
            yield return new WaitForSeconds(0.3f);
            Instantiate(player.salt, player.saltPoint.position, Quaternion.identity);
            player.plState = playerStates.Stand;
            player.playerBody.GetComponent<Collider>().enabled = true;
    }
}
