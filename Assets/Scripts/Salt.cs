using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Salt : MonoBehaviour
{
    public GameObject gm;
    public PhotonView pv;
    public PlayerSalt ps;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ghost")
		{
            gm = other.gameObject.transform.parent.gameObject;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
            velocityDegree(gm.GetComponent<PlayerSettings>());
		}
	}
    void velocityDegree(PlayerSettings player) 
    {
        player.pv.RPC("Stun",RpcTarget.All);
        ps.DestroySalt(gameObject);
        
    }

}
