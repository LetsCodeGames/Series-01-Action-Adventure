using UnityEngine;
using System.Collections;

public class AttackableBush : AttackableBase
{
    public GameObject DestroyedPrefab;
    public GameObject DestroyEffect;

    public void CreateDestroyedPrefab()
    {
        GameObject newDestroyedBushObject = (GameObject)Instantiate( DestroyedPrefab, transform.position, transform.rotation );
        newDestroyedBushObject.transform.parent = transform.parent;
    }

    public void DestroyBush()
    {
        Destroy( gameObject );

        if( DestroyEffect != null )
        {
            GameObject destroyEffect = (GameObject)Instantiate( DestroyEffect );
            destroyEffect.transform.position = transform.position;
        }
    }

    public void DropLoot()
    {
        BroadcastMessage( "OnLootDrop", SendMessageOptions.DontRequireReceiver );
    }

    public override void OnHit( Collider2D hitCollider, ItemType item )
    {
        DestroyBush();
        CreateDestroyedPrefab();
        DropLoot();
    }

    void OnPickupObject( Character character )
    {
        CreateDestroyedPrefab();
        DropLoot();
    }

    void OnObjectThrown()
    {
        DestroyBush();
    }
}
