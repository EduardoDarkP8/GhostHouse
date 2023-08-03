using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public PlayerSettings player;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void hide(Transform local) 
    {
        
            player.plState = playerStates.Hidden;
            player.playerBody.GetComponent<Collider>().enabled = false;
            player.transform.position = new Vector3(local.position.x,transform.position.y,local.position.z);
            player.playerBody.GetComponent<MeshRenderer>().enabled = false;
    }
}
