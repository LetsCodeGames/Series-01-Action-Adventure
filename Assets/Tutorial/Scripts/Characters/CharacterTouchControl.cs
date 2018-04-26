using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterTouchControl : CharacterBaseControl 
{
    bool m_IsMoving;
    int m_MovingFingerId;
    Vector2 m_MovingReferencePosition;
    Vector2 m_LastDirection;

    void Start() 
    {
        
    }

    void Update() 
    {
        UpdateTouches();
    }

    void UpdateTouches()
    {
        for( int i = 0; i < Input.touchCount; ++i )
        {
            UpdateTouch( Input.touches[ i ] );
        }
    }

    void UpdateTouch( Touch touch )
    {
        switch( touch.phase )
        {
        case TouchPhase.Began:
            HandleTouchBegan( touch );
            break;
        case TouchPhase.Ended:
        case TouchPhase.Canceled:
            HandleTouchEnd( touch );
            break;
        case TouchPhase.Moved:
        case TouchPhase.Stationary:
            HandleTouchMove( touch );
            break;
        }
    }

    void HandleTouchBegan( Touch touch )
    {
        Vector2 viewportPosition = Camera.main.ScreenToViewportPoint( touch.position );

        if( viewportPosition.x > 0.5f )
        {
            if( m_Character.Interaction.IsCarryingObject() == true ||
                m_Character.Interaction.FindUsableInteractable() != null )
            {
                OnActionPressed();
            }
            else
            {
                OnAttackPressed();
            }
        }
        else
        {
            if( m_IsMoving == false )
            {
                m_IsMoving = true;
                m_MovingFingerId = touch.fingerId;
                m_MovingReferencePosition = touch.position;
                m_LastDirection = Vector2.zero;
            }
        }
    }

    void HandleTouchEnd( Touch touch )
    {
        if( m_IsMoving == true && touch.fingerId == m_MovingFingerId )
        {
            m_IsMoving = false;
        }
    }

    void HandleTouchMove( Touch touch )
    {
        if( m_IsMoving == true && touch.fingerId == m_MovingFingerId )
        {
            float dpi = Screen.dpi;
            if( dpi < 25 || dpi > 1000 )
            {
                dpi = 150;
            }

            Vector2 difference = touch.position - m_MovingReferencePosition;
            Vector2 physicalDifference = difference / dpi;

            float threshold = 0.1f;

            if( physicalDifference.x > threshold )
            {
                m_LastDirection.x++;

                if( m_LastDirection.x > 1 )
                {
                    m_LastDirection.x = 1;
                    m_LastDirection.y = 0;
                }

                m_MovingReferencePosition = touch.position;
                
            }
            else if( physicalDifference.x < -threshold )
            {
                m_LastDirection.x--;

                if( m_LastDirection.x < -1 )
                {
                    m_LastDirection.x = -1;
                    m_LastDirection.y = 0;
                }

                m_MovingReferencePosition = touch.position;
            }

            if( physicalDifference.y > threshold )
            {
                m_LastDirection.y++;

                if( m_LastDirection.y > 1 )
                {
                    m_LastDirection.x = 0;
                    m_LastDirection.y = 1;
                }

                m_MovingReferencePosition = touch.position;

            }
            else if( physicalDifference.y < -threshold )
            {
                m_LastDirection.y--;

                if( m_LastDirection.y < -1 )
                {
                    m_LastDirection.x = 0;
                    m_LastDirection.y = -1;
                }

                m_MovingReferencePosition = touch.position;
            }

            //Debug.Log( physicalDifference.x.ToString( "0.00000" ) + ", " + physicalDifference.y.ToString( "0.00000" ) + " - " + m_LastDirection );

            SetDirection( m_LastDirection );
        }
    }
}
