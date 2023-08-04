using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerHide : MonoBehaviour
{
    public PlayerSettings player;
    bool find;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetButton("Interact") && player.plState != playerStates.SeekCloset) 
        {
            StartCoroutine(findCloset());

        }
    }
    public void hide(Transform local, Closet cl) 
    {
        if (player.plState != playerStates.Hidden)
        {
            player.plState = playerStates.Hidden;
            player.playerBody.GetComponent<Collider>().isTrigger = false;
            player.transform.position = new Vector3(local.position.x, transform.position.y, local.position.z);
            player.playerBody.GetComponent<MeshRenderer>().enabled = false;

            cl.players.Add(gameObject);
            
        }   
    }
    public void getOut(Transform local) 
    {
        if(player.plState == playerStates.Hidden)
        {
            player.plState = playerStates.Stand;
            player.transform.position += new Vector3(local.forward.x, 0, local.forward.z);
            player.playerBody.GetComponent<Collider>().isTrigger = false;
            player.playerBody.GetComponent<MeshRenderer>().enabled = true;

        }
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
        if (collider.tag == "Closet" && find)
        {
            find = false;
            GameObject gm = collider.gameObject;
                
                if (!collider.GetComponent<Closet>().players.Contains(gameObject))
                {
                    hide(collider.transform, collider.GetComponent<Closet>());

                }
                if(collider.GetComponent<Closet>().players.Contains(gameObject))
                {
                    getOut(collider.transform);
                }
            
        }
    }
    IEnumerator findCloset() 
    {
        find = true;
        player.plState = playerStates.SeekCloset;
        yield return new WaitForSeconds(0.5f);
        player.plState = playerStates.Stand;
        find = false;
    }
}
