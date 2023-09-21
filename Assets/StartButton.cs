using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class StartButton : MonoBehaviour
{
    PhotonView pv;
    ExitGames.Client.Photon.Hashtable startMatch = new ExitGames.Client.Photon.Hashtable();
	private void Update()
	{
		if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["StartMatch"]) 
        {
            Destroy(gameObject);
        }
	}
	public void startMatchButton() 
    {
        startMatch["StartMatch"] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(startMatch);
       
    }
}
