using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitApplication : MonoBehaviour
{
    
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("c�k�ld�");
        }
    }
}
