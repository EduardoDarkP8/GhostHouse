using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorButton : MonoBehaviour
{
    public bool click;
    public bool drag;
    public void Click() 
    {
		if (!drag) 
        {
            click = true;
        }
        drag = true;
    }
    public void exitClick() 
    {
        click = false;
        drag = false;
    }
}
