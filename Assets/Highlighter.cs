using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour {

    public bool hit = false;
    public float time = 0.1f;
    Renderer rend;
    Shader outline;
    Shader standard;
	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        outline = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
        standard = Shader.Find("Standard");
    }
	
	// Update is called once per frame
	void Update () {
        if(hit == true)
        {
            rend.material.shader = outline;
            time -= Time.deltaTime;
            if (time <= 0)
                hit = false;
        }
        else
            rend.material.shader = standard;
    }
}
