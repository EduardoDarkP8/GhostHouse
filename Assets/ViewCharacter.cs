using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ViewCharacter : MonoBehaviour
{
    ViewArea va;
    public PlayerSettings pl;
    public float powerTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        pl = gameObject.transform.parent.parent.GetComponent<PlayerSettings>();
        va = GetComponent<ViewArea>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pl.isStuning)
        {
            pl.pv.RPC("ChangeColor2", RpcTarget.All);
            //pl.pv.RPC("Stunning", RpcTarget.All);
        }
        else if (!pl.isStuning && pl.lights[0].color != pl.colors[0])
        {
            pl.pv.RPC("ChangeColor1", RpcTarget.All);
        }
                  
    }
    
    IEnumerator StunTimer() 
    {
        pl.isStuning = true;
        yield return new WaitForSeconds(powerTime);
        pl.isStuning = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Survival" || other.tag == "Ghost")
        {
            if (other.GetComponent<TipeOfView>())
            {
                other.GetComponent<TipeOfView>().TurnOn();
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Survival" || other.tag == "Ghost")
        {
            if (other.GetComponent<TipeOfView>())
            {
                other.GetComponent<TipeOfView>().TurnOn();
            }
        }
    }





}
