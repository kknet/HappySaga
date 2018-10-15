using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.zonglv.minigame.common;

public class AdController : MonoBehaviour {

    AndroidJavaClass unityPlayer;
    AndroidJavaObject currentActivity;

    AndroidJavaObject tolerManager;
    public static AdController instance;

    private static string rewardedVideoListenerClassName = "com.toler.tolersdk.listener.RewardedVideoListener";
    private static string interstitialListenerListenerClassName = "com.toler.tolersdk.listener.InterstitialListener";
    public static string mLastRewardType = null;

    public bool IsRewardedVideoLoaded {
        get; set;
    }
    public bool IsInterstitialLoaded {
        get; set;
    }

    public event EventHandler<RewardTypeEventArgs> OnAdRewarded;
    public event EventHandler OnAdLoaded;
    private void Awake() {
        instance = this;
    }
    void Start() {
#if UNITY_ANDROID || UNITY_IPHONE
        if (!ConfigController.instance.config.showad) { return; }
        //获取context
        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaClass androidCall = new AndroidJavaClass("com.toler.sdk.TolerManager");
        tolerManager = androidCall.CallStatic<AndroidJavaObject>("getInstance", currentActivity);

        RewardedVideoListener rewardedVideoListener = new RewardedVideoListener();
        rewardedVideoListener.OnAdLoaded += HandlerRewardedVideoLoaded;
        rewardedVideoListener.OnAdRewarded += HandlerRewardedVideoRewaded;
        rewardedVideoListener.OnAdStarted += HandlerRewardedVideoStarted;
        rewardedVideoListener.OnAdFialed += HandlerRewardedVideoFailed;

        InterstitialListener interstitialListener = new InterstitialListener();
        interstitialListener.OnAdShowSucceeded += HandlerInerstitialShowSucceeded;
        interstitialListener.OnAdShowFailed += HandlerInerstitialShowFailed;
        interstitialListener.OnAdShowReady += HandlerInterstitialShowReady;
        interstitialListener.OnAdShowClosed += HandlerInterstitialShowClosed;


        if (tolerManager != null) {
            Debug.Log("TolerManager is not null");
            tolerManager.Call("setRewardedVideoListener", rewardedVideoListener);
            tolerManager.Call("setInterstitialListener", interstitialListener);
        } else {
            Debug.Log("TolerManager is null");
        }
#endif
    }

    private void HandlerInterstitialShowClosed(object sender, EventArgs e) {
        LoadInterstitial ();
    }

    private void HandlerInterstitialShowReady(object sender, EventArgs e) {
        Debug.Log ("interstitial ready");
        IsInterstitialLoaded = true;

    }

    private void HandlerInerstitialShowFailed(object sender, EventArgs e) {
        IsInterstitialLoaded = false;
        LoadInterstitial ();
        Debug.Log ("HandlerInerstitialShowFailed");
    }

    private void HandlerInerstitialShowSucceeded(object sender, EventArgs e) {
        Debug.Log ("HandlerInerstitialShowSucceeded");
        CUtils.SetActionTime (CUtils.ActionInterstitial);
    }

    private void HandlerRewardedVideoFailed(object sender, EventArgs e) {
        IsRewardedVideoLoaded = false;
    }

    private void HandlerRewardedVideoStarted(object sender, EventArgs e) {
        IsRewardedVideoLoaded = false;

    }

    private void HandlerRewardedVideoLoaded(object sender, EventArgs e) {
        IsRewardedVideoLoaded = true;
        if (OnAdRewarded != null) {
            this.OnAdLoaded (this, EventArgs.Empty);
        }

    }
    private void HandlerRewardedVideoRewaded(object sender, RewardTypeEventArgs rewardType) {

        if (OnAdRewarded != null) {
            this.OnAdRewarded (this, rewardType);
        }

    }

    void Update() {
    }

    public void ShowRewardedVideo(String rewardType = null) {
        if (tolerManager != null) {
            mLastRewardType = rewardType;
            tolerManager.Call ("showRewardedVideo", "显示激励型视频");
        }
    }
    public void LoadInterstitial() {
        if (tolerManager != null) {
            tolerManager.Call ("loadInterstitial", "加载插屏");
        }
    }
    public void ShowInterstitial() {
        IsInterstitialLoaded = false;
        if (tolerManager != null) {
            tolerManager.Call ("showInterstitial", "显示插屏");
        }
    }
    public void ShowBanner() {
        if (tolerManager != null) {
            tolerManager.Call ("showBanner", "显示Banner");
        }
    }

