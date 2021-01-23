using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    private Transform target;
    private Rigidbody2D rig;

    public float speed;

    GameController gc;

	// Use this for initialization
	void Start ()
    {
        rig = GetComponent<Rigidbody2D>();

        gc = FindObjectOfType<GameController>();

        if (Random.Range(0,100) >= 75)
            target = GameObject.FindGameObjectWithTag("Player").transform;	
        else
            target = GameObject.FindGameObjectWithTag("Planet").transform;


        
            gc.enemiesAlive++;
    }
	
	// Update is called once per frame
	void Update () {

        LookAt2D();
        rig.velocity = transform.up * speed;


        if (Planet.health <= 0)
            Destroy(gameObject);

        if (GameController.LoadedScene != "game")
            Destroy(gameObject);

	}

    public void LookAt2D()
    {

        Vector3 s = target.position - transform.position;
        s.Normalize();

        transform.up = s;
    }

    private void OnDestroy()
    {
        gc.enemiesAlive--;
    }


}
