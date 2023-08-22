using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ViewCharacter : MonoBehaviour
{
    ViewArea va;
    public PlayerSettings pl;
    public List<TipeOfView> tipeOfViews = new List<TipeOfView>();
    public float timer, powerTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        pl = gameObject.transform.parent.parent.GetComponent<PlayerSettings>();
        va = GetComponent<ViewArea>();
    }

    // Update is called once per frame
    void Update()
    {
        //pl.pv.RPC("Stunning",RpcTarget.All);
    }
    [PunRPC]
    void Stunning(PhotonMessageInfo info) 
    {
        timer = powerTime;
        if (pl.isStuning)
        {
            timer = 0;
        }
        else if (timer < powerTime)
        {
            foreach (Light lt in pl.lights)
            {
                lt.color = pl.colors[1];
            }
            foreach (TipeOfView view in tipeOfViews)
            {
                if (view.gameObject.tag == "Ghost")
                {
                    view.viewCharacter.pl.plState = playerStates.Stunned;
                }
            }
            timer++;
        }
        else if (timer >= powerTime && pl.isStuning)
        {

            foreach (Light lt in pl.lights)
            {
                lt.color = pl.colors[0];
            }
            pl.isStuning = false;

        }
    }
    public void addMesh(MeshRenderer newMesh)
    {       
        newMesh.enabled = true;
    }
    public void removeMesh(MeshRenderer newMesh)
    {
        newMesh.enabled = false;
    }
   
        
    
    
}
