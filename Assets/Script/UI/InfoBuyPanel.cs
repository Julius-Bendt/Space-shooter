using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoBuyPanel : MonoBehaviour {

    public Slider firerate, sliderspeed;
    public TextMeshProUGUI name, price;

    private static int id;

    private float a_price;

    public void Open(string _name,float _price,float _firerate,float _speed,int _id)
    {
        firerate.value = _firerate;
        sliderspeed.value = _speed;
        name.text = _name;
        price.text = _price.ToString();
        a_price = _price;
        id = _id;

    }


    public void BuyShip()
    {

        if(GameController.money >= a_price)
        {
            GameController.money -= a_price;
            GameController.shipID = id;

            GameController gc = FindObjectOfType<GameController>();

            gc.ac.PlaySound(gc.purchase_comp);

            CanvasGroup cg = GetComponent<CanvasGroup>();

            cg.interactable = false;
            cg.blocksRaycasts = false;
            cg.alpha = 0;

        }
        


    }
	
}
