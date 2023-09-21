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
		if (player.pv.IsMine && (bool)PhotonNetwork.CurrentRoom.CustomProperties["StartMatch"]) 
        {
            if (Input.GetButtonDown("Jump") && time >= targetTime && player.plState != playerStates.Hidden)
            {
                time = 0;
                player.pv.RPC("NetworkSalt", RpcTarget.All);
                GameObject gm = PhotonNetwork.Instantiate("Salt", player.saltPoint.position, Quaternion.identity);
                gm.GetComponent<Salt>().ps = this;
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
        player.anima.SetBool("Salt",true);
        yield return new WaitForSeconds(0.3f);
        player.plState = playerStates.Stand;
        player.anima.SetBool("Salt", false);
        player.playerBody.GetComponent<Collider>().enabled = true;
    }
    public void DestroySalt(GameObject gm) 
    {
        PhotonNetwork.Destroy(gm);
    }
}
