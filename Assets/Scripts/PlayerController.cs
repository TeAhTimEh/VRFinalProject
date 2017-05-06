using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public Transform cam;
    public Transform center;
    public Canvas cursor;
    public Transform spotlight1, spotlight2;
    public Transform startPos;
 
    public AudioClip Up, Down, Left, Right, XButt, solved, error, timedOut;
    public songPlayer sp;
    public openUp toLvl2, toLvl3, toOutside;
    public GameObject lightning1, lightning2, lightning3;
    public GameObject myPlatform;

    Vector3 dir;
    Renderer rend;
    //LineRenderer lineRenderer;
    AudioSource aus;


    private Light sl1, sl2;
    private static int[] songOfStorms = { 5, 2, 8, 5, 2, 8 };
    private static int[] zeldasLullaby = { 4, 8, 6, 4, 8, 6 };
    private static int[] sariasSong = { 2, 6, 4, 2, 6, 4 };
    private static int[] songOfTime = { 6, 5, 2, 6, 5, 2 };
    private static int[] sunsSong = { 6, 2, 8, 6, 2, 8 };
    private static int[] eponasSong = { 8, 4, 6, 8, 4, 6 };
    private int[] currentNotes = { 0, 0, 0, 0, 0, 0 };
    private int[][] songs = { zeldasLullaby, sunsSong, songOfStorms, eponasSong, songOfTime, sariasSong };
    private int index = 0;
    private bool dpadUDAxis = false, dpadLRAxis = false;
    private float delay = 0;
    private bool onMagicPlat = true;
    private int level = 0;
    private bool inStartMenu = true;
    private GameObject Rlabel, Llabel;
    
    // Use this for initialization
    void Start () {
        //lineRenderer = GetComponent<LineRenderer>();
        aus = GetComponent<AudioSource>();
        sl1 = spotlight1.GetComponentInChildren<Light>();
        sl2 = spotlight2.GetComponentInChildren<Light>();
        Rlabel = spotlight1.FindChild("R").gameObject;
        Llabel = spotlight2.FindChild("L").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        dir = cam.forward;

        /*Ray ray = new Ray(cam.transform.position, dir);*/
        RaycastHit raycast;
        //Vector3 endPos = cam.transform.position + (200 * dir);

        float thickness = 0.2f; //<-- Desired thickness here.
        Vector3 origin = cam.transform.position;
        //Vector3 direction = transform.TransformDirection(Vector3.forward);
        

        /*if (Physics.Raycast(ray, out raycast, 200))
        {*/
        if(Physics.SphereCast(origin, thickness, dir, out raycast, 200.0f))
        { 
            //endPos = raycast.point;
            if (myPlatform != raycast.collider.gameObject && (raycast.collider.tag == "Platform" || raycast.collider.tag == "MagicPlatform"))
            {
                Highlighter h = raycast.transform.gameObject.GetComponent<Highlighter>();
                h.time = 0.1f;
                h.hit = true;
                print(raycast.transform.position);
                if (Input.GetButtonDown("Fire1"))
                {
                    move(raycast.transform);
                    myPlatform = raycast.collider.gameObject;
                    if (raycast.collider.tag == "MagicPlatform")
                        onMagicPlat = true;
                    else
                        onMagicPlat = false;
                }
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            cursor.enabled = !cursor.enabled;
        }

        if (Input.GetButtonDown("LeftBumper"))
        {
            placeLight(1);
        }
        if (Input.GetButtonDown("RightBumper"))
        {
            placeLight(2);
        }
        if(onMagicPlat)
        {
            if (Input.GetAxis("D-PadUpDown") > 0 && !dpadUDAxis)
            {
                aus.clip = Up;
                aus.Play();
                currentNotes[index] = 8;
                index++;
                delay = Time.time + 2.0f;
                dpadUDAxis = true;
            }
            if (Input.GetAxis("D-PadUpDown") < 0 && !dpadUDAxis)
            {
                aus.clip = Down;
                aus.Play();
                currentNotes[index] = 2;
                index++;
                delay = Time.time + 2.0f;
                dpadUDAxis = true;
            }
            if (Input.GetAxis("D-PadUpDown") == 0)
            {
                dpadUDAxis = false;
            }
            if (Input.GetAxis("D-PadLeftRight") < 0 && !dpadLRAxis)
            {
                aus.clip = Left;
                aus.Play();
                currentNotes[index] = 4;
                index++;
                delay = Time.time + 2.0f;
                dpadLRAxis = true;
            }
            if (Input.GetAxis("D-PadLeftRight") > 0 && !dpadLRAxis)
            {
                aus.clip = Right;
                aus.Play();
                currentNotes[index] = 6;
                index++;
                delay = Time.time + 2.0f;
                dpadLRAxis = true;
            }
            if (Input.GetAxis("D-PadLeftRight") == 0)
            {
                dpadLRAxis = false;
            }
            if (Input.GetButtonDown("Fire3"))
            {
                aus.clip = XButt;
                aus.Play();
                currentNotes[index] = 5;
                index++;
                delay = Time.time + 2.0f;
            }
        } 
        
        if (Input.GetButtonDown("Submit") && level > 0)
        {
            Vector3 save = transform.position;
            transform.position = startPos.position;
            startPos.position = save;
            inStartMenu = !inStartMenu;
            if(!inStartMenu)
            {
                Rlabel.SetActive(true);
                Llabel.SetActive(true);
                setSpotLightAngles(45);
            }
            else
            {
                Rlabel.SetActive(false);
                Llabel.SetActive(false);
                setSpotLightAngles(60);
            }
        }
        if (index == 6)
        {
            checkSong();
            index = 0;
        }
        if (index != 0 && Time.time > delay)
        {
            index = 0;
            aus.clip = timedOut;
            aus.Play();
        }

        /////http://jegged.com/Games/Legend-of-Zelda-Ocarina-of-Time/Ocarina-Songs/
    }
    
    public void setFacing()
    {
        transform.LookAt(new Vector3(center.position.x, transform.position.y, center.position.z));
    }
    void move(Transform platform)
    {
        transform.position = new Vector3(platform.position.x, platform.position.y + 2.0f, platform.position.z);
        //transform.LookAt(new Vector3(center.position.x, transform.position.y, center.position.z));
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
    void checkSong()
    {
        bool foundSong = false;
        int i = 0;
        for (; i < songs.Length; i++)
        {
            for(int j = 0; j < songs[i].Length; j++)
            {
                if(songs[i][j] != currentNotes[j])
                {
                    break;
                }
                if(j == 5)
                {
                    foundSong = true;
                    //break;
                }
            }
            if(foundSong)
            {
                break;
            }
        }
        if(foundSong)
        {
            if(level == 0 && i == 2)
            {
                aus.clip = solved;
                aus.Play();
                sp.playSong(i);
                level++;
                Vector3 save = transform.position;
                transform.position = startPos.position;
                startPos.position = save;
                setSpotLightAngles(45);
                inStartMenu = !inStartMenu;
                Rlabel.SetActive(true);
                Llabel.SetActive(true);
            }
            else if (level == 1 && i == 5)
            {
                aus.clip = solved;
                aus.Play();
                sp.playSong(i);
                toLvl2.open = true;
                level++;
                lightning1.SetActive(false);
                lightning3.SetActive(true);
                moveSoundPosition(0, 36, 0);
            }
            else if (level == 2 && i == 0)
            {
                aus.clip = solved;
                aus.Play();
                sp.playSong(i);
                toLvl3.open = true;
                level++;
                lightning2.SetActive(false);
                moveSoundPosition(0, 30, 0);
            }
            else if (level == 3 && i == 4)
            {
                aus.clip = solved;
                aus.Play();
                sp.playSong(i);
                toOutside.open = true;
                level++;
            }
            else if (i == 3 || i == 1)
            {
                aus.clip = solved;
                aus.Play();
                sp.playSong(i);
                //toLvl3.open = true;
                //level++;
            }
            else
            {
                aus.clip = error;
                aus.Play();
            }
            //else if (i == )
            //toLvl2.open = true;
            //toLvl3.open = true;
        }
        else
        {
            aus.clip = error;
            aus.Play();
        }
    }
    void moveSoundPosition(float x, float y, float z)
    {
        sp.transform.position = new Vector3(sp.transform.position.x + x, sp.transform.position.y + y, sp.transform.position.z + z);
    }

    void setSpotLightAngles(float angle)
    {
        sl1.spotAngle = angle;
        sl2.spotAngle = angle;
    }

}
