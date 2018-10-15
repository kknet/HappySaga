using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

/**
 * Author :     XiaoYing
 * Version:     1.0
 * Date：       2018-10-12
 * Description：视频按钮
 * Change History:
 */

public class RewardedButton : MonoBehaviour {
    public GameObject content;
    public UnityEvent countDownEvent;
    public UnityEvent buttonHide;
    private const string ACTION_NAME = "rewarded_video";
    private bool isEventAttached;

    private void Start() {

#if UNITY_ANDROID || UNITY_IOS
        Timer.Schedule (this,0.1f,AddEvents);
        if (!IsAvailableToShow())
        {
            content.SetActive (false);
        }

        InvokeRepeating ("IUpdate", 1, 1);
#else
        content.SetActive (false);
#endif
    }

    private void AddEvents() {
        AdController.instance.OnAdRewarded += HandleRewardBasedVideoRewarded;
        AdController.instance.OnAdLoaded += HandleRewardVideoLoaded;

    }

    private void HandleRewardVideoLoaded(object sender, EventArgs e) {
        Debug.Log ("rewardVideo loaded");
    }
    private void HandleRewardBasedVideoRewarded(object sender, EventArgs e) {
        Debug.Log ("rewardVideo rewarded");

        if (((AdController.RewardTypeEventArgs)e).rewardType == null) {

            //var dialog = (RewardedVideoDialog)DialogController.instance.GetDialog (DialogType.RewardedVideo);
            //dialog.SetAmount (ConfigController.Config.rewardedVideoAmount);
            //DialogController.instance.ShowDialog (dialog);
            //CUtils.SetActionTime (ACTION_NAME);
            content.SetActive (false);
            if (buttonHide != null) {
                buttonHide.Invoke ();
            }
        }

    }

    private void IUpdate() {
        content.SetActive (IsAvailableToShow ());
        if (!IsAvailableToShow ()) {
            if (buttonHide != null) {
                buttonHide.Invoke ();
            }
        } else {
            if (countDownEvent != null) {
                countDownEvent.Invoke ();
            }
        }
    }

    public void OnClick() {

        AdController.instance.ShowRewardedVideo ();
        //Sound.instance.PlayButton ();//测试用

    }



    public bool IsAvailableToShow() {
        return IsActionAvailable () && IsAdAvailable ();
    }

    private bool IsActionAvailable() {
        //return CUtils.IsActionAvailable (ACTION_NAME, ConfigController.Config.rewardedVideoPeriod);
        return true;
    }

    private bool IsAdAvailable() {
        bool isLoaded = AdController.instance.IsRewardedVideoLoaded;
        return isLoaded;
    }

    private void OnDestroy() {
#if UNITY_ANDROID || UNITY_IOS
        AdController.instance.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        AdController.instance.OnAdLoaded -= HandleRewardVideoLoaded;
#endif
    }


}
