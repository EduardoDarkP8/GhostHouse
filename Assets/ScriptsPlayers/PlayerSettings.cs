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
	// Start is called before the first frame update

    private void Awake()

    {
        plState = playerStates.Stand;
        rg = GetComponent<Rigidbody>();
        tag = GameSettings.tags[1];
        playerBody.tag = tag;
		if (tag == GameSettings.tags[0]) 
        {
            speed = 4;
            gameObject.AddComponent<PlayerDash>();
            gameObject.GetComponent<PlayerDash>().player = this;
            GameObject gm = Instantiate(viewGhost, playerBody.transform.position, Quaternion.identity) as GameObject;
            gm.transform.SetParent(playerBody.transform);
            gm.name = "ViewMesh";
            
            
        }
        else if (tag == GameSettings.tags[1])
		{
            speed = 6;
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
    [PunRPC]
    public void Stunning(PhotonMessageInfo info)
    {
        List<TipeOfView> tipeOfViews = playerBody.GetComponent<TipeOfView>().viewCharacter.tipeOfViews;
        if (tipeOfViews.Count > 0)
        {
            foreach (TipeOfView view in tipeOfViews)
            {
                if (view.gameObject.tag == "Ghost")
                {
                    view.viewCharacter.pl.plState = playerStates.Stunned;
                }
            }
        }
    }
}
