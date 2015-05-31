using UnityEngine;
using System.Collections;

public class DestroyAnimationListener : MonoBehaviour 
{
    public void DoDestroy()
    {
        Destroy( gameObject );
    }
}
