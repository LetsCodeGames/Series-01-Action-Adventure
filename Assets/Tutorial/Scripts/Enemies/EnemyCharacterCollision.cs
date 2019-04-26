using UnityEngine;
using System.Collections;

public class EnemyCharacterCollision : MonoBehaviour 
{
    CharacterBatControl m_Control;
    Character character;

    void Awake()
    {
        m_Control = GetComponentInParent<CharacterBatControl>();
        character = GetComponentInParent<Character>();
    }

    void OnTriggerEnter2D( Collider2D collider )
    {
        if( collider.CompareTag( "Player" ) )
        {
            m_Control.OnHitCharacter( collider.gameObject.GetComponent<Character>() );
        }
    }
}
