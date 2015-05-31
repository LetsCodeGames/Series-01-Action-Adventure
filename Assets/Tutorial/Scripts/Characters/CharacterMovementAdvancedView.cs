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

    void Update()
    {
        UpdatePushing();
    }

    void UpdatePushing()
    {
        Animator.SetBool( "IsPushing", m_Model.IsPushing() );
        Animator.SetBool( "IsPushingAndWalking", m_Model.IsPushingAndWalking() );
    }
}
