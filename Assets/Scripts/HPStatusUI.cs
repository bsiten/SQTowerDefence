using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HPStatusUI : MonoBehaviour
{
    //　敵のステータス
    private Entity.Status status;

    //　HP表示用スライダー
    private Slider hpSlider;

    void Start()
    {
        //　自身のルートに取り付けているステータス取得
        status = transform.root.GetComponent<Entity.Status>();
        //　HP用Sliderを子要素から取得
        hpSlider = transform.Find("HPBar").GetComponent<Slider>();
        //　スライダーの値0～1の間になるように比率を計算
        hpSlider.value = (float)status.maxHealth / (float)status.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;
    }

    //　死んだらHPUIを非表示にする
    public void SetDisable()
    {
        gameObject.SetActive(false);
    }

    public void UpdateHPValue()
    {
        hpSlider.value = (float)status.health / (float)status.maxHealth;
    }
}