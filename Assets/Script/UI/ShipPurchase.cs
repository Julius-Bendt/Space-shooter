using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipPurchase : MonoBehaviour {

 

    public Transform contentviewer;

    public GameObject prefab;

    // Use this for initialization
    void Start()
    {

        int id = 0;

        GameController gc = FindObjectOfType<GameController>();

        foreach (GameController.ShipList sl in gc.shipList)
        {
            GameObject g  = Instantiate(prefab, contentviewer);

            ShipButton sb = g.GetComponent<ShipButton>();

            sb.Init(sl.picture, sl.title, sl.cost);
            sb.id = id;
            id++;
        }
    }

    // Update is called once per frame
    void Update() {

    }

}

