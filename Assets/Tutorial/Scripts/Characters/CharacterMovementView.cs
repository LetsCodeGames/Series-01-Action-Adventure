using UnityEngine;
using System.Collections;

public class CharacterMovementView : MonoBehaviour
{
    public Animator Animator;

    private CharacterMovementModel m_MovementModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();

        if( Animator == null )
        {
            Debug.LogError( "Character Animator is not setup!" );
            enabled = false;
        }
    }

    public void Update() 
    {
        UpdateDirection();   
    }

    void UpdateDirection()
    {
        Vector3 direction = m_MovementModel.GetDirection();

        if( direction != Vector3.zero )
        {
            Animator.SetFloat( "DirectionX", direction.x );
            Animator.SetFloat( "DirectionY", direction.y );
        }

        Animator.SetBool( "IsMoving", m_MovementModel.IsMoving() );
    }
}
