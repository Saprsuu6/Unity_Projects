using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCanvas : MonoBehaviour
{
    [SerializeField]
    private Player Player;

    [SerializeField]
    private GameObject Coin;
    private Renderer CoinRenderer;

    private TMPro.TextMeshProUGUI coinDistanceText;
    private TMPro.TextMeshProUGUI coinResultText;
    private Image arrowImage;                 // стрелка компаса
    private Image staminaIndicator;           // индикатор выносливости
    private Image staminaIndicatorBack;
    private TMPro.TextMeshProUGUI LeftHint;   // Боковые "стрелки" подсказывающие
    private TMPro.TextMeshProUGUI RightHint;  // направление на монету


    void Start()
    {
        coinDistanceText = GameObject.Find("CoinDistanceText")
            .GetComponent<TMPro.TextMeshProUGUI>();

        arrowImage = GameObject.Find("ArrowImage")
            .GetComponent<Image>();

        staminaIndicator = GameObject.Find("Front")
           .GetComponent<Image>();

        staminaIndicatorBack = GameObject.Find("Back")
           .GetComponent<Image>();

        LeftHint = GameObject.Find("LeftHint")
            .GetComponent<TMPro.TextMeshProUGUI>();

        RightHint = GameObject.Find("RightHint")
            .GetComponent<TMPro.TextMeshProUGUI>();

        coinResultText = GameObject.Find("CoinResultText")
            .GetComponent<TMPro.TextMeshProUGUI>();

        CoinRenderer = Coin.GetComponentInChildren<Renderer>();

        LeftHint.enabled = false;
    }

    void Update()
    {
        float coinDistance = (Coin.transform.position - Player.transform.position).magnitude;
        coinDistanceText.text = coinDistance.ToString("0.0");
        coinDistanceText.color = new Color(
            1 / (1 + coinDistance / 10),  // Red - чем ближе, тем больше
            0.2f,
            1 - 1 / (1 + coinDistance / 10)  // Blue - наоборот, чем ближе тем меньше
        );

        #region Compas
        // Компас     coin
        //          /
        //         /  угол поворота компаса (стрелки)
        //  Player ------> forward
        //
        // Задача: определить угол между Player.forward и направлением на монету
        float angleCoinPlayer = Vector3.SignedAngle(
            Coin.transform.position - Player.transform.position,
            Player.transform.forward,
            Vector3.up
        );
        arrowImage.transform.eulerAngles = new Vector3(0, 0, angleCoinPlayer);
        #endregion

        #region Hints
        if (CoinRenderer.isVisible) 
        {
            LeftHint.enabled = false;
            RightHint.enabled = false;
        }
        else
        {
            if(angleCoinPlayer > 0)
            {
                LeftHint.enabled = true;
                RightHint.enabled = false;
            }
            else
            {
                RightHint.enabled = true;
                LeftHint.enabled = false;
            }
        }
        #endregion

        coinResultText.text = Player.CoinCount.ToString();
        staminaIndicator.fillAmount = Player.Stamina;
    }

    private void LateUpdate()
    {
        coinDistanceText.enabled = GameSettings.DisplayCoinDistance;
        LeftHint.enabled = RightHint.enabled = arrowImage.enabled = GameSettings.DisplayDirectionHint;
        staminaIndicatorBack.enabled = staminaIndicator.enabled = GameSettings.DisplayStamina;
    }
}