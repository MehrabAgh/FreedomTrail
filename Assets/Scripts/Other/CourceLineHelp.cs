using UnityEngine;
using UnityEngine.EventSystems;
public class CourceLineHelp : MonoBehaviour, IPointerClickHandler
{   
    public void OnPointerClick(PointerEventData eventData)=>gameObject.SetActive(false);    
}

