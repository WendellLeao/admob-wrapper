using GoogleMobileAds.Api;
using Game.Services;
using UnityEngine;
using System;

namespace Game.Ads
{
    public sealed class AdService : MonoBehaviour, IAdService
    {
        private const string AndroidBannerAdId = "";
        private const string IOSBannerAdId = "";
        private const string AndroidInterstitialAdId = "";
        private const string IOSInterstitialAdId = "";
        private const string UnexpectedPlatformId = "unexpected_platform";

        private BannerView _bannerView;
        private InterstitialAd _interstitialAd;

        public void ShowBannerAd()
        {
            RequestBanner();
            
            AdRequest request = new AdRequest.Builder().Build();
            
            _bannerView.LoadAd(request);
        }

        public void ShowInterstitialAd()
        {
            RequestInterstitial();
            
            if (!_interstitialAd.IsLoaded())
            {
                return;
            }
            
            _interstitialAd.Show();
        }
        
        private void RequestBanner()
        {
#if UNITY_ANDROID
            string bannerUnitID = AndroidBannerAdId;
#elif UNITY_IPHONE
            string bannerUnitID = IOSBannerAdId;
#else
            string bannerUnitID = UnexpectedPlatformId;
#endif
            _bannerView = new BannerView(bannerUnitID, AdSize.Banner, AdPosition.Bottom);
            
            _bannerView.OnAdLoaded += HandleOnAdLoaded;
            _bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            _bannerView.OnAdOpening += HandleOnAdOpened;
            _bannerView.OnAdClosed += HandleOnAdClosed;
        }
        
        private void RequestInterstitial()
        {
#if UNITY_ANDROID
            string interstitialUnitID = AndroidBannerAdId;
#elif UNITY_IPHONE
            string interstitialUnitID = IOSBannerAdId;
#else
            string interstitialUnitID = UnexpectedPlatformId;
#endif
            
            _interstitialAd = new InterstitialAd(interstitialUnitID);
            
            _interstitialAd.OnAdLoaded += HandleOnAdLoaded;
            _interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
            _interstitialAd.OnAdOpening += HandleOnAdOpening;
            _interstitialAd.OnAdClosed += HandleOnAdClosed;

            AdRequest request = new AdRequest.Builder().Build();
            
            _interstitialAd.LoadAd(request);
        }
        
        private void Awake()
        {
            ServiceLocator.RegisterService<IAdService>(this);
            
            MobileAds.Initialize((initializationStatus) =>
            {
                Debug.Log("MobileAds has initialized!");
            });
        }
        
        private void HandleOnAdLoaded(object sender, EventArgs args)
        {
            Debug.Log("HandleAdLoaded event received");
        }

        private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
        {
            Debug.Log("HandleFailedToReceiveAd event received with message: " + args);
        }

        private void HandleOnAdOpening(object sender, EventArgs args)
        {
            Debug.Log("HandleAdOpening event received");
        }

        private void HandleOnAdOpened(object sender, EventArgs args)
        {
            Debug.Log("HandleAdOpened event received");
        }

        private void HandleOnAdClosed(object sender, EventArgs args)
        {
            Debug.Log("HandleAdClosed event received");
        }
    }
}
