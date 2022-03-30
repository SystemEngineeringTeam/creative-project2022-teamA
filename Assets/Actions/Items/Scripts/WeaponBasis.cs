using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName="アクションゲーム/アイテム系/武器")]
public class WeaponBasis : ItemBasis
{
    public GameDefines.attackAttribute attr = GameDefines.attackAttribute.Nomal;
    public float attackPower=1;

}
