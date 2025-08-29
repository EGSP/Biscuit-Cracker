using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Code.Ui
{
    public class BiscuitPointsBadge : MonoBehaviour
    {
        public TMP_Text PointsText;
        public Vector3 WorldOffset = new Vector3(0, 0,-0.2f);

        private Biscuit _biscuit = null;

        public Biscuit Biscuit
        {
            get => _biscuit;
            set
            {
                if (_biscuit != null)
                {
                    _biscuit.OnClickedPointsChanged.RemoveListener(UpdateBiscuitPoints);
                    _biscuit.OnDestroy.RemoveListener(OnBiscuitDespawned);
                }
                _biscuit = value;
                if (_biscuit != null)
                {
                    _biscuit.OnDestroy.AddListener(OnBiscuitDespawned);
                    _biscuit.OnClickedPointsChanged.AddListener(UpdateBiscuitPoints);
                    UpdateBiscuitPoints(_biscuit.ClickedPoints, 0);
                }
            }
        }


        // Update is called once per frame
        void LateUpdate()
        {
            UpdateScreenPosition();
        }

        private void UpdateScreenPosition()
        {
            if (Biscuit != null)
            {
                var worldPos = Biscuit.transform.position + WorldOffset;
                var screenPos = Camera.main.WorldToScreenPoint(worldPos);
                transform.position = screenPos;
            }
        }

        private void UpdateBiscuitPoints(int points, int change)
        {
            var clickPointsLeft = Biscuit.ClickPoints - points;
            PointsText.text = clickPointsLeft.ToString();
        }

        private void OnBiscuitDespawned()
        {
            if(Biscuit != null)
            {
                Biscuit = null;
            }
            Destroy(gameObject);
        }
    }
}