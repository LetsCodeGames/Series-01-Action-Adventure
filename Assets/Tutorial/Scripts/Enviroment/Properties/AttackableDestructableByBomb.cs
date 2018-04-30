using UnityEngine;
using System.Collections;

public class AttackableDestructableByBomb : DestructableByBomb 
{
    public override void OnDestroyedByBomb()
    {
        GetComponent<AttackableBase>().OnHit( null, ItemType.Bomb );
    }
}
