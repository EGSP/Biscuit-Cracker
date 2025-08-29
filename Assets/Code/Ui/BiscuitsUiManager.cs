using System.Collections;
using UnityEngine;

namespace Assets.Code.Ui
{
    public class BiscuitsUiManager : MonoBehaviour
    {
        public BiscuitPointsBadge BiscuitPointsBadgePrefab;
        public RectTransform BiscuitBadgesParent;



        public void CreateBiscuitPointsBadge(Biscuit biscuit)
        {
            var badge = Instantiate(BiscuitPointsBadgePrefab, BiscuitBadgesParent);
            badge.Biscuit = biscuit;
        }
    }
}