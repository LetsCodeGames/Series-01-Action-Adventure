using UnityEngine;
using System.Collections;

public class CharacterInteractionView : MonoBehaviour 
{
    public Animator Animator;

    CharacterInteractionModel m_Model;

    void Awake() 
    {
        m_Model = GetComponent<CharacterInteractionModel>();
    }

    void Update() 
    {
        UpdateIsCarryingObject();
    }

    void UpdateIsCarryingObject()
    {
        Animator.SetBool( "IsCarrying", m_Model.IsCarryingObject() );
    }
}
