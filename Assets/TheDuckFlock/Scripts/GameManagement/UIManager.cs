using TMPro;
using UnityEngine;
using DG.Tweening;

namespace TheDuckFlock
{
    public class UIManager : MonoSingleton<UIManager>, IUIEventsListener<int>
    {
        [SerializeField] private ResultsPopup resultsPopup;
        [SerializeField] private StartPopup startPopup;

        //[SerializeField] private Transform UIRoot;
        [SerializeField] private GameObject indicatorsRoot;
        [SerializeField] private NestIndicator nestIndicator;

        [SerializeField] private ScreenFader screenFader;

        [SerializeField] private TextMeshProUGUI duckiesCounterLabel;

        [SerializeField] private Color idleDuckieColor = Color.white;
        [SerializeField] private Color hatchedDuckieColor = Color.yellow;
        [SerializeField] private Color lostDuckieColor = Color.red;
        [SerializeField] private float duckieCounterBumpDuration = 1.6f;

        private int previousScore = 0;

        private void Awake()
        {
            UIEventsManager.SetupListeners(this);
        }

        private void OnDestroy()
        {
            UIEventsManager.RemoveListeners();
        }

        // Start is called before the first frame update
        void Start()
        {
            ShowStartScreen();  // TODO: move this to more accurate place

            SwitchIndicatorsVisibility(false); // TODO: move this 
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateDuckiesCounterLabel(int currentValue, bool isLost = false)
        {
            duckiesCounterLabel.text = currentValue.ToString();

            duckiesCounterLabel.DOColor(isLost ? lostDuckieColor : hatchedDuckieColor, duckieCounterBumpDuration/2). //.SetEase(Ease.)    
            OnComplete(() =>
            {
                duckiesCounterLabel.DOColor(idleDuckieColor, duckieCounterBumpDuration / 2);
            });

            duckiesCounterLabel.rectTransform.DOScale(1.25f, duckieCounterBumpDuration / 2).SetEase(Ease.OutBack).
                OnComplete(() =>
                {
                    duckiesCounterLabel.rectTransform.DOScale(1f, duckieCounterBumpDuration / 2).SetEase(Ease.Linear);
                });


        }


        /// <summary>
        /// 
        /// </summary>
        public void ShowResultScreen()
        {
            screenFader.DoFade(false);

            ScoreManager.Instance.SwitchScoreVisibility(false);
            SwitchIndicatorsVisibility(false);

            resultsPopup.RefreshValues();
            resultsPopup.SetupButton(ScoreManager.Instance.WasGoalAchieved);
            //resultsPopup.ShowPopup(true);
            resultsPopup.ShowResultsPopup();
        }

        public void ShowStartScreen()
        {
            SoundManager.Instance.PlayMusic(SoundTag.MusicMenu);

            startPopup.ShowStartPopup();

            ScoreManager.Instance.SwitchScoreVisibility(false);
            SwitchIndicatorsVisibility(false);
        }
        
        public void SwitchIndicatorsVisibility(bool isVisible)
        {
            indicatorsRoot.SetActive(isVisible);
            duckiesCounterLabel.gameObject.SetActive(isVisible);
           
        }

        public void SetScoreGoal(int numberOfDuckies)
        {
            foreach (Transform child in indicatorsRoot.transform)
            {
                child.parent = ObjectPooler.Instance.PoolRoot;
                child.gameObject.SetActive(false);
            }

            for (int i = 0; i < numberOfDuckies; i++)
            {
                GameObject indicatorObject = ObjectPooler.Instance.SpawnFromPool(PoolTag.ScoreIndicator);
                indicatorObject.SetActive(true);
                indicatorObject.transform.parent = indicatorsRoot.transform;

                ScoreIndicator indicator = indicatorObject.GetComponent<ScoreIndicator>();
                indicator.IsAchieved = false;
                
            }

        }

        public void UpdateScore(int numberOfDuckies)
        {
            Debug.Log(name + " | Update score " + numberOfDuckies);

            for (int iChild = 0; iChild < indicatorsRoot.transform.childCount; iChild++)
            {
                ScoreIndicator indicator = indicatorsRoot.transform.GetChild(iChild).GetComponent<ScoreIndicator>();

                indicator.IsAchieved = iChild < numberOfDuckies;

               // Debug.Log(name + " | indicator " + iChild + " " + (iChild < numberOfDuckies));
            }

            if (previousScore < numberOfDuckies) // duckie hatched
            {
                UpdateDuckiesCounterLabel(numberOfDuckies, false);
            }
            else // duckie lost
            {
                UpdateDuckiesCounterLabel(numberOfDuckies, true);
            }

            previousScore = numberOfDuckies;
        }

        public void SwitchNestIndicatorVisibility(bool isVisible)
        {
            nestIndicator.SwitchVisibility(isVisible);
        }

        #region UIEvent listeners

        public void OnCloseStartPopup(params int[] parameters)
        {
            startPopup.HidePopup(true);

            screenFader.DoFade(true);

            GameplayEventsManager.DispatchEvent(GameplayEvent.StartGame);

            //resultsPopup.ShowPopup();
        }

        public void OnShowResultsPopup(params int[] parameters)
        {
            
        }
        #endregion
    }
}
