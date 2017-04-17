using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform cam;
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

            }
        }
        //lineRenderer.SetPosition(0, cam.transform.position);
        //lineRenderer.SetPosition(1, endPos);
    }

}
