using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VolumeBox.Toolbox;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoCached
{
    private SpriteRenderer sr;
    
    public override void Rise()
    {
        sr = GetComponent<SpriteRenderer>();
        ResizeSpriteToScreen();
    }
    
    public void ResizeSpriteToScreen() 
    {
        if (sr == null) return;
     
        transform.localScale = Vector3.one;

        var sprite = sr.sprite;
        
        float width = sprite.bounds.size.x;
        float height = sprite.bounds.size.y;
     
        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector3 newSize = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);

        transform.localScale = newSize;
    }
}
