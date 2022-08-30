using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    //public PlayerWeapon weapon;
    public int Coin;
    private int indexGun;
    public static bool[] gunUnlock;
    private GameObject select;
    private ItemDetect[] All;
    private void Start()
    {
        Coin = PlayerPrefs.GetInt("CoinStart");
        //weapon = FindObjectOfType<PlayerWeapon>();
       // GunLoaded();
    }
    private void Update()
    {
        //if(weapon == null)
        //{
        //    weapon = FindObjectOfType<PlayerWeapon>();
        //    //GunLoaded();
        //}
    }
    public void Buy(int price)
    {      
        if (Coin >= price)
        {
            var x = EventSystem.current.currentSelectedGameObject;
            PlayerPrefs.SetString(x.name,"Unlock");
            Coin -= price;
            PlayerPrefs.SetInt("CoinStart", Coin);
        }        
    }
    public void GunSelected()
    {
        select = EventSystem.current.currentSelectedGameObject;
        All = FindObjectsOfType<ItemDetect>();
        foreach (var item in All)
        {
            item.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        select.GetComponentInParent<ItemDetect>().GetComponent<Image>().color = Color.cyan;
    }
    #region Guns

    //public void SMG_Gun()
    //{
    //    indexGun = 3;
    //    weapon.changeGun(indexGun);
    //    GunSelected();
    //    PlayerPrefs.SetString("Gun", "SMG");
    //}
    #endregion
    //private void GunLoaded()
    //{
    //    var gunName = PlayerPrefs.GetString("Gun");
    //    switch (gunName)
    //    {
    //        case "MG":
    //            weapon.changeGun(2);
    //            break;
    //        case "SMG":
    //            weapon.changeGun(3);
    //            break;
    //        case "ShutGun":
    //            weapon.changeGun(1);
    //            break;
    //        case "Rifle":
    //            weapon.changeGun(0);
    //            break;
    //        default:
    //            break;
    //    }
    //}
}
