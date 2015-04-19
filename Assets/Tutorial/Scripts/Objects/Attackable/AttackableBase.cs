using UnityEngine;
using System.Collections;

public class AttackableBase : MonoBehaviour 
{
    public virtual void OnHit( ItemType item )
    {
        Debug.LogWarning( "No OnHit Event setup for " + gameObject.name, gameObject );
    }
}
