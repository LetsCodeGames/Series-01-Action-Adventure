using UnityEngine;
using System.Collections;

public class BatProximityTrigger : MonoBehaviour 
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
            m_Control.SetCharacterInRange( collider.gameObject );
        }
    }

    void OnTriggerExit2D( Collider2D collider )
    {
        if( collider.tag == "Player" )
        {
            m_Control.SetCharacterInRange( null );
        }
    }
}
