using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public static int health;

    public GameObject explosion;

    public float rotationsPerMinute = 10.0f;

    GameObject alarm;
    GameController gc;

    private void Start()
    {

         gc = FindObjectOfType<GameController>();

        health = gc.maxPlanetHealth;
        gc.uic.UpdatePlanetHealthbar();
        
       
    }

    private void Update()
    {
        transform.Rotate(0, 0, 6.0f * rotationsPerMinute * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Enemy")
        {
            Destroy(o.gameObject);
            health -= 10;
            Instantiate(explosion, transform.position, Quaternion.identity);

            FindObjectOfType<CameraScript>().Shake(0.35f);
            gc.uic.UpdatePlanetHealthbar();

            if(health <= (gc.maxPlanetHealth*0.2f) && alarm == null)
            {
                alarm = gc.ac.PlaySound(gc.alarm,true,0.2f);
            }

            if (health == 0)
            {
               gc.uic.gameoverTab.SetActive(true);

                alarm.GetComponent<AudioSource>().Stop();

                for (int i = 0; i < 5; i++)
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                }

                
            }

        }
    }


}
