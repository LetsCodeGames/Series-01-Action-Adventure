using UnityEngine;
using System.Collections;

public class CharacterMovementAdvancedModel : MonoBehaviour 
{
    
    CharacterMovementModel m_MovementModel;

    void Awake()
    {
        m_MovementModel = GetComponent<CharacterMovementModel>();
    }

    
}
