using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName="アクションゲーム/アイテム系/武器")]
public class WeaponBasis : ItemBasis
{
    public enum WeaponAttr{
        Nomal,
        Fire,
        Thunder,
        Ice,
        Sacred,
        Dark,
    }
    public WeaponAttr attr = WeaponAttr.Nomal;
    public float attackPower=1;

}
