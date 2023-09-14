using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public bool used;
    public Transform jumpPoint;
    public bool isUsing = false;
    public Light light;
}
