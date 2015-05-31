using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHealthBar : MonoBehaviour 
{
    public CharacterHealthModel HealthModel;
    public Text HealthText;
    public RectTransform HealthBar;

    void Update()
    {
        UpdateText();
        UpdateHealthBar();
    }

    void UpdateText()
    {
        HealthText.text = Mathf.RoundToInt( HealthModel.GetHealth() ) + "/" +
            Mathf.RoundToInt( HealthModel.GetMaximumHealth() );
    }

    void UpdateHealthBar()
    {
        HealthBar.localScale = new Vector3( HealthModel.GetHealthPercentage(), 1f, 1f );
    }
}
