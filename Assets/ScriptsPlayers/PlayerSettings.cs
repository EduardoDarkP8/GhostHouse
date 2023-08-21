using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum playerStates
{
    Stand,
    Walk,
    Salt,
    Dash,
    SeekCloset,
    Hidden
}
public class PlayerSettings : MonoBehaviour
{
    public Rigidbody rg;
    public GameObject playerBody;
    public float speed;
    public playerStates plState;
    public float dashForce;
    public Transform saltPoint;
    public GameObject salt;
    public PhotonView pv;
    public Camera cm;
    public GameObject viewFlashLight;
    public GameObject viewGhost;
	// Start is called before the first frame update
	private void Awake()
    {
        plState = playerStates.Stand;
        rg = GetComponent<Rigidbody>();
        tag = GameSettings.tags[1];
        playerBody.tag = tag;
		if (tag == GameSettings.tags[0]) 
        {
            speed = 5;
            gameObject.AddComponent<PlayerDash>();
            gameObject.GetComponent<PlayerDash>().player = this;
            GameObject gm = Instantiate(viewGhost, playerBody.transform.position, Quaternion.identity) as GameObject;
            gm.transform.SetParent(playerBody.transform);
            gm.name = "ViewMesh";
        }
        else if (tag == GameSettings.tags[1])
		{
            speed = 7;
            gameObject.AddComponent<PlayerSalt>();
            gameObject.GetComponent<PlayerSalt>().player = this;
            gameObject.GetComponent<PlayerSalt>().saltPoint = saltPoint;
            gameObject.AddComponent<PlayerHide>();
            gameObject.GetComponent<PlayerHide>().player = this;
            GameObject gm = Instantiate(viewFlashLight, playerBody.transform.position, Quaternion.identity) as GameObject;
            gm.transform.SetParent(playerBody.transform);
            gm.name = "ViewMesh";
        }
		if (pv.IsMine) 
        {
            playerBody.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine) 
        {
            cm.enabled = false;
            cm.gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }
}
