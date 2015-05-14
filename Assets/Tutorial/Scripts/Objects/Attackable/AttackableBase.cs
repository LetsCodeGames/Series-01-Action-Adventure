using UnityEngine;
using System.Collections;

public class AttackableBase : MonoBehaviour 
{
    public virtual void OnHit( Collider2D hitCollider, ItemType item )
    {
        Debug.LogWarning( "No OnHit Event setup for " + gameObject.name, gameObject );
    }
}
