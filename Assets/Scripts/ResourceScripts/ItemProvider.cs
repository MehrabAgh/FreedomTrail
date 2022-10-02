using Vino.Devs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemProvider : MonoBehaviour
{
    public bool _isBuy;
    private int indexing;
    public int ItemIndex;
    private Animator anim;
    private enum ModeState { GunMode, SkinMode }
    [SerializeField] private ModeState modeState;
    public enum StatusItem
    {
        Selected,
        NotSelect,
        Selection
    }
    public StatusItem status;
    [SerializeField] private int Price;

    private void CheckStatus()
    {
        switch (status)
        {
            case StatusItem.Selected:
                anim.SetBool("selected", true);
                break;
            case StatusItem.NotSelect:
                anim.SetBool("select", false);
                anim.SetBool("selected", false);
                break;
            case StatusItem.Selection:
                anim.SetBool("select", true);
                break;
            default:
                break;
        }
    }
    public void SelectItem()
    {
        ItemManager.instance.DisableOther();
        if (!_isBuy)
        {
            indexing++;
            if ((indexing % 2) == 1)
            {
                status = StatusItem.Selection;
                ItemManager.instance.price = Price;
                ItemManager.instance.btn = gameObject;
            }
            else status = StatusItem.NotSelect;
        }
        else
        {
            CheckMode();
            status = StatusItem.Selected;
            ItemManager.instance.price = 0;

        }
    }
    private void ChangeSkin()
    {
        var ind = GetComponent<IndexGunButton>().index;
        PlayerPrefs.SetInt("PlayerIndex", ind);
        if (ItemIndex >= 9)
        {
            GameManager.instance.CResource.DefaultCharacter.gameObject.SetActive(false);
            GameManager.instance.CResource.ProCharacter.gameObject.SetActive(true);
            GameManager.instance.Player = FindObjectOfType<MainPlayer>();
            GameManager.instance.Player.mymodel.sharedMesh = GameManager.instance.CResource.Characters[ind];
            PlayerPrefs.SetInt("Player", 1);
        }
        else
        {
            GameManager.instance.CResource.DefaultCharacter.gameObject.SetActive(true);
            GameManager.instance.CResource.ProCharacter.gameObject.SetActive(false);
            GameManager.instance.Player = FindObjectOfType<MainPlayer>();
            GameManager.instance.Player.mymodel.material = GameManager.instance.CResource.StandardMat[ind];
            PlayerPrefs.SetInt("Player", -1);
        }
        GameManager.instance.ChangePLayerForAll();
    }
    private void ChangeGun()
    {        
        GameManager.instance.Player.gun.CodeGun = GetComponent<IndexGunButton>().index;
        PlayerPrefs.SetInt("Gun", GameManager.instance.Player.gun.CodeGun);
        GameManager.instance.Player.gun.CreateGun();
        GameManager.instance.Player.gunRes = GameManager.instance.Player.gun.GetGunResponse();
    }
    private void CheckMode()
    {
        switch (modeState)
        {
            case ModeState.GunMode:
                ChangeGun();
                break;
            case ModeState.SkinMode:
                ChangeSkin();
                break;
            default:
                break;
        }
    }
    private void Awake()
    {
        if (ItemIndex == 0 || ItemIndex == 5)
        {
            PlayerPrefs.SetString("CheckBuy" + ItemIndex, "a");
            status = StatusItem.Selected;
        }
    }
    private void Start()
    {
        var str = PlayerPrefs.GetString("CheckBuy" + ItemIndex);
        if (str == "a")
        {
            transform.GetChild(1).gameObject.SetActive(false);
            _isBuy = true;
        }
        else _isBuy = false;
        GetComponent<Button>().onClick.AddListener(SelectItem);
       
        anim = GetComponent<Animator>();
        status = StatusItem.NotSelect;
    }
    private void Update() => CheckStatus();
}
