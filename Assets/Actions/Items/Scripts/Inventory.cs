using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField]
    private ItemDB itemDB;

    public TableItem itemCounts = new TableItem();
    
    void Start(){
        itemDB.getItemList().ForEach((item)=>{
            itemCounts.GetList().Add(new TableItemPair(item,0));
        });
        Debug.Log(GetItem("hogeWeapon"));
    }

    public ItemBasis GetItem(string ID){
        return itemDB.getItemList().Find((item)=>(item.itemID==ID));
    }

    public int GiveItem(ItemBasis item, int cnt){
        TableItemPair itemPair = itemCounts.GetList().Find((pair)=>(pair.Key==item));
        int result=0;
        itemPair.Value+=cnt;
        if(itemPair.Value>item.maxCountInventory){
            result=itemPair.Value-item.maxCountInventory;
            itemPair.Value=item.maxCountInventory;
        }
        return result;
    }

}
