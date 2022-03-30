using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyConfigExample", menuName="アクションゲーム/データ/キーコンフィグ")]
public class KeyConfig : ScriptableObject
{
    [System.Serializable]
    public struct Key
    {
        public List<KeyCode> keys;
        public Key(KeyCode k){
            keys=new List<KeyCode>(new KeyCode[]{k});
        }
        public bool Down(){
            bool result=false;
            foreach (var key in keys)
            {
                if(Input.GetKeyDown(key)){
                    result=true;
                    break;
                }
            }
            return result;
        }
        public bool Up(){
            bool result=false;
            foreach (var key in keys)
            {
                if(Input.GetKeyUp(key)){
                    result=true;
                    break;
                }
            }
            return result;
        }
        public bool Stay(){
            bool result=false;
            foreach (var key in keys)
            {
                if(Input.GetKey(key)){
                    result=true;
                    break;
                }
            }
            return result;
        }
        public bool All(){
            bool result=true;
            foreach (var key in keys)
            {
                if(!Input.GetKey(key)){
                    result=false;
                    break;
                }
            }
            return result;
        }
        public bool AllDown(){
            bool result=false;
            if(All()){
                foreach (var key in keys)
                {
                    if(Input.GetKeyDown(key)){
                        result=true;
                        break;
                    }
                }
            }
            return result;
        }
    }

    public Key up = new Key(KeyCode.W);
    public Key down = new Key(KeyCode.S);
    public Key left = new Key(KeyCode.A);
    public Key right = new Key(KeyCode.D);
    public Key jump = new Key(KeyCode.Space);
    public Key dash = new Key(KeyCode.LeftShift);
}
