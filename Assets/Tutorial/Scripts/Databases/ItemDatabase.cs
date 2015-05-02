using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : ScriptableObject 
{
    public List<ItemData> Data;

    public ItemData FindItem( ItemType itemType )
    {
        for( int i = 0; i < Data.Count; ++i )
        {
            if( Data[ i ].Type == itemType )
            {
                return Data[ i ];
            }
        }

        return null;
    }
}

[System.Serializable]
public class ItemData
{
    public enum PickupAnimation
    {
        None,
        OneHanded,
        TwoHanded,
    }

    public enum EquipPosition
    {
        NotEquipable,
        SwordHand,
        ShieldHand,
    }

    public ItemType Type;
    public GameObject Prefab;
    public EquipPosition IsEquipable;
    public PickupAnimation Animation;
}