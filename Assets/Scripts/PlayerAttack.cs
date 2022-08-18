using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public enum Gun_Style
    {
        Automatic,
        Manual
    }
    public Gun_Style gunStyle;
    public GunManager gun;
    public float time , oriTime;
    public bool enableTime;
    public Transform hLeft, hRight;
    public void manual()
    {
        gunStyle = Gun_Style.Manual;
    }
    public void Automatic()
    {
        gunStyle = Gun_Style.Automatic;
    }
    private void Start()
    {
        time = oriTime;
    }
    private void Update()
    {
        if (enableTime)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0)
        {
            GetComponent<Animator>().SetLayerWeight(1, 0);
        }
        else if (time > 0 && enableTime)
        {
            GetComponent<Animator>().SetLayerWeight(1, 1);
        }
        if (gun == null || !gun.gameObject.activeSelf)
        {
            gun = FindObjectOfType<GunManager>();
        }
        else
        {
            switch (gunStyle)
            {
                case Gun_Style.Automatic:
                    gun.Shooted();
                    GetComponent<Animator>().SetLayerWeight(1, 1);                 
                    break;
                case Gun_Style.Manual:                   

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        enableTime = true;                        
                        gun.Shoot();
                                         
                    }//
                    if (Input.GetKeyUp(KeyCode.Mouse0))
                    {
                        enableTime = false;
                        time = oriTime;
                        gun.Reload();
                        GetComponent<Animator>().SetLayerWeight(1, 0);
                    }
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                gun.Shooted();                              
                                break;
                            case TouchPhase.Ended:
                                gun.Reload();
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
           
        }
    }
}