    public void HideBanner() {
        if (tolerManager != null) {
            tolerManager.Call ("hideBanner", "隐藏Banner");
        }
    }

    class RewardedVideoListener : AndroidJavaProxy {
        public event EventHandler<EventArgs> OnAdLoaded;
        public event EventHandler<EventArgs> OnAdFialed;
        public event EventHandler<EventArgs> OnAdStarted;
        public event EventHandler<RewardTypeEventArgs> OnAdRewarded;

        public RewardedVideoListener() : base (AdController.rewardedVideoListenerClassName) {
        }

        public void onRewardedVideoAdOpened() {
        }
        public void onRewardedVideoAdClosed() {
        }
        public void onRewardedVideoAvailabilityChanged(bool available) {
            Debug.Log ("onRewardedVideoAvailabilityChanged value:" + available);
            if (available && this.OnAdLoaded != null) {
                this.OnAdLoaded (this, EventArgs.Empty);
            }
        }
        public void onRewardedVideoAdStarted() {
            Debug.Log ("onRewardedVideoAdStarted");
            this.OnAdStarted (this, EventArgs.Empty);
        }
        public void onRewardedVideoAdEnded() {
            Debug.Log ("onRewardedVideoAdEnded");
        }
        public void onRewardedVideoAdRewarded(String placementName) {
            Debug.Log ("onRewardedVideoAdRewarded");
            this.OnAdRewarded (this, new RewardTypeEventArgs (mLastRewardType));
        }
        public void onRewardedVideoAdShowFailed(AndroidJavaObject error) {
            Debug.Log ("onRewardedVideoAdShowFailed");
            AdController.mLastRewardType = null;
            this.OnAdFialed (this, EventArgs.Empty);
        }
        public void onRewardedVideoAdClicked(String placementName) {
            Debug.Log ("onRewardedVideoAdClicked");
        }
    }
    class InterstitialListener : AndroidJavaProxy {
        public event EventHandler<EventArgs> OnAdShowSucceeded;
        public event EventHandler<EventArgs> OnAdShowFailed;
        public event EventHandler<EventArgs> OnAdShowReady;
        public event EventHandler<EventArgs> OnAdShowClosed;


        public InterstitialListener() : base (AdController.interstitialListenerListenerClassName) {
        }
        public void onInterstitialAdReady() {
            if (OnAdShowReady != null) {
                this.OnAdShowReady (this, EventArgs.Empty);
            }
            Debug.Log ("onInterstitialAdReady");
        }

        public void onInterstitialAdLoadFailed(AndroidJavaObject error) {
            if (OnAdShowFailed != null) {
                this.OnAdShowFailed (this, EventArgs.Empty);
            }
            Debug.Log ("onInterstitialAdLoadFailed");

        }

        public void onInterstitialAdOpened() {
            Debug.Log ("onInterstitialAdOpened");
        }

        public void onInterstitialAdClosed() {
            if (OnAdShowClosed != null) {
                this.OnAdShowClosed (this, EventArgs.Empty);
            }
            Debug.Log ("onInterstitialAdClosed");
        }

        public void onInterstitialAdShowSucceeded() {
            if (OnAdShowSucceeded != null) {
                this.OnAdShowSucceeded (this, EventArgs.Empty);
            }
            Debug.Log ("onInterstitialAdShowSucceeded");
        }

        public void onInterstitialAdClicked() {
            Debug.Log ("onInterstitialAdClicked");
        }
    }

    void OnGUI() {
        if (ConfigController.instance.config.adtest) {
            GUI.skin.textArea.fontSize = 50;
            GUI.skin.button.fontSize = 50;

            // rewarded video
            if (GUI.Button (new Rect (100, 300, 450, 300), "show Rewarded")) {
                tolerManager.Call ("showRewardedVideo", "显示激励型视频");
            }

            // interstitial
            if (GUI.Button (new Rect (600, 300, 450, 300), "show Interstitial")) {
                tolerManager.Call ("showInterstitial", "显示插屏");
            }

            // banner
            if (GUI.Button (new Rect (100, 700, 450, 300), "show Banner")) {
                tolerManager.Call ("showBanner", "显示Banner");
            }
        }
    }

    public class RewardTypeEventArgs : EventArgs {
        public readonly string rewardType;

        public RewardTypeEventArgs(String rewardType) {
            this.rewardType = rewardType;
        }

    }


}

