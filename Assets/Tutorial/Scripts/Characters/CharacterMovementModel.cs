using UnityEngine;
using System.Collections;

public class CharacterMovementModel : MonoBehaviour 
{
    public float Speed;

    private Vector3 m_MovementDirection;

    void Start() 
    {
        
    }

    void Update()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        if( m_MovementDirection == Vector3.zero )
        {
            return;
        }

        m_MovementDirection.Normalize();

        transform.position += m_MovementDirection * Speed * Time.deltaTime;
    }

    public void SetDirection( Vector2 direction )
    {
        m_MovementDirection = new Vector3( direction.x, direction.y, 0 );
    }

    public Vector3 GetDirection()
    {
        return m_MovementDirection;
    }

    public bool IsMoving()
    {
        return m_MovementDirection != Vector3.zero;
    }
}
