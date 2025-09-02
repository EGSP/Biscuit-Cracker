using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.Ui.Upgrades
{
    public class NoiseDecayUpgradeUi : MonoBehaviour
    {
        public TMP_Text CostText;
        public TMP_Text SummuryText;

        public Button UpgradeButton;

        public NoiseDecayUpgrade Upgrade;

        private void Awake()
        {
            UpdateUi();
        }

        public void DoUpgrade()
        {
            var cost = Upgrade.GetCost();

            Upgrade.Level++;
            GameManager.Instance.Score -= cost;
        }

        public void CheckScoreToAllowUpgrade(float score, float added)
        {
            var cost = Upgrade.GetCost();
            if (score < cost)
            {
                UpgradeButton.interactable = false;
            }
            else
            {
                UpgradeButton.interactable = true;
            }

            UpdateUi();
        }

        private void UpdateUi()
        {
            CostText.text = Upgrade.GetCost().ToString();
            SummuryText.text = Upgrade.GetUpgradeSummury().ToString();
        }
    }
}