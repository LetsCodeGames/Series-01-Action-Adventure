using UnityEngine;
using System.Collections;

public class CollectableItem : MonoBehaviour 
{
    public ItemType ItemType;
    public int Amount;

    void OnTriggerEnter2D( Collider2D collider )
    {
        CharacterInventoryModel inventoryModel = collider.GetComponent<CharacterInventoryModel>();

        if( inventoryModel != null )
        {
            inventoryModel.AddItem( ItemType, Amount );
            Destroy( gameObject );
        }
    }
}
