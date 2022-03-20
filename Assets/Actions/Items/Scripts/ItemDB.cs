using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDB", menuName="アクションゲーム/アイテム系/データベース")]

public class ItemDB : ScriptableObject
{
    [SerializeField]
    private List<ItemBasis> itemList = new List<ItemBasis>();
    public List<ItemBasis> getItemList(){
        return itemList;
    }
}