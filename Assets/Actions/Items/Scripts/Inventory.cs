using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct GiveItemData
    {
        public string itemID;
        public int count;
    }
    [SerializeField]
    private ItemDB itemDB;

    public TableItem itemCounts = new TableItem();
    
    void Start(){
        itemDB.getItemList().ForEach((item)=>{
            itemCounts.GetList().Add(new TableItemPair(item,0));
        });
    }

    public ItemBasis GetItem(string ID){
        return itemDB.getItemList().Find((item)=>(string.Compare(item.itemID,ID)==0));
    }
    public int GetItemCnt(string itemStr){
        return GetItemCnt(GetItem(itemStr));
    }
    
    public int GetItemCnt(ItemBasis item){
        return itemCounts.GetList().Find((pair)=>(pair.Key==item)).Value;
    }

    public void GiveItem(ItemBasis item, int cnt){
        TableItemPair itemPair = itemCounts.GetList().Find((pair)=>(pair.Key==item));
        itemPair.Value+=cnt;
        if(itemPair.Value>item.maxCountInventory){
            itemPair.Value=item.maxCountInventory;
        }
    }
    public void GiveItem(string itemID, int cnt){
        GiveItem(GetItem(itemID),cnt);
    }
    public void GiveItem(string itemJSON){
        GiveItemData data = JsonUtility.FromJson<GiveItemData>(itemJSON);
        GiveItem(data.itemID,data.count);
    }
}
