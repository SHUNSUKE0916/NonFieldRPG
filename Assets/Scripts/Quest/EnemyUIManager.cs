using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{
    [SerializeField] Text hptext;
    [SerializeField] Text nametext;

    public void SetUpUI(EnemuManager enemy)
    {
        hptext.text = string.Format("HP : {0}", enemy.hp);
        nametext.text = string.Format("{0}", enemy.name);
    }

    public void UpdateUI(EnemuManager enemy)
    {
        hptext.text = string.Format("HP : {0}", enemy.hp);
    }

}
