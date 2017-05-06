using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openUp : MonoBehaviour {

    public bool open = false;
    Vector3 dest;
    private bool reachedDest = false;

	// Use this for initialization
	void Start () {
        dest = new Vector3(transform.position.x-51, transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	    if(open && !reachedDest)
        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);
            setInBounds(transform.position, dest);
        }
	}
    void setInBounds(Vector3 obj, Vector3 des)
    {
        if (Vector3.Distance(obj, des) <= 1)
            reachedDest = true;
    }
}
