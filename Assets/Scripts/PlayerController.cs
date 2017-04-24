using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform cam;
    public Transform center;
    public Canvas cursor;
    public Transform spotlight1, spotlight2;
 

    Vector3 dir;
    Renderer rend;
    LineRenderer lineRenderer;

    
	// Use this for initialization
	void Start () {
        lineRenderer = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        dir = cam.forward;

        Ray ray = new Ray(cam.transform.position, dir);
        RaycastHit raycast;
        Vector3 endPos = cam.transform.position + (200 * dir);

        if (Physics.Raycast(ray, out raycast, 200))
        {
            endPos = raycast.point;
            if (raycast.collider.tag == "Platform")
            {
                Highlighter h = raycast.transform.gameObject.GetComponent<Highlighter>();
                h.time = 0.1f;
                h.hit = true;
                print(raycast.transform.position);
                if (Input.GetButtonDown("Fire1"))
                {
                    move(raycast.transform);
                }
            }
        }

        if(Input.GetButtonDown("Fire2"))
        {
            cursor.enabled = !cursor.enabled;
        }
        
        if(Input.GetButtonDown("LeftBumper"))
        {
            placeLight(1);
        }
        if (Input.GetButtonDown("RightBumper"))
        {
            placeLight(2);
        }



    }

    void move(Transform platform)
    {
        transform.position = new Vector3(platform.position.x, platform.position.y + 2.0f, platform.position.z);
        transform.LookAt(new Vector3(center.position.x, transform.position.y, center.position.z));
    }

    void placeLight(int light)
    {
        Vector3 lightPos = new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);
        if(spotlight1.position != lightPos && spotlight2.position != lightPos)
        {
            if (light == 1)
            {
                spotlight1.gameObject.SetActive(true);
                spotlight1.position = lightPos;
                spotlight1.rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);
            }
            else
            {
                spotlight2.gameObject.SetActive(true);
                spotlight2.position = lightPos;
                spotlight2.rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);

            }
        }
        else if(spotlight1.position == lightPos && light == 1)
        {
            spotlight1.position = Vector3.zero;
            spotlight1.gameObject.SetActive(false);
        }
        else if (spotlight2.position == lightPos && light == 2)
        {
            spotlight2.position = Vector3.zero;
            spotlight2.gameObject.SetActive(false);
        }
    }

}
