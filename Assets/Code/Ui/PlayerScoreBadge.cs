using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Code.Ui
{
    public class PlayerScoreBadge : MonoBehaviour
    {
        public TMP_Text scoreText;

        public void UpdateScore(float score, float scoreAdded)
        {
            scoreText.text = score.ToString();
        }
    }
}