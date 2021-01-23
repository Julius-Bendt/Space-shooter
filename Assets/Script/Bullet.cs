using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public string sender;
    public GameObject  explosion;
    public float speed;

    Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject,4);
    }

    // Update is called once per frame
    void Update ()
    {
        rig.velocity = transform.up * speed;
	}


    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Enemy")
        {

            FindObjectOfType<CameraScript>().Shake(0.2f);
            Destroy(other.gameObject);

            GameController gc = FindObjectOfType<GameController>();

            gc.ac.PlaySound(gc.explosion);
            Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
