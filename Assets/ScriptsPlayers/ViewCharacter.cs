using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ViewCharacter : MonoBehaviour
{
    ViewArea va;
    public PlayerSettings pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = gameObject.transform.parent.parent.GetComponent<PlayerSettings>();
        va = GetComponent<ViewArea>();
    }

    // Update is called once per frame
    void Update()
    {

          
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Survival" || other.tag == "Ghost")
        {
            if (other.GetComponent<TipeOfView>() == null)
            {
                return;
            }
            else
            {

                TipeOfView tov = other.GetComponent<TipeOfView>();
                if (tov != null)
                {
                    if (pl.isStuning)
                    {
                        tov.viewCharacter.pl.pv.RPC("Stun", RpcTarget.All);
                    }
                    if (tov.viewCharacter.pl.plState != playerStates.Hidden)
                    {
                        other.GetComponent<TipeOfView>().TurnOn();
                    }
                }
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Survival" || other.tag == "Ghost")
        {
            if (other.GetComponent<TipeOfView>() == null)
            {
                return;
            }
            else
            {

                TipeOfView tov = other.GetComponent<TipeOfView>();
                if (tov != null)
                {
                    if (pl.isStuning)
                    {
                        tov.viewCharacter.pl.pv.RPC("Stun", RpcTarget.All);
                    }
                    if (tov.viewCharacter.pl.plState != playerStates.Hidden)
                    {
                        other.GetComponent<TipeOfView>().TurnOn();
                    }
                }
            }

        }
    }





}
