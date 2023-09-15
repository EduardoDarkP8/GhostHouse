using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    public PlayerSettings player;
    public float x, z;
    Quaternion target;
    //public bl_Joystick joystick;
    public GameObject joystickInstance;
    public float timer, targetTime=0.25f;

    void Start()
    {
		if (player.pv.IsMine) 
        {
            joystickInstance = GameObject.Find("Joystick");
            //joystick = joystickInstance.GetComponent<bl_Joystick>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
		if (player.pv.IsMine) 
        {
			
            if (player.plState != playerStates.Fight || player.plState != playerStates.Loser || player.plState != playerStates.Winner) 
            { 
            
            if (player.plState == playerStates.Stand || player.plState == playerStates.Walk)
            {
                
                    
                    //x = joystick.Horizontal;
                //z = joystick.Vertical;
                x = Input.GetAxisRaw("Horizontal");
                z = Input.GetAxisRaw("Vertical");
                x = Mathf.Clamp(x, -1, 1);
                z = Mathf.Clamp(z, -1, 1);
                if (x == 0 && z == 0)
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
            
        }
        if (player.plState == playerStates.Loser || player.plState == playerStates.Winner)
        {
            timer += Time.deltaTime;
            if (timer > targetTime)
            {
                player.plState = playerStates.Stand;
                player.targetPl = null;
                player.life--;
            }
        }
    }
    private void FixedUpdate()
    {
        if (player.pv.IsMine)
        {
            player.rg.velocity = new Vector3(x, 0, z).normalized * player.speed;
            player.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            player.playerBody.transform.rotation = target;
        }
        if (player.isFighting)
        {
            player.rg.velocity = new Vector3(0, 0, 0);
        }
    }
    

}
