using UnityEngine;
using System.Collections;

public class InteractableSign : InteractableBase
{
    public string Text;

    public override void OnInteract( Character character )
    {
        if( DialogBox.IsVisible() == true )
        {
            character.Movement.SetFrozen( false, false, true );
            DialogBox.Hide();
        }
        else
        {
            character.Movement.SetFrozen( true, true, true );
            DialogBox.Show( Text );
        }
        
    }

    
}
