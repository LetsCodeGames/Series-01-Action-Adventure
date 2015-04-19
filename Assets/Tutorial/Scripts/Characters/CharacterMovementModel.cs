using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class CharacterMovementModel : MonoBehaviour 
{
    public float Speed;
    public Transform WeaponParent;
    public GameObject SwordPrefab;

    private Vector3 m_MovementDirection;
    private Vector3 m_FacingDirection;

    private Rigidbody2D m_Body;

    private bool m_IsFrozen;
    private bool m_IsAttacking;

    private ItemType m_EquippedWeapon = ItemType.None;

    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        SetDirection( new Vector2( 0, -1 ) );
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void LateUpdate()
    {
        

    }

    void UpdateMovement()
    {
        if( m_IsFrozen == true || m_IsAttacking == true )
        {
            m_Body.velocity = Vector2.zero;
            return;
        }

        if( m_MovementDirection != Vector3.zero )
        {
            m_MovementDirection.Normalize();
        }

        m_Body.velocity = m_MovementDirection * Speed;
    }

    public void SetFrozen( bool isFrozen )
    {
        m_IsFrozen = isFrozen;
    }

    public void SetDirection( Vector2 direction )
    {
        if( m_IsFrozen == true || m_IsAttacking == true )
        {
            return;
        }

        m_MovementDirection = new Vector3( direction.x, direction.y, 0 );

        if( direction != Vector2.zero )
        {
            m_FacingDirection = m_MovementDirection;
        }
    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public Vector3 GetFacingDirection()
    {
        return m_FacingDirection;
    }

    public bool IsMoving()
    {
        if( m_IsFrozen == true )
        {
            return false;
        }

        return m_MovementDirection != Vector3.zero;
    }

    public void EquipWeapon( ItemType itemType )
    {
        if( itemType != ItemType.Sword )
        {
            return;
        }

        m_EquippedWeapon = itemType;

        GameObject newSwordObject = (GameObject)Instantiate( SwordPrefab );

        newSwordObject.transform.parent = WeaponParent;
        newSwordObject.transform.localPosition = Vector2.zero;
        newSwordObject.transform.localRotation = Quaternion.identity;
    }

    public bool CanAttack()
    {
        if( m_IsAttacking == true )
        {
            return false;
        }

        if( m_EquippedWeapon == ItemType.None )
        {
            return false;
        }

        return true;
    }

    public void DoAttack()
    {
        
    }

    public void OnAttackStarted()
    {
        m_IsAttacking = true;
    }

    public void OnAttackFinished()
    {
        m_IsAttacking = false;
    }
}
