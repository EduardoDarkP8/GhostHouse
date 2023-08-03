using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public PlayerSettings player;
    public float x, z;
    Quaternion target;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player.plState == playerStates.Stand || player.plState == playerStates.Walk) 
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
			if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0) 
            {
                player.plState = playerStates.Stand;
            }
        }
		else 
        {
            x = 0;
            z = 0;
        }
        if (x != 0 || z != 0)
        {
            target = Quaternion.Euler(0, Mathf.Atan2(x, z) * Mathf.Rad2Deg, 0);
            player.plState = playerStates.Walk;
        }
        
        

    }
    private void FixedUpdate()
    {
        
            player.rg.velocity = new Vector3(x, 0, z) * player.speed;
            player.playerBody.transform.rotation = target;
    }
}