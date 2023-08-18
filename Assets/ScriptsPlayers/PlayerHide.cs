using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using Photon.Pun;
public class PlayerHide : MonoBehaviour
{
    public PlayerSettings player;
    public bool find;

    void Start()
    {

    }

    void Update()
    {

        if (Input.GetButtonUp("Interact") && player.plState == playerStates.Hidden)
        {
            StartCoroutine(exitCloset());
        }
        else if (Input.GetButtonUp("Interact") && player.plState != playerStates.Hidden)
        {
            StartCoroutine(findCloset());

        }

    }
    public void hide(Transform local, Closet cl)
    {
        if (player.plState != playerStates.Hidden && find)
        {
            player.plState = playerStates.Hidden;
            player.playerBody.GetComponent<Collider>().isTrigger = false;
            player.transform.position = new Vector3(local.position.x, transform.position.y, local.position.z);
            player.playerBody.GetComponent<MeshRenderer>().enabled = false;
            cl.players.Add(gameObject);


        }
    }
    public void getOut(Transform local)
    {
        if (player.plState == playerStates.Hidden)
        {

            player.transform.position += new Vector3(local.forward.x, 0, local.forward.z);
            player.playerBody.GetComponent<Collider>().isTrigger = false;
            player.playerBody.GetComponent<MeshRenderer>().enabled = true;
            player.plState = playerStates.Stand;
            find = false;
        }
    }
    IEnumerator findCloset()
    {
        find = true;
        yield return new WaitForSeconds(0.1f);
        if (player.plState != playerStates.Hidden)
        {
            find = false;
        }
    }
    IEnumerator exitCloset()
    {
        find = false;
        yield return new WaitForSeconds(0.5f);
    }
    private void OnTriggerStay(Collider other)
    {
        checkPlayer(other);
    }
    private void OnTriggerEnter(Collider other)
    {
        checkPlayer(other);
    }
    void checkPlayer(Collider collider)
    {
        if (collider.tag == "Closet")
        {

            GameObject gm = collider.gameObject;
            if (!collider.GetComponent<Closet>().players.Contains(gameObject))
            {
                hide(collider.transform, collider.GetComponent<Closet>());

            }
            else if (!find)
            {
                getOut(collider.transform);
            }
        }

    }
}
