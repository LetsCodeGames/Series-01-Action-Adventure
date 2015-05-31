using UnityEngine;
using System.Collections;
using System.Runtime.Remoting.Messaging;

public class CharacterMovementModel : MonoBehaviour 
{
    public float Speed;
    public Transform WeaponParent;
    public Transform ShieldParent;
    public Transform PickupItemParent;

    private Vector3 m_MovementDirection;
    private Vector3 m_FacingDirection;

    private Rigidbody2D m_Body;

    private bool m_IsFrozen;
    private bool m_IsDirectionFrozen;
    private bool m_IsAttacking;
    private ItemType m_PickingUpObject = ItemType.None;

    private ItemType m_EquippedWeapon = ItemType.None;
    private ItemType m_EquippedShield = ItemType.None;

    private GameObject m_PickupItem;

    private Vector2 m_PushDirection;
    private float m_PushTime;

    private int m_LastSetDirectionFrameCount;

    private float m_LastFreezeTime;

    private bool m_IsAbleToAttack = true;

    Vector2 m_ReceivedDirection;

    private bool m_IsOverrideSpeed;
    private float m_OverrideSpeed;

    void Awake()
    {
        m_Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdatePushTime();
        UpdateDirection();
        ResetReceivedDirection();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void LateUpdate()
    {
        

    }

    void ResetReceivedDirection()
    {
        m_ReceivedDirection = Vector2.zero;
    }

    void UpdateDirection()
    {
        if( m_IsFrozen == true )
        {
            if( m_ReceivedDirection != Vector2.zero &&
                GetItemThatIsBeingPickedUp() != ItemType.None &&
                GetTimeSinceFrozen() > 0.5f )
            {
                m_PickingUpObject = ItemType.None;
                SetFrozen( false, false, true );
                Destroy( m_PickupItem );
            }
        }

        if( m_IsDirectionFrozen == true && m_ReceivedDirection != Vector2.zero )
        {
            return;
        }

        if( m_IsAttacking == true )
        {
            return;
        }

        if( IsBeingPushed() == true )
        {
            m_MovementDirection = m_PushDirection;
            return;
        }

        if( Time.frameCount == m_LastSetDirectionFrameCount )
        {
            return;
        }

        m_MovementDirection = new Vector3( m_ReceivedDirection.x, m_ReceivedDirection.y, 0 );

        if( m_ReceivedDirection != Vector2.zero )
        {
            Vector3 facingDirection = m_MovementDirection;

            if( facingDirection.x != 0 && facingDirection.y != 0 )
            {
                if( facingDirection.x == m_FacingDirection.x )
                {
                    facingDirection.y = 0;
                }
                else if( facingDirection.y == m_FacingDirection.y )
                {
                    facingDirection.x = 0;
                }
                else
                {
                    facingDirection.x = 0;
                }
            }

            m_FacingDirection = facingDirection;
            m_LastSetDirectionFrameCount = Time.frameCount;
        }
    }

    void UpdatePushTime()
    {
        m_PushTime = Mathf.MoveTowards( m_PushTime, 0f, Time.deltaTime );
    }

    void UpdateMovement()
    {
        if( m_IsFrozen == true || m_IsAttacking == true )
        {
            m_Body.velocity = Vector2.zero;
            return;
        }

        if( GameCamera.Instance != null && GameCamera.Instance.IsSwitchingScene() == true )
        {
            m_Body.velocity = Vector2.zero;
            return;
        }

        if( m_MovementDirection != Vector3.zero )
        {
            m_MovementDirection.Normalize();
        }

        if( IsBeingPushed() == true )
        {
            m_Body.velocity = m_PushDirection;
        }
        else
        {
            float speed = Speed;

            if( m_IsOverrideSpeed == true )
            {
                speed = m_OverrideSpeed;
            }

            m_Body.velocity = m_MovementDirection * speed;
        }
    }

    public void SetOverrideSpeedEnabled( bool enabled, float speed = 0f )
    {
        m_IsOverrideSpeed = enabled;
        m_OverrideSpeed = speed;
    }

    public bool IsBeingPushed()
    {
        return m_PushTime > 0;
    }

    public bool IsFrozen()
    {
        return m_IsFrozen;
    }

    float GetTimeSinceFrozen()
    {
        if (IsFrozen () == false) {
            return 0f;
        }

        return Time.realtimeSinceStartup - m_LastFreezeTime;
    }

    public void SetFrozen( bool isFrozen, bool isDirectionFrozen, bool affectGameTime )
    {
        m_IsFrozen = isFrozen;
        m_IsDirectionFrozen = isDirectionFrozen;

        if( affectGameTime == true )
        {
            if( isFrozen == true )
            {
                m_LastFreezeTime = Time.realtimeSinceStartup;
                StartCoroutine( FreezeTimeRoutine() );
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator FreezeTimeRoutine()
    {
        yield return null;

        Time.timeScale = 0;
    }

    public void SetDirection( Vector2 direction )
    {
        if( direction == Vector2.zero )
        {
            return;
        }

        m_ReceivedDirection = direction;
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
        EquipItem( itemType, ItemData.EquipPosition.SwordHand, WeaponParent, ref m_EquippedWeapon );
    }

    public void EquipShield( ItemType itemType )
    {
        EquipItem( itemType, ItemData.EquipPosition.ShieldHand, ShieldParent, ref m_EquippedShield );
    }

    void EquipItem( ItemType itemType, ItemData.EquipPosition equipPosition, 
                    Transform itemParent, ref ItemType equippedItemSlot )
    {
        if( itemParent == null )
        {
            return;
        }

        ItemData itemData = Database.Item.FindItem( itemType );

        if( itemData == null )
        {
            return;
        }

        if( itemData.IsEquipable != equipPosition )
        {
            return;
        }

        equippedItemSlot = itemType;

        GameObject newItemObject = (GameObject)Instantiate( itemData.Prefab );

        newItemObject.transform.parent = itemParent;
        newItemObject.transform.localPosition = Vector2.zero;
        newItemObject.transform.localRotation = Quaternion.identity;
    }

    public void ShowItemPickup( ItemType itemType )
    {
        if( PickupItemParent == null )
        {
            return;
        }

        ItemData itemData = Database.Item.FindItem( itemType );

        if( itemData == null )
        {
            return;
        }

        SetDirection( new Vector2( 0, -1 ) );
        SetFrozen( true, true, true );

        m_PickingUpObject = itemType;

        m_PickupItem = (GameObject)Instantiate( itemData.Prefab );

        m_PickupItem.transform.parent = PickupItemParent;
        m_PickupItem.transform.localPosition = Vector2.zero;
        m_PickupItem.transform.localRotation = Quaternion.identity;

        MonoBehaviour[] components = m_PickupItem.GetComponentsInChildren<MonoBehaviour>();

        foreach( MonoBehaviour component in components )
        {
            component.enabled = false;
        }
    }

    public void PushCharacter( Vector2 pushDirection, float time )
    {
        if( m_IsAttacking == true )
        {
            GetComponentInChildren<CharacterAnimationListener>().OnAttackFinished();
        }

        m_PushDirection = pushDirection;
        m_PushTime = time;
    }

    public ItemType GetItemThatIsBeingPickedUp()
    {
        return m_PickingUpObject;
    }

    public ItemType GetEquippedShield()
    {
        return m_EquippedShield;
    }

    public ItemType GetEquippedWeapon()
    {
        return m_EquippedWeapon;
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

        if( IsBeingPushed() == true )
        {
            return false;
        }

        if( m_IsAbleToAttack == false )
        {
            return false;
        }

        return true;
    }

    public void SetIsAbleToAttack( bool isAbleToAttack )
    {
        m_IsAbleToAttack = isAbleToAttack;
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
