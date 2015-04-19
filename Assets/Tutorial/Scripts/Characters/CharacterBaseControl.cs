using UnityEngine;
using System.Collections;

public class CharacterBaseControl : MonoBehaviour
{
    private CharacterMovementModel m_MovementModel;
    private CharacterInteractionModel m_InteractionModel;
    private CharacterMovementView m_MovementView;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_MovementView = GetComponent<CharacterMovementView>();
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

    protected void OnAttackPressed()
    {
        if( m_MovementModel == null )
        {
            return;
        }

        if( m_MovementModel.CanAttack() == false )
        {
            return;
        }

        m_MovementModel.DoAttack();
        m_MovementView.DoAttack();
    }
}
