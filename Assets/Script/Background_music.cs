using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_music : MonoBehaviour {

    private static Background_music _lock;

	// Use this for initialization
	void Start ()
    {
        if(_lock == null)
        {
            _lock = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }  
	}
}
