using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeyConfigExample", menuName="アクションゲーム/データ/キーコンフィグ")]
public class KeyConfig : ScriptableObject
{
    public enum MouseKey{
        LeftClick,
        RightClick,
        MiddleClick,
    }
    [System.Serializable]
    public struct Key
    {
        public List<KeyCode> keys;
        public List<MouseKey> clicks;
        public Key(KeyCode k){
            keys=new List<KeyCode>(new KeyCode[]{k});
            clicks=new List<MouseKey>();
        }
        public Key(MouseKey c){
            keys=new List<KeyCode>();
            clicks=new List<MouseKey>(new MouseKey[]{c});
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
            if(result){
                return result;
            }
            foreach (var click in clicks)
            {
                if(Input.GetMouseButtonDown((int)click)){
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
            if(result){
                return result;
            }
            foreach (var click in clicks)
            {
                if(Input.GetMouseButtonUp((int)click)){
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
            if(result){
                return result;
            }
            foreach (var click in clicks)
            {
                if(Input.GetMouseButton((int)click)){
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
            if(!result){
                return result;
            }
            foreach (var click in clicks)
            {
                if(!Input.GetMouseButton((int)click)){
                    result=false;
                    break;
                }
            }
            return result;
        }
        public bool AllDown(){
            bool result=false;
            if(All()){
                result=Down();
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
    public Key action = new Key(KeyCode.S);
    public Key attack = new Key(MouseKey.LeftClick);
    public Key lookAt = new Key(MouseKey.MiddleClick);
}
