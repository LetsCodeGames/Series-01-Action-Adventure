using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIArmorBar : MonoBehaviour 
{
    public CharacterHealthModel HealthModel;
    public Text ArmorText;
    public RectTransform ArmorBar;
    public Image[] Images;

    void Update()
    {
        if( HealthModel.GetTotalMaximumArmor() == 0 )
        {
            SetImagesVisible( false );
            ArmorText.text = "";
            //Hide
        }
        else
        {
            SetImagesVisible( true );
            UpdateText();
            UpdateBar();
        }
    }

    void UpdateText()
    {
        ArmorText.text = Mathf.RoundToInt( HealthModel.GetTotalArmor() ) + "/" +
            Mathf.RoundToInt( HealthModel.GetTotalMaximumArmor() );
    }

    void UpdateBar()
    {
        ArmorBar.localScale = new Vector3( HealthModel.GetTotalArmorPercentage(), 1f, 1f );
    }

    void SetImagesVisible( bool visible )
    {
        foreach( Image image in Images )
        {
            image.enabled = visible;
        }
    }
}
