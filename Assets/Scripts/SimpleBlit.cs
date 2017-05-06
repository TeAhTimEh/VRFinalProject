using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SimpleBlit : MonoBehaviour
{
    
    public Material TransitionMaterial;
    public PlayerController pc;

    private float blinkVal = 0;
    private bool start = false;
    private bool goDown = true;

    void Awake()
    {
        blinkVal = 0;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
       if (TransitionMaterial != null)
        {   
            Graphics.Blit(src, dst, TransitionMaterial);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            start = true;
        }
        if (start && goDown)
        {
            blinkVal = blinkVal + 0.1f;
            TransitionMaterial.SetFloat("_Fade", blinkVal);
            if(blinkVal >= 1.2f)
            {
                pc.setFacing();
                goDown = !goDown;
            }
        }
        else if (start && !goDown)
        {
            blinkVal = blinkVal - 0.1f;
            TransitionMaterial.SetFloat("_Fade", blinkVal);
            if (blinkVal <= 0)
            {
                goDown = !goDown;
                start = false;
            }
        }
    }
}
