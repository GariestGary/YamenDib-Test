using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumeBox.Toolbox;

public class ParallaxScroller : MonoCached
{
    [SerializeField] private float parallaxEffect;
    
    private float startPos;
    private float length;

    public bool Scrolling { get; set; }
    
    public override void Rise()
    {
        Scrolling = true;
        startPos = transform.position.x;
        length   = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public override void Tick()
    {
        if (!Scrolling)
        {
            return;
        }

        float dist = delta * parallaxEffect;

        transform.position += Vector3.left * dist;

        if (transform.position.x < startPos - length)
        {
            transform.position += Vector3.right * length;
        }
    }
}
