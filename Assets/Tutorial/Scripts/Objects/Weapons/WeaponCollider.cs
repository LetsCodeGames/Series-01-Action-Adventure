using UnityEngine;
using System.Collections;

public class WeaponCollider : MonoBehaviour
{
    public ItemType Type;

    void OnTriggerEnter2D( Collider2D collider )
    {
        
        AttackableBase attackable = collider.gameObject.GetComponent<AttackableBase>();

        if( attackable != null )
        {
            attackable.OnHit( Type );
        }
    }
}
