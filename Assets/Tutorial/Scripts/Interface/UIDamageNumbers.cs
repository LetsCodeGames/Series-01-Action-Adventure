using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDamageNumbers : MonoBehaviour 
{
    public static UIDamageNumbers Instance;

    public GameObject DamageNumberPrefab;

    RectTransform m_RectTransform;

    void Awake()
    {
        Instance = this;
        m_RectTransform = GetComponent<RectTransform>();
    }

    public void ShowDamageNumber( float damage, Vector3 worldPosition )
    {
        GameObject newDamageNumberObject = Instantiate<GameObject>( DamageNumberPrefab );
        newDamageNumberObject.transform.GetChild( 0 ).GetComponent<Text>().text = Mathf.RoundToInt( damage ).ToString();

        Vector3 screenPosition = Camera.main.WorldToViewportPoint( worldPosition );

        RectTransform damageNumberTransform = newDamageNumberObject.GetComponent<RectTransform>();

        damageNumberTransform.SetParent( transform, true );
        damageNumberTransform.localScale = Vector3.one;
        damageNumberTransform.anchoredPosition = new Vector2( screenPosition.x * m_RectTransform.rect.width, screenPosition.y * m_RectTransform.rect.height );

        Destroy( newDamageNumberObject, 2f );
    }
}
