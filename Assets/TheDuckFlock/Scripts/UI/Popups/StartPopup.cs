using DG.Tweening;

namespace TheDuckFlock
{
    public class StartPopup : PopupBase
    {
        private void OnEnable()
        {
            buttonPlay.gameObject.SetActive(true);
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnButtonPlayClick()
        {
            buttonPlay.gameObject.SetActive(false);

            UIEventsManager.DispatchEvent(UIEvent.CloseStartPopup);

        }

        public void HideStartPopup()
        {
            HidePopup(true);
        }

        public void ShowStartPopup()
        {
            ShowPopup(true);

            buttonPlay.transform.DOScale(1f, showAnimationDuration)
                .SetDelay(showAnimationDuration / 2)
                .From(0f, true)
                .SetEase(Ease.OutBack);
        }
        
    }
}
