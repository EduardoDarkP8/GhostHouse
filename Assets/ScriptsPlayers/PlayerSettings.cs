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
    Loser,
    Waiting
}
public class PlayerSettings : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audios;
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
    public float canFightTime, canFightTimer = 3f;
    public TriviaManager triviaManager;
    public bool isFighting;
    public Animator anima;
    public GameObject dashButton;
    public GameObject saltButton;
    public GameObject hideButton;
    public DoorButton[] buttons;
    public bool inLimit;
    private void Awake()
    {
        plState = playerStates.Stand;
        audioSource = GetComponent<AudioSource>();
        rg = GetComponent<Rigidbody>();
        tag = GameSettings.tags[tagIndex];
        playerBody.tag = tag;
        canFightTime = canFightTimer;
        canFight = true;
        canvas = GameObject.Find("CanvasLocal").GetComponent<Transform>();
        if (tag == GameSettings.tags[0])
        {
            speed = 5.5f;
            gameObject.AddComponent<PlayerDash>();
            gameObject.GetComponent<PlayerDash>().player = this;
            
			if (pv.IsMine)
			{
                buttons = new DoorButton[1];
                GameObject btn = Instantiate(dashButton, canvas.transform);
                buttons[0] = btn.GetComponent<DoorButton>();
			}
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
            if (pv.IsMine)
            {
                buttons = new DoorButton[2];
                GameObject btn = Instantiate(hideButton, canvas.transform);
                buttons[0] = btn.GetComponent<DoorButton>();
                btn = Instantiate(saltButton, canvas.transform);
                buttons[1] = btn.GetComponent<DoorButton>();
            }
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
        
        if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["TimesUp"])
        {
            gameOver = true;
        }
        if (gameOver) 
        {
            if (pv.IsMine)
            {
				if (gameObject.tag == "Survival") 
                {
					if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["TimesUp"])
                    {
                        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Lose").gameObject.SetActive(true);
                    }
                    else if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["TimesUp"])
                    {
                        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Win").gameObject.SetActive(true);
                    }
                }
                else if (gameObject.tag == "Ghost") 
                {
                    if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["TimesUp"])
                    {
                        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Lose").gameObject.SetActive(true);
                    }
                    else if (!(bool)PhotonNetwork.CurrentRoom.CustomProperties["TimesUp"])
                    {
                        GameObject.FindGameObjectWithTag("Canvas").transform.Find("Win").gameObject.SetActive(true);
                    }
                }
                Destroy(GameObject.FindGameObjectWithTag("Trivia"));
                pv.RPC("destroyBody", RpcTarget.All);
            }

        }
        else if (!gameOver)
		{

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
        
        if (plState == playerStates.Loser || plState == playerStates.Winner || plState == playerStates.Fight)
        {
            isFighting = true;
            canFight = false;
        }
        else 
        {
            isFighting = false;
        }
        if (!isFighting) 
        {
			if (!canFight) 
            {
                canFightTime += Time.deltaTime;
				if (canFightTime >= canFightTimer) 
                {
                    canFight = true;
                    canFightTime = 0;
                }
            }
        }
            if (plState == playerStates.Loser || plState == playerStates.Winner)
            {
                if (tag == "Survival")
                {
                    if (plState == playerStates.Winner)
                    {
                        isStuning = true;
                        if (life <= 0)
                        {
                            gameOver = true;
                            targetPl.gameOver = true;
                            StartCoroutine(TurnDestroyOne(0.5f));
                            StartCoroutine(targetPl.TurnDestroyOne(0.5f));
                        }
                        plState = playerStates.Stand;
                        targetPl.pv.RPC("Stun", RpcTarget.All);
                    }
                }
                if (tag == "Ghost" && targetPl.life <= 0)
                {
                    gameOver = true;
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

        if (!isStuning && lights[0].color != colors[0])
        {
            foreach (Light lt in lights)
            {
                lt.color = colors[0];
            }
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
            targetPl.plState = playerStates.Winner;
            if (gameObject.tag == "Survival") 
            {
                gameOver = true;
            }
        }
        [PunRPC]
        public void Fight(PhotonMessageInfo info)
        {
            plState = playerStates.Fight;
            StartCoroutine(SurvivalDelay());
        }
        IEnumerator SurvivalDelay() 
        {
            yield return new WaitForSeconds(0.1f);
            if (gameObject.tag == "Survival")
            {
                life--;
            }
        }
        [PunRPC]
        public void Stand(PhotonMessageInfo info)
        {
            plState = playerStates.Stand;
        }
    IEnumerator TurnDestroyOne(float t) 
    {
        yield return new WaitForSeconds(t);
        gameOver = true;
    }
	[PunRPC]
	public void destroyBody()
	{
		if (playerBody != null) 
        {
            lights = new List<Light>();
            Destroy(GetComponent<PlayerMovement>());
            Destroy(playerBody);
		    if (GetComponent<PlayerHide>() != null)
		    {
            Destroy(GetComponent<PlayerHide>());
            Destroy(GetComponent<PlayerSalt>());
            }
            if (GetComponent<PlayerDash>() != null)
            {
                Destroy(GetComponent<PlayerDash>());
            }

        }
	}
    public void playAudio(int value)
	{
        audioSource.clip = audios[value];
        audioSource.Play();
	}
}   
