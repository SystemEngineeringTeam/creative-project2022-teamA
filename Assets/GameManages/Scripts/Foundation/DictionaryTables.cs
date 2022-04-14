using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

#region DictionaryItem
    [System.Serializable]
    public class TableItem: Serialize.TableBase<ItemBasis, int,TableItemPair>
    {
        
    }

    [System.Serializable]
    public class TableItemPair : Serialize.KeyAndValue<ItemBasis, int>{

        public TableItemPair (ItemBasis key, int value) : base (key, value) {

        }
    }
#endregion

#region DictionaryFloat
    [System.Serializable]
    public class TableFloat: Serialize.TableBase<string, float,TableFloatPair>
    {
        
    }

    [System.Serializable]
    public class TableFloatPair : Serialize.KeyAndValue<string, float>{

        public TableFloatPair (string key, float value) : base (key, value) {

        }
    }
#endregion

#region DictionaryEvent
    [System.Serializable]
    public class TableEvent: Serialize.TableBase<string, UnityEvent,TableEventPair>
    {
        
    }

    [System.Serializable]
    public class TableEventPair : Serialize.KeyAndValue<string, UnityEvent>{

        public TableEventPair (string key, UnityEvent value) : base (key, value) {

        }
    }
#endregion

#region DictionaryPoint
    [System.Serializable]
    public class TablePoint: Serialize.TableBase<string, Vector3,TablePointPair>
    {
        
    }

    [System.Serializable]
    public class TablePointPair : Serialize.KeyAndValue<string, Vector3>{

        public TablePointPair (string key, Vector3 value) : base (key, value) {

        }
    }
#endregion


