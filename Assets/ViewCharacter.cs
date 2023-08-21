using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void addMesh(MeshRenderer newMesh)
    {       
        newMesh.enabled = true;
        StartCoroutine(remover(newMesh));
    }
    public void removeMesh(MeshRenderer newMesh)
    {
        newMesh.enabled = false;
    }
    IEnumerator remover(MeshRenderer newMesh)
    {
        yield return new WaitForSeconds(2f);
        removeMesh(newMesh);
    }
    
}
