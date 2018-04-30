using UnityEngine;
using System.Collections;

[System.Serializable]
public struct DropItemProbability
{
    public ItemType DropItemType;
    public int Amount;

    [Range( 0f, 1f )]
    public float Probability;
}

public class DropItemOnDestroy : MonoBehaviour 
{
    public DropItemProbability[] Probabilities;
    public Transform DropPosition;

    void OnLootDrop()
    {
        for( int i = 0; i < Probabilities.Length; ++i )
        {
            float randomValue = Random.Range( 0f, 1f );

            if( randomValue > Probabilities[ i ].Probability )
            {
                continue;
            }

            DropItem( Probabilities[ i ].DropItemType );

            return;
        }
    }
    
    void DropItem( ItemType itemType )
    {
        ItemData data = Database.Item.FindItem( itemType );

        if( data == null || data.Prefab == null )
        {
            Debug.LogWarning( "Can't drop " + itemType + ". No data found" );
            return;
        }

        Transform dropPosition = DropPosition;

        if( dropPosition == null )
        {
            dropPosition = transform;
        }

        Instantiate( data.Prefab, dropPosition.position, Quaternion.identity );
    }
}
