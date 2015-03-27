using UnityEngine;
using System.Collections;

public class CharacterMovementBaseControl : MonoBehaviour
{
    private CharacterMovementModel m_MovementModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    protected void SetDirection( Vector2 direction )
    {
        m_MovementModel.SetDirection( direction );
    }
}
