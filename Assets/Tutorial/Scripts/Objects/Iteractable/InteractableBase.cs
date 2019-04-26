using UnityEngine;
using System.Collections;

public class InteractableBase : MonoBehaviour 
{
    AudioSource interactionSound;

    protected virtual void Awake()
    {
        interactionSound = GetComponent(typeof(AudioSource)) as AudioSource;
    }
    virtual public void OnInteract( Character character )
    {
        Debug.LogWarning( "OnInteract is not implemented" );
    }
    public void PlayInteractionSound() {
        if (!interactionSound) {
            Debug.Log("No audio selected!");
            return;
        }
        interactionSound.Play();
    }
}
