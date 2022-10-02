using System.Collections.Generic;
using UnityEngine;
using Vino.Devs;
public class GiftItems : MonoBehaviour
{
    [SerializeField] private List<ItemResourceGift> ItemGifts;
    private int index { get; set; }
    public Sprite RandomSelectItem()
    {
        index = Random.Range(0, ItemGifts.Count);        
        return ItemGifts[index].IMG;
    }
    public Vector2 GetScale()
    {
        if(ItemGifts[index].Thetype == ItemResourceGift.TYPE.Coin)
            return new Vector2(1024f, 360);
        else
            return new Vector2(596, 754);
    }
    public void OnRecived() => ItemGifts[index].UpdateMode();
}
[System.Serializable]
public struct ItemResourceGift
{
    public Sprite IMG;
    public enum TYPE { Coin , Character}
    public TYPE Thetype;
    public int ValueCoin;
    public ItemProvider ValueCharacter;    
    public void UpdateMode()
    {
        switch (Thetype)
        {
            case TYPE.Coin:
                ScoreManager.instance.Coin += ValueCoin;              
                break;
            case TYPE.Character:
                ValueCharacter._isBuy = true;
                PlayerPrefs.SetString("CheckBuy" + ValueCharacter.ItemIndex , "a");               
                break;
            default:
                break;
        }
    }
}