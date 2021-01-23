using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {


    public Vector2 size;

	// Use this for initialization
	void Start ()
    {
        Background b = GetComponent<Background>();


        Texture2D t = b.GenerateNebula((int)size.x, (int)size.y);

        Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero);

        GetComponent<SpriteRenderer>().sprite = s;




    }
}
