using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed, rotationSpeed;

    Rigidbody2D rig;

    float thrust, rotationThrust;

    [Header("Fire mechanism")]
    public GameObject bullet;
    public Transform[] shootingpos;
    public float fireRate;
    private float timer;
    public float energyDrain = 0.02f, maxEnergy;


    public Slider energySlider;

    bool canGeneratePower = true;

    float e_timer;

    GameController gc;

    // Use this for initialization
    void Start ()
    {
        rig = GetComponent<Rigidbody2D>();
        energySlider.maxValue = maxEnergy;
        energySlider.value = maxEnergy;

        gc = FindObjectOfType<GameController>();
        
	}

    private void Update()
    {
        thrust = Input.GetAxis("Vertical");
        rotationThrust = Input.GetAxis("Horizontal");


        if(Input.GetButton("Fire") && Planet.health > 0)
        {
            if(timer >= fireRate && energySlider.value > energyDrain)
            {
                timer = 0;

                foreach (Transform t in shootingpos)
                {
                    Instantiate(bullet, t.position, t.rotation);
                }

                gc.ac.PlaySound(gc.laser,false,0.1f);

                energySlider.value -= energyDrain;

                canGeneratePower = false;
                
            }
        }


        if(!canGeneratePower)
        {
            e_timer += Time.deltaTime;

            if (e_timer >= 0.75f)
            {
                canGeneratePower = true;
                e_timer = 0;
            }
        }
        else
        {
            energySlider.value += energyDrain * Time.deltaTime * 4;
        }
       
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {

        if (Planet.health <= 0)
        {
            rig.velocity = Vector3.zero;
            return;
        }
           

        if (thrust != 0)
            rig.velocity = transform.up * thrust * speed;


        rig.rotation += -rotationThrust * rotationSpeed;

	}
}
