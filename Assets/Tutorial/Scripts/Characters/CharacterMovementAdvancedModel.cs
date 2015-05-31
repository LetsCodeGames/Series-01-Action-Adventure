using UnityEngine;
using System.Collections;

public class CharacterMovementAdvancedModel : MonoBehaviour 
{
    public float PushingSpeed;

    CharacterMovementModel m_MovementModel;
    CharacterInteractionModel m_InteractionModel;

    Vector3 m_LastPosition;
    float m_LastPositionTime;
    float m_MovementStartTime;
    bool m_WasMoving;
    Pushable m_ClosestPushable;
    Transform m_ClosestPushableParent;
    Collider2D m_Collider;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
        m_InteractionModel = GetComponent<CharacterInteractionModel>();
        m_Collider = GetComponent<Collider2D>();
    }

    void Update()
    {
        UpdatePushing();
        UpdateWasMoving();
        UpdatePushableObjects();
    }

    void UpdateWasMoving()
    {
        if( m_WasMoving == false && m_MovementModel.IsMoving() == true )
        {
            m_MovementStartTime = Time.realtimeSinceStartup;
        }

        m_WasMoving = m_MovementModel.IsMoving();
    }

    void UpdatePushableObjects()
    {
        if( m_ClosestPushable != null )
        {
            if( m_MovementModel.IsMoving() == false )
            {
                StopPushingClosestPushable();
            }

            return;
        }

        if( IsPushing() == false )
        {
            return;
        }

        m_ClosestPushable = FindClosestPushable();

        if( m_ClosestPushable == null )
        {
            return;
        }

        StartPushingClosestPushable();
    }

    void StopPushingClosestPushable()
    {
        Collider2D closestCollider = m_ClosestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision( m_Collider, closestCollider, false );

        m_ClosestPushable.transform.parent = m_ClosestPushableParent;
        m_ClosestPushable = null;

        m_MovementModel.SetFrozen( false, false, false );
        m_MovementModel.SetOverrideSpeedEnabled( false );
    }

    void StartPushingClosestPushable()
    {
        m_ClosestPushableParent = m_ClosestPushable.transform.parent;
        m_ClosestPushable.transform.parent = transform;

        Collider2D closestPushableCollider = m_ClosestPushable.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision( m_Collider, closestPushableCollider );

        m_MovementModel.SetFrozen( false, true, false );
        m_MovementModel.SetOverrideSpeedEnabled( true, PushingSpeed );
    }

    Pushable FindClosestPushable()
    {
        Collider2D[] closeColliders = m_InteractionModel.GetCloseColliders();

        Pushable closestPushable = null;
        float angleToClosestPushable = Mathf.Infinity;

        for( int i = 0; i < closeColliders.Length; ++i )
        {
            Pushable colliderPushable = closeColliders[ i ].GetComponent<Pushable>();

            if( colliderPushable == null )
            {
                continue;
            }

            Vector3 directionToPushable = closeColliders[ i ].transform.position - transform.position;

            float angleToPushable = Vector3.Angle( m_MovementModel.GetFacingDirection(), directionToPushable );

            Debug.Log( i + ": " + angleToPushable );
            if( angleToPushable < 40 )
            {
                if( angleToPushable < angleToClosestPushable )
                {
                    closestPushable = colliderPushable;
                    angleToClosestPushable = angleToPushable;
                }
            }
        }

        return closestPushable;
    }

    void UpdatePushing()
    {
        Vector3 position = transform.position;

        if( Vector3.Distance( position, m_LastPosition ) > 0.005f )
        {
            m_LastPosition = position;
            m_LastPositionTime = Time.realtimeSinceStartup;
        }
    }

    float GetMovingDuration()
    {
        if( m_MovementModel.IsMoving() == false )
        {
            return 0f;
        }

        return Time.realtimeSinceStartup - m_MovementStartTime;
    }

    float GetTimeInSamePosition()
    {
        return Time.realtimeSinceStartup - m_LastPositionTime;
    }

    public bool IsPushing()
    {
        if( m_MovementModel.IsMoving() == false || m_WasMoving == false )
        {
            return false;
        }

        if( m_ClosestPushable != null )
        {
            return true;
        }

        if( m_MovementModel.IsFrozen() == true )
        {
            return false;
        }

        if( GameCamera.Instance.IsSwitchingScene() == true )
        {
            return false;
        }

        return GetMovingDuration() > 0.1f &&
               GetTimeInSamePosition() > 0.1f;
    }

    public bool IsPushingAndWalking()
    {
        if( IsPushing() == false )
        {
            return false;
        }

        return GetTimeInSamePosition() < 0.1f;
    }
}
