using UnityEngine;
using System.Collections;

public class InteractableSign : InteractableBase
{
    public string Text;

    public override void OnInteract( Character character )
    {
        if( DialogBox.IsVisible() == true )
        {
            Time.timeScale = 1;
            character.Movement.SetFrozen( false );
            DialogBox.Hide();
        }
        else
        {
            character.Movement.SetFrozen( true );

            StartCoroutine( FreezeTimeRoutine() );
            DialogBox.Show( Text );
        }
        
    }

    IEnumerator FreezeTimeRoutine()
    {
        yield return null;

        Time.timeScale = 0;
    }
}
