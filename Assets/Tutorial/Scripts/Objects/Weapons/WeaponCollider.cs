using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Collider2D ) )]
public class WeaponCollider : MonoBehaviour
{
    public ItemType Type;

    Collider2D m_Collider;

    void Awake()
    {
        m_Collider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D( Collider2D collider )
    {
        AttackableBase attackable = collider.gameObject.GetComponent<AttackableBase>();

        if( attackable != null )
        {
            attackable.OnHit( m_Collider, Type );
        }
    }
}
