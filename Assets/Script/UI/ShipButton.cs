using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShipButton : MonoBehaviour
{
    public Image thumbnail;
    public TextMeshProUGUI title, price;
    public int id;

    private string a_title;
    private float _cost;

    private InfoBuyPanel ibp;

    public void Init(Sprite picture, string _title, float cost)
    {
        thumbnail.sprite = picture;
        title.text = a_title = _title;
        price.text = cost.ToString();

        _cost = cost;
    }

    public void buy()
    {

        GameObject g = GameObject.Find("info(buy)");

        FindObjectOfType<ShopController>().OpenCanvasGroup(g.GetComponent<CanvasGroup>());

        ibp = g.GetComponent<InfoBuyPanel>();

        ibp.Open(a_title, _cost, 0.5f, 0.5f,id);
    }

}
