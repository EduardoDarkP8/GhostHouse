using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ViewCharacter : MonoBehaviour
{
    public PlayerSettings pl;
    public TipeOfView tipeOfView;
    // Start is called before the first frame update
    void Start()
    {
        pl = gameObject.transform.parent.parent.GetComponent<PlayerSettings>();
        tipeOfView = transform.parent.GetComponent<TipeOfView>();
    }

    // Update is called once per frame
    void Update()
    {

          
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
		if (pl != null && tipeOfView!= null)
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
		if (other.GetComponent<ViewCharacter>() != null) 
        {
            ViewCharacter viewCharacter = other.GetComponent<ViewCharacter>();
            if (viewCharacter != null)
            {
                if (pl.isStuning)
                {
                    viewCharacter.pl.pv.RPC("Stun", RpcTarget.All);
                }
                if (viewCharacter.pl.plState != playerStates.Hidden)
                {
                    other.GetComponent<ViewCharacter>().tipeOfView.TurnOn();
                }
            }
        }
        
		}

    }
    private void OnTriggerStay(Collider other)
    {
        if (pl != null && tipeOfView != null)
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
            if (other.GetComponent<ViewCharacter>() != null)
            {
                ViewCharacter viewCharacter = other.GetComponent<ViewCharacter>();
                if (viewCharacter != null)
                {
                    if (pl.isStuning)
                    {
                        viewCharacter.pl.pv.RPC("Stun", RpcTarget.All);
                    }
                    if (viewCharacter.pl.plState != playerStates.Hidden)
                    {
                        other.GetComponent<ViewCharacter>().tipeOfView.TurnOn();
                    }
                }
            }

        }

    }

}
