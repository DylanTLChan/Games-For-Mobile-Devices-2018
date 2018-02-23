using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touchable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Select()
    {
      GetComponent<Renderer>().material.color = Color.green;
    }

    internal void DeSelect()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
