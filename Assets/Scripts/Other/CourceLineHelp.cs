using UnityEngine;
using UnityEngine.EventSystems;
public class CourceLineHelp : MonoBehaviour
{
    private void Update() { if (Input.GetKeyDown(KeyCode.Mouse0)) gameObject.SetActive(false); }
}

