using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Reference for GUI button https://www.youtube.com/watch?v=Mn6lUik3nyk

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]

public class ColourChange : MonoBehaviour {

    [SerializeField] MeshRenderer target = null;
    SpriteRenderer srend;
    
    private void Awake()
    {
        srend = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        //Debug.Log(gameObject.name);
        target.material.color = srend.color;
    }
}

