using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public bool click;
    public void Click() 
    {
        click = true;
    }
    public void exitClick() 
    {
        click = false;
    }
}
