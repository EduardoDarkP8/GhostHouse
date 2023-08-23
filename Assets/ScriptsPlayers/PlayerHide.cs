using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Photon.Pun;
public class PlayerHide : MonoBehaviour
{
    public PlayerSettings player;
    public bool find;

    void Start()
    {

    }

    void Update()
    {
        if (player.pv.IsMine) { 
        if (Input.GetButtonUp("Interact") && player.plState == playerStates.Hidden)
        {
                player.pv.RPC("exitClosetCourotine", RpcTarget.All);
        }
        else if (Input.GetButtonUp("Interact") && player.plState != playerStates.Hidden)
        {
                player.pv.RPC("findClosetCourotine", RpcTarget.All);
        }
        }
    }
    public void hide(Transform local, Closet cl)
    {
        if (player.plState != playerStates.Hidden && find)
        {
			if (!cl.isUsing) 
            {
                player.plState = playerStates.Hidden;
                player.playerBody.GetComponent<Collider>().isTrigger = false;
                player.transform.position = new Vector3(local.position.x, transform.position.y, local.position.z);
                player.playerBody.GetComponent<MeshRenderer>().enabled = false;
                foreach (Light l in player.lights)
                {
                    l.enabled = false;
                }
                cl.players.Add(gameObject);
            }
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
    public void getOutPhoton(PhotonMessageInfo info)
    { 
        player.plState = playerStates.Stand;
        player.playerBody.GetComponent<Collider>().isTrigger = false;
        player.playerBody.GetComponent<MeshRenderer>().enabled = true;
        foreach (Light l in player.lights)
        {
            l.enabled = true;
        }
        find = false;
        player.isStuning = true;
    }
    [PunRPC]
    public void findClosetCourotine(PhotonMessageInfo info)
    {

        StartCoroutine(findCloset());

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
    [PunRPC]
    public void exitClosetCourotine(PhotonMessageInfo info) 
    {

        StartCoroutine(exitCloset());
        
    }
    IEnumerator exitCloset()
    {
        find = false;
        yield return new WaitForSeconds(0.5f);
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
