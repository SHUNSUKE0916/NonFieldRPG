using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] Text hptext;
    [SerializeField] Text attext;

    public void SetUpUI(Playeranager player)
    {
        hptext.text = string.Format("HP : {0}", player.hp);
        attext.text = string.Format("攻撃力 : {0}", player.at);
    }


    public void UpdateUI(Playeranager player)
    {
        hptext.text = string.Format("HP : {0}", player.hp);
    }
}
