using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextGainGold : MonoBehaviour
{
    private TextMeshProUGUI textGainGold;

    private void Awake() 
    {
        textGainGold = GetComponent<TextMeshProUGUI>();    
    }

    private void OnEnable() 
    {   
        GameManager.Instance.onUpdateGainGold += ResponseGainGold;    
    }

    private void OnDisable() 
    {
        GameManager.Instance.onUpdateGainGold -= ResponseGainGold;    
    }

    private void ResponseGainGold(int totalGold)
    {
        textGainGold.text = totalGold.ToString("N0");
    }
}