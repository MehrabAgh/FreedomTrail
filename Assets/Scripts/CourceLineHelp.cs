using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CourceLineHelp : MonoBehaviour, IPointerClickHandler
{
    public GameObject CourceLineY;
    public void OnPointerClick(PointerEventData eventData)
    {
        if(gameObject.name == "CourceLineX") {           
            CourceLineY.gameObject.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}

