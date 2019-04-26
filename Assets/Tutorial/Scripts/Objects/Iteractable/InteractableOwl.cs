using UnityEngine;
using System.Collections;

public class InteractableOwl : InteractableBase
{
    public override void OnInteract( Character character )
    {

        this.PlayInteractionSound();
        
    }
}
