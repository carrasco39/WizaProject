using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeheaderTavern.Scripts.Core;

public class PhotonDebugTest : Photon.MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Debug.Log(GameManager.instance.name);
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }
}
