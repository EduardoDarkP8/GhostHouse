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
    Hidden,
    Stunned,
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
    public List<Light> lights = new List<Light>();
    public bool isStuning = false;
    public Color[] colors = new Color[2];
    public float stunTimer = 3.0f, stunTime;
    public int tagIndex;
    public Transform canvas;
    public GameObject[] trivias = new GameObject[3];
    public int life;

    private void Awake()

    {
        plState = playerStates.Stand;
        rg = GetComponent<Rigidbody>();
        tag = GameSettings.tags[tagIndex];
        playerBody.tag = tag;
        canvas = GameObject.Find("CanvasLocal").GetComponent<Transform>();
		if (tag == GameSettings.tags[0]) 
        {
            speed = 5.5f;
            gameObject.AddComponent<PlayerDash>();
            gameObject.GetComponent<PlayerDash>().player = this;
            GameObject gm = Instantiate(viewGhost, playerBody.transform.position, Quaternion.identity) as GameObject;
            gm.transform.SetParent(playerBody.transform);
            lights.Add(gm.GetComponent<Light>());
            gm.name = "ViewMesh";
            life = 1;
            
        }
        else if (tag == GameSettings.tags[1])
		{
            speed = 6.5f;
            gameObject.AddComponent<PlayerSalt>();
            gameObject.GetComponent<PlayerSalt>().player = this;
            gameObject.GetComponent<PlayerSalt>().saltPoint = saltPoint;
            gameObject.AddComponent<PlayerHide>();
            gameObject.GetComponent<PlayerHide>().player = this;
            GameObject gm = Instantiate(viewFlashLight, playerBody.transform.position, Quaternion.identity) as GameObject;
            gm.transform.SetParent(playerBody.transform);
            gm.name = "ViewMesh";
            lights.Add(gm.GetComponent<Light>());
            lights.Add(gm.transform.Find("Light").GetComponent<Light>());
            life = 2;
        }
		if (pv.IsMine) 
        {
            playerBody.GetComponent<MeshRenderer>().enabled = true;
            foreach (Light l in lights)
            {
                l.enabled = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pv.IsMine) 
        {
            cm.enabled = false;
            cm.gameObject.GetComponent<AudioListener>().enabled = false;
            if (plState == playerStates.Stunned) 
            {
                stunTimer += Time.deltaTime;
				if (stunTime >= stunTimer) 
                {
                    plState = playerStates.Stand;
                }
            }
            else if (plState != playerStates.Stunned) 
            {
                stunTime = 0;
            }
        }
    }
    [PunRPC]
    public void Stunning(PhotonMessageInfo info,PlayerSettings pl)
    {
        pl.plState = playerStates.Stunned;
    }
    [PunRPC]
    public void ChangeColor2(PhotonMessageInfo info)
    {
        foreach (Light lt in lights)
        {
            lt.color = colors[0];
        }
    }
    [PunRPC]
    public void ChangeColor1(PhotonMessageInfo info)
    {
        foreach (Light lt in lights)
        {
            lt.color = colors[0];
        }
    }
}
