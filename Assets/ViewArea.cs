using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewArea : MonoBehaviour
{
    public Material visionConeMaterial;
    public float visionRange;
    public float visionAngle;
    public LayerMask visionObstructingLayer;
    public LayerMask body;
    public int visionConeResolution = 60;
    Mesh visionConeMesh;
    MeshFilter meshFilter_;
    ViewCharacter vc;
    
    void Start()
    {
        vc = GetComponent<ViewCharacter>();
        transform.AddComponent<MeshRenderer>().material = visionConeMaterial;
        meshFilter_ = transform.AddComponent<MeshFilter>();
        visionConeMesh = new Mesh();
        visionAngle *= Mathf.Deg2Rad;
    }


    void Update()
    {
		if (vc.pl.pv.IsMine) 
        {
            DrawVisionCone();
        }        
    }

    void DrawVisionCone()//this method creates the vision cone mesh
    {
        int[] triangles = new int[(visionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[visionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -visionAngle / 2;
        float angleIcrement = visionAngle / (visionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < visionConeResolution; i++)
        {
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);
            float visibility = 1.0f;
            if (Currentangle < -Mathf.PI / 4.0f || Currentangle > Mathf.PI / 4.0f)
            {
                float normalizedAngle = Mathf.Abs(Currentangle) - Mathf.PI / 4.0f;
                visibility = Mathf.Clamp01(1.0f - normalizedAngle * 2.0f);
            }
            RaycastHit hit;
            RaycastHit hit2;
            if (Physics.Raycast(transform.position, RaycastDirection, out hit, visionRange, visionObstructingLayer))
            {
                Vertices[i + 1] = VertForward * hit.distance * visibility;
            }
            else if (Physics.Raycast(transform.position, RaycastDirection, out hit2, visionRange, body)) 
            {
				if (hit2.collider.gameObject.GetComponent<TipeOfView>()) 
                {
                    hit2.collider.gameObject.GetComponent<TipeOfView>().spotLook = true;
                    hit2.collider.gameObject.GetComponent<TipeOfView>().timer = 0;
                    if (!vc.tipeOfViews.Contains(hit2.collider.gameObject.GetComponent<TipeOfView>())) 
                    {
                        vc.tipeOfViews.Add(hit2.collider.gameObject.GetComponent<TipeOfView>());
                    }
                    Vertices[i + 1] = VertForward * hit2.distance * visibility;
                }
				else 
                {
                    Vertices[i + 1] = VertForward * hit2.distance * visibility;
                }
            }
            else
            {
                Vertices[i + 1] = VertForward * visionRange * visibility;
            }


            Currentangle += angleIcrement;
        }
        List<TipeOfView> tipeOfViewsToRemove = new List<TipeOfView>();

        foreach (TipeOfView tp in vc.tipeOfViews)
        {
            if (tp.spotLook == true)
            {
                vc.addMesh(tp.meshRenderer);
            }
            else if(tp.spotLook == false)
            {
                vc.removeMesh(tp.meshRenderer);
                tipeOfViewsToRemove.Add(tp);
            }
        }

        foreach (TipeOfView tpToRemove in tipeOfViewsToRemove)
        {
            vc.tipeOfViews.Remove(tpToRemove);
        }
        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }
        
    }


}
