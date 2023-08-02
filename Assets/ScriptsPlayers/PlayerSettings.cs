using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum playerStates
{
    Stand,
    Walk,
    Salt,
    Dash
}
public class PlayerSettings : MonoBehaviour
{
    public Rigidbody rg;
    public GameObject playerBody;
    public float speed;
    public playerStates plState;
    public float dashForce;
    public Transform saltPoint;
    // Start is called before the first frame update
    void Start()
    {
        plState = playerStates.Stand;
        rg = GetComponent<Rigidbody>();
        tag = GameSettings.tags[Random.Range(0,2)];
        playerBody.tag = tag;
		if (tag == GameSettings.tags[0]) 
        {
            speed = 5;
            gameObject.AddComponent<PlayerDash>();
            gameObject.GetComponent<PlayerDash>().player = this;
        }
        else if (tag == GameSettings.tags[1])
		{
            speed = 7;
            gameObject.AddComponent<PlayerSalt>();
            gameObject.GetComponent<PlayerSalt>().player = this;
            gameObject.GetComponent<PlayerSalt>().saltPoint = saltPoint;
}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
