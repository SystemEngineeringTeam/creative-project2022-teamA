using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName="アクションゲーム/アイテム系/アイテム")]
public class ItemBasis: ScriptableObject{
    
    public string itemName;
    public string itemID;
    public string information;
    public int maxCountInventory=999;
    public int maxCountInBox=999;
    public bool Stackable=true;

    public Sprite icon;


}