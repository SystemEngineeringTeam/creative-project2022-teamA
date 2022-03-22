
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


