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
    public float powerTime = 3f, powerCount;
    public bool exit;

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
        }
        if (plState == playerStates.Stunned)
        {
            if (stunTime >= stunTimer)
            {
                plState = playerStates.Stand;
            }
            stunTime += Time.deltaTime;
        }
        else if (plState != playerStates.Stunned)
        {
            stunTime = 0;
        }
		if (plState == playerStates.Hidden) 
        {
            playerBody.GetComponent<Collider>().isTrigger = true;
            playerBody.GetComponent<MeshRenderer>().enabled = false;
            foreach (Light lt in lights)
            {
                lt.enabled = false;
            }
        }
        if (exit) 
        {
            playerBody.GetComponent<Collider>().isTrigger = false;
            playerBody.GetComponent<MeshRenderer>().enabled = true;
            foreach (Light lt in lights)
            {
                
                    lt.enabled = true;
                
            }
            exit = false;
            isStuning = true;
        }
        if (isStuning)
        {
            foreach (Light lt in lights)
            {
                lt.color = colors[1];
            }
            powerCount += Time.deltaTime;
            if (powerCount >= powerTime)
            {
                isStuning = false;
                powerCount = 0;
            }
            
        }
        else if (!isStuning && lights[0].color != colors[0])
        {
            foreach (Light lt in lights)
            {
                lt.color = colors[0];
            }
        }
    }
        [PunRPC]
        public void Stun(PhotonMessageInfo info)
        {
            plState = playerStates.Stunned;
        }

}   
