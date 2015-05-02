using UnityEngine;
using System.Collections;

public class EnemyCharacterCollision : MonoBehaviour 
{
    CharacterBatControl m_Control;

    void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
    }

    void OnTriggerEnter2D( Collider2D collider )
    {
        if( collider.tag == "Player" )
        {
            m_Control.OnHitCharacter( collider.gameObject );
        }
    }
}
