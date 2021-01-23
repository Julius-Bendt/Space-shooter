using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    public void CloseCanvasGroup(CanvasGroup cg)
    {
        cg.interactable = false;
        cg.blocksRaycasts = false;
        cg.alpha = 0;
    }

    public void OpenCanvasGroup(CanvasGroup cg)
    {
        cg.interactable = true;
        cg.blocksRaycasts = true;
        cg.alpha = 1;
    }

}
