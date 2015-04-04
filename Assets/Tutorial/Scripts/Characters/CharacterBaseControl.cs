using UnityEngine;
using System.Collections;

public class CharacterBaseControl : MonoBehaviour
{
    private CharacterMovementModel m_MovementModel;
    private CharacterInteractionModel m_InteractionModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
    }

    protected void SetDirection( Vector2 direction )
    {
        if( m_MovementModel == null )
        {
            return;
        }

        m_MovementModel.SetDirection( direction );
    }

    protected void OnActionPressed()
    {
        if( m_InteractionModel == null )
        {
            return;
        }

        m_InteractionModel.OnInteract();
    }
}
