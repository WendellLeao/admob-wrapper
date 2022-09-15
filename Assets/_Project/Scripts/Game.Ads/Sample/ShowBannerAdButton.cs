using UnityEngine.UI;
using Game.Services;
using UnityEngine;

namespace Game.Ads.Sample
{
    public sealed class ShowBannerAdButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private IAdService _adService;

        private void OnEnable()
        {
            _adService = ServiceLocator.GetService<IAdService>();
            
            _button.onClick.AddListener(HandleButtonClick);
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            _adService.ShowBannerAd();
        }
    }
}