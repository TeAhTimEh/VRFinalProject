using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blinkHalf : MonoBehaviour {

   /* public bool moveDown = false;

   /* private Image im;
    private bool startMoving =  false;
    private bool moveBack = false;
    private Vector3 dest;
    private Vector3 origin;

	// Use this for initialization
	void Start () {
        im = GetComponent<Image>();
        im.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		/*if(Input.GetButtonDown("Jump"))
        {
            im.enabled = true;
            startMoving = true;
            if (moveDown)
            {
                dest = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
                origin = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
            }
            
            else
            {
                dest = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
                origin = new Vector3(transform.position.x, transform.position.y - 10, transform.position.z);
            }
        }

        if (startMoving)
        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime *20 );
            if(Mathf.Abs(transform.position.y - dest.y) < 0.5)
            {
                moveBack = true;
                startMoving = false;
            }
        }
        if (moveBack)
        {
            transform.position = Vector3.Lerp(transform.position, origin, Time.deltaTime *20 );
            if (Mathf.Abs(transform.position.y - origin.y) < 0.5)
            {
                moveBack = false;
                startMoving = false;
                im.enabled = false;
            }
        }}*/
    
}
