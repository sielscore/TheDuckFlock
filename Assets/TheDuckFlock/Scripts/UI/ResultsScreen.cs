using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TheDuckFlock
{
    public class ResultsScreen : MonoBehaviour
    {
        [SerializeField] private GameObject panel;

        [SerializeField] private TextMeshProUGUI titleLabel;

        [SerializeField] private TextMeshProUGUI hatchedDuckiesValueText;
        [SerializeField] private TextMeshProUGUI lostDuckiesValueText;

        
        private int hatchedEggs = 0;
        private int lostDuckies = 0;

        // Start is called before the first frame update
        void Start()
        {
           // UpdateValues(3, 4);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateValues(int hatched, int lost)
        {
            hatchedEggs = hatched;
            lostDuckies = lost;

            RefreshValues();
        }

        public void SwitchVisibility(bool isVisible)
        {
            panel.SetActive(isVisible);
        }

        public void RefreshValues()
        {
            hatchedDuckiesValueText.text = "" + ScoreManager.Instance.CurrentScore;
            lostDuckiesValueText.text = "" + ScoreManager.Instance.LostDuckies;

            titleLabel.text = "Level " + ScoreManager.Instance.CurrentLevel;
        }
    }
}
