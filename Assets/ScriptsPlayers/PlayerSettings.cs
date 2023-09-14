using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public enum playerStates
{
    Stand,
    Walk,
    Salt,
    Dash,
    SeekCloset,
    Hidden,
    Stunned,
    Fight,
    Winner,
    Loser
}
public class PlayerSettings : MonoBehaviour
{
    public Rigidbody rg;
    public GameObject playerBody;
    public float speed;
    public float bonuSpeed;
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
    public PlayerSettings targetPl;
    public bool canFight;
    public bool change;
    public bool gameOver;
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
            bonuSpeed = speed;
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
            playerBody.transform.Find("Mesh").gameObject.SetActive(true);
            foreach (Light l in lights)
            {
                l.enabled = true;
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
		if (gameOver) 
        {
            if (pv.IsMine)
            {
                PhotonNetwork.LeaveRoom();
                ChangeScene.changeScene("Start");
            }
            PhotonNetwork.Destroy(pv);
			
        }
		if (change) 
        {
		if (plState == playerStates.Fight) 
        {
            pv.RPC("Fight", RpcTarget.All);
        }
        else if (plState == playerStates.Loser) 
        {
            pv.RPC("Lose", RpcTarget.All);
        }
        else if (plState == playerStates.Winner)
        {
            pv.RPC("Win", RpcTarget.All);
        }
            change = false;
        }
        if (plState != playerStates.Loser || plState != playerStates.Winner || plState != playerStates.Fight) 
        {
			if (!canFight) 
            {
                StartCoroutine(CanFight(2f));
            }
        }
        if (plState == playerStates.Loser || plState == playerStates.Winner)
        {
			if (tag == "Survival") 
            { 
            if(plState == playerStates.Winner)
			{
                isStuning = true;
					if (life <= 0) 
                    {
                        gameOver = true;
                    }
            }
            else if (plState == playerStates.Loser)
            {
            isStuning = true;
                    if (life <= 0)
                    {
                        targetPl.gameOver = true;
                        StartCoroutine(TurnDestroyOne(0.5f));
                    }
            }

            }
            if (tag == "Ghost")
            {
				if (targetPl.plState == playerStates.Loser || plState == playerStates.Winner) 
                {
                    targetPl.gameOver = true;

                    StartCoroutine(TurnDestroyOne(0.5f));
                }
				else 
                {
                    pv.RPC("Stun", RpcTarget.All);
                }
            }
        }
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
            playerBody.transform.Find("Mesh").gameObject.SetActive(false);
            foreach (Light lt in lights)
            {
                lt.enabled = false;
            }
        }
        if (exit) 
        {
            playerBody.GetComponent<Collider>().isTrigger = false;
            playerBody.transform.Find("Mesh").gameObject.SetActive(true);
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
            speed = bonuSpeed + 2;
            powerCount += Time.deltaTime;
            if (powerCount >= powerTime)
            {
                isStuning = false;
                speed = bonuSpeed;
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
        [PunRPC]
        public void Win(PhotonMessageInfo info)
        {
            plState = playerStates.Winner;
        }
        [PunRPC]
        public void Lose(PhotonMessageInfo info)
        {
            plState = playerStates.Loser;
        }
        [PunRPC]
        public void Fight(PhotonMessageInfo info)
        {
            plState = playerStates.Fight;
        }
        [PunRPC]
        public void Stand(PhotonMessageInfo info)
        {
            plState = playerStates.Stand;
        }
    IEnumerator CanFight(float t) 
    {
        yield return new WaitForSeconds(t);
        canFight = true;
    }
    IEnumerator TurnDestroyOne(float t) 
    {
        yield return new WaitForSeconds(t);
        gameOver = true;
    }
}   
