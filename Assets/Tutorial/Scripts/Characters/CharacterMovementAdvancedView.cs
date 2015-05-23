using UnityEngine;
using System.Collections;

public class CharacterMovementAdvancedView : MonoBehaviour 
{
    public Animator Animator;

    CharacterMovementAdvancedModel m_Model;

    void Awake() 
    {
        m_Model = GetComponent<CharacterMovementAdvancedModel>();
    }
}
