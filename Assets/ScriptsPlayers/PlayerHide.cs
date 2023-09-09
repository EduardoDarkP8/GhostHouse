using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Photon.Pun;
public class PlayerHide : MonoBehaviour
{
    public PlayerSettings player;
    public bool find;
    public bool hideBool;
    public bool exitBool;
    void Start()
    {

    }

    void Update()
    {
        if (player.pv.IsMine) { 
        if (Input.GetButtonUp("Interact") && player.plState == playerStates.Hidden)
        {
                exitBool = true;
                
            }
        else if (Input.GetButtonUp("Interact") && player.plState != playerStates.Hidden)
        {
                hideBool = true;
        }
        }
		
        if (hideBool) 
        {
            player.pv.RPC("findClosetCourotine", RpcTarget.All);
            hideBool = false;
        }
		if(exitBool)
        {
            player.pv.RPC("exitClosetCourotine", RpcTarget.All);
            exitBool = false;
        }
    }
    public void hide(Transform local, Closet cl)
    {
        if (player.plState != playerStates.Hidden && find)
        {
			if (!cl.isUsing) 
            {
                player.transform.position = new Vector3(local.position.x, transform.position.y, local.position.z);
                player.pv.RPC("hidePhoton", RpcTarget.All);
                foreach (Light l in player.lights)
                {
                    l.enabled = false;
                }
                cl.players.Add(gameObject);
            }
        }
    }
    IEnumerator findCloset()
    {
        find = true;
        yield return new WaitForSeconds(0.1f);
        if (player.plState != playerStates.Hidden)
        {
            find = false;
        }
    }
    public void getOut(Transform local)
    {
        if (player.plState == playerStates.Hidden)
        {

            player.transform.position = new Vector3(local.position.x, 0, local.position.z);
            player.pv.RPC("getOutPhoton",RpcTarget.All);
        }
    }
    [PunRPC]
    public void hidePhoton(PhotonMessageInfo info)
    {
        player.plState = playerStates.Hidden;
    }
    [PunRPC]
    public void getOutPhoton(PhotonMessageInfo info)
    { 
        player.plState = playerStates.Stand;
        find = false;
        player.exit = true;
        print("AA");
    }
    [PunRPC]
    public void findClosetCourotine(PhotonMessageInfo info)
    {
        StartCoroutine(findCloset());
    }
    
    [PunRPC]
    public void exitClosetCourotine(PhotonMessageInfo info) 
    {
        find = false;
    }
    private void OnTriggerStay(Collider other)
    {
        checkPlayer(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        checkPlayer(other);
    }
    void checkPlayer(Collider collider)
    {
            if (collider.tag == "Closet")
            {
                Closet cl = collider.GetComponent<Closet>();
                GameObject gm = collider.gameObject;
                if (!collider.GetComponent<Closet>().players.Contains(gameObject))
                {
                    hide(collider.transform, cl);

                }
                else if (!find)
                {
                    getOut(cl.jumpPoint);
                }
            }
        
    }
}
