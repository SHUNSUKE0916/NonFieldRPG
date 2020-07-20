using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManger : MonoBehaviour
{
    //スタートボタンが押されたら行う
    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(0);
    }
}
