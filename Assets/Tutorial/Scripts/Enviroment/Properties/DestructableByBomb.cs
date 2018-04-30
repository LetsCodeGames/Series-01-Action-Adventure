using UnityEngine;
using System.Collections;

public class DestructableByBomb : MonoBehaviour 
{
    public virtual void OnDestroyedByBomb()
    {
        Destroy( gameObject );
    }
}
