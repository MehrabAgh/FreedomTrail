using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Vino.Devs;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private List<ItemProvider> Items { get; set; }
    public int price;
    public GameObject btn , PanelError;
    [HideInInspector]public GiftItems giftItems;
    public static ItemManager instance;
    private void Awake() { instance = this; Saver(); giftItems = FindObjectOfType<GiftItems>(); }
    private void Update() => Saver();
    public void Saver() => Items = FindObjectsOfType<ItemProvider>().ToList();
    public void DisableOther()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            Items[i].status = ItemProvider.StatusItem.NotSelect;
        }
    }
    public void BuyItem()
    {
        if (ScoreManager.instance.storage._coin >= price)
        {
            ScoreManager.instance.storage._coin -= price;
            btn.transform.GetChild(1).gameObject.SetActive(false);
            btn.GetComponent<ItemProvider>()._isBuy = true;
            PlayerPrefs.SetString("CheckBuy" + btn.GetComponent<ItemProvider>().ItemIndex, "a");
        }
        else
        {
            var p = btn.transform.parent.parent.parent;
            PanelError = p.GetChild(p.childCount - 1).gameObject;
            PanelError.SetActive(true);
        }
    }
    public void OpenGift()
    {
        if (ScoreManager.instance.storage._key >= price)
        {
            ScoreManager.instance.storage._key -= price;
            UIManager.instance.GiftMenu.gameObject.SetActive(true);
            UIManager.instance.GiftMenu.transform.GetChild(0).
                GetComponent<Animator>().SetBool("OpenGift", true);            
            UIManager.instance.GiftMenu.transform.GetChild(1).GetComponent<Image>
                ().sprite = giftItems.RandomSelectItem();
            UIManager.instance.GiftMenu.transform.GetChild(1)
                .GetComponent<RectTransform>().sizeDelta = giftItems.GetScale();
        }
        else
        {
            var p = btn.transform.parent.parent.parent;
            PanelError = p.GetChild(p.childCount - 1).gameObject;
            PanelError.SetActive(true);
        }
    }
}