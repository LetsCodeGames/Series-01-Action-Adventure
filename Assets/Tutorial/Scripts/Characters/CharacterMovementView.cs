using UnityEngine;
using System.Collections;

public class CharacterMovementView : MonoBehaviour
{
    public Animator Animator;
    public Transform WeaponParent;

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

    void Start()
    {
        SetWeaponActive( false );
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

    public void DoAttack()
    {
        Animator.SetTrigger( "DoAttack" );
    }

    public void OnAttackStarted()
    {
        SetWeaponActive( true );
    }

    public void OnAttackFinished()
    {
        SetWeaponActive( false );
    }

    void SetWeaponActive( bool doActivate )
    {
        WeaponParent.gameObject.SetActive( doActivate );
        /*for( int i = 0; i < WeaponParent.childCount; ++i )
        {
            WeaponParent.GetChild( i ).gameObject.GetComponentInChildren<Renderer>().enabled = doActivate;
        }*/
    }
}
