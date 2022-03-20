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

}
