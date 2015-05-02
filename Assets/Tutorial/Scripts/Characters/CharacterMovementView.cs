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
        UpdatePickingUpAnimation();
        UpdateHit();
    }

    void UpdatePickingUpAnimation()
    {
        bool isPickingUpOneHanded = false;
        bool isPickingUpTwoHanded = false;

        if( m_MovementModel.IsFrozen() == true )
        {
            ItemType pickupItem = m_MovementModel.GetItemThatIsBeingPickedUp();
            
            if( pickupItem != ItemType.None )
            {
                ItemData itemData = Database.Item.FindItem( pickupItem );

                switch( itemData.Animation )
                {
                case ItemData.PickupAnimation.OneHanded:
                    isPickingUpOneHanded = true;
                    break;
                case ItemData.PickupAnimation.TwoHanded:
                    isPickingUpTwoHanded = true;
                    break;
                }
            }
        }

        Animator.SetBool( "IsPickingUpOneHanded", isPickingUpOneHanded );
        Animator.SetBool( "IsPickingUpTwoHanded", isPickingUpTwoHanded );
    }

    void UpdateHit()
    {
        Animator.SetBool( "IsHit", m_MovementModel.IsBeingPushed() );
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
        
    }

    public void OnAttackFinished()
    {
        
    }

    public void ShowWeapon()
    {
        SetWeaponActive( true );
    }

    public void HideWeapon()
    {
        SetWeaponActive( false );
    }

    public void SetSortingOrderOfWeapon( int sortingOrder )
    {
        if( WeaponParent == null )
        {
            return;
        }

        SpriteRenderer[] spriteRenderers = WeaponParent.GetComponentsInChildren<SpriteRenderer>();

        foreach( SpriteRenderer spriteRenderer in spriteRenderers )
        {
            spriteRenderer.sortingOrder = sortingOrder;
        }
    }

    void SetWeaponActive( bool doActivate )
    {
        if( WeaponParent == null )
        {
            return;
        }

        WeaponParent.gameObject.SetActive( doActivate );
        /*for( int i = 0; i < WeaponParent.childCount; ++i )
        {
            WeaponParent.GetChild( i ).gameObject.GetComponentInChildren<Renderer>().enabled = doActivate;
        }*/
    }
}
