using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Salt : MonoBehaviour
{
    public GameObject gm;
    public PhotonView pv;
    public PlayerSalt ps;
    public MeshRenderer ms;
    void Start()
    {
        pv = GetComponent<PhotonView>();
        StartCoroutine(destroy());
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
            ms.enabled = false;
            velocityDegree(gm.GetComponent<PlayerSettings>());
		}
	}
    void velocityDegree(PlayerSettings player) 
    {
        player.pv.RPC("Stun",RpcTarget.All);
        ps.DestroySalt(gameObject);
        
    }
    IEnumerator destroy() 
    {
        yield return new WaitForSeconds(5f);
        print("AAA");
        PhotonNetwork.Destroy(pv);

    }

}
