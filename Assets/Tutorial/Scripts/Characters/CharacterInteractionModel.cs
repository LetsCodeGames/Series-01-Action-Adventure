using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent( typeof( Character ) )]
public class CharacterInteractionModel : MonoBehaviour
{
    private Character m_Character;
    private Collider2D m_Collider;
    private CharacterMovementModel m_MovementModel;

    void Awake()
    {
        m_Character = GetComponent<Character>();
        m_Collider = GetComponent<Collider2D>();
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    void Update() 
    {
        
    }

    public void OnInteract()
    {
        InteractableBase usableInteractable = FindUsableInteractable();

        if( usableInteractable == null )
        {
            return;
        }

        usableInteractable.OnInteract( m_Character );
    }

    InteractableBase FindUsableInteractable()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        Collider2D[] closeColliders = Physics2D.OverlapAreaAll( 
            (Vector2)transform.position + boxCollider.offset + boxCollider.size * 0.6f,
            (Vector2)transform.position + boxCollider.offset - boxCollider.size * 0.6f );

        InteractableBase closestInteractable = null;
        float angleToClosestInteractble = Mathf.Infinity;

        for( int i = 0; i < closeColliders.Length; ++i )
        {
            InteractableBase colliderInteractable = closeColliders[ i ].GetComponent<InteractableBase>();

            if( colliderInteractable == null )
            {
                continue;
            }

            Vector3 directionToInteractble = closeColliders[ i ].transform.position - transform.position;

            float angleToInteractable = Vector3.Angle( m_MovementModel.GetFacingDirection(), directionToInteractble );

            if( angleToInteractable < 40 )
            {
                if( angleToInteractable < angleToClosestInteractble )
                {
                    closestInteractable = colliderInteractable;
                    angleToClosestInteractble = angleToInteractable;
                }
            }
        }

        return closestInteractable;
    }
}
