using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public class Timer : MonoBehaviour
{
    public float time = 120;
    public PhotonView pv;
    public Text txt;
    ExitGames.Client.Photon.Hashtable setTime = new ExitGames.Client.Photon.Hashtable();
    void Start()
    {
        pv = GetComponent<PhotonView>();
        txt = GetComponent<Text>();
    }
    void Update()
    {
        txt.text = PhotonNetwork.CurrentRoom.CustomProperties["Time"].ToString();
        timer();
    }
    public void timer() 
    {
		if (PhotonNetwork.IsMasterClient && (bool)PhotonNetwork.CurrentRoom.CustomProperties["StartMatch"]) 
        {
            time -= Time.deltaTime;
            setTime["Time"] = (int)time;
            PhotonNetwork.CurrentRoom.SetCustomProperties(setTime);
            if((int)PhotonNetwork.CurrentRoom.CustomProperties["Time"] <= 0)
			{
                setTime["StartMatch"] = false;
                setTime["TimesUp"] = true;
			}

        }
    }
}
