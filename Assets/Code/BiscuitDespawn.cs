using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class BiscuitDespawn : MonoBehaviour
    {
        public float DespawnRadius = 2f;
        public Transform DespawnCenter;

        private Vector3 DespawnCenterPoint
        {
            get { return DespawnCenter != null ? DespawnCenter.position : transform.position; }
        }
        

        // Update is called once per frame
        void Update()
        {
            var biscuitsToDespawn = CheckBiscuitsToDespawn();
            if (biscuitsToDespawn.Count > 0)
            {
                DespawnBiscuit(biscuitsToDespawn.ToArray());
            }
        }


        private List<Biscuit> CheckBiscuitsToDespawn()
        {
            var biscuitsToDespawn = new List<Biscuit>();
            foreach (var biscuit in GameManager.Instance.GetSpawnedBiscuits())
            {
                if (biscuit != null && Vector3.Distance(biscuit.transform.position, DespawnCenterPoint) < DespawnRadius)
                {
                    biscuitsToDespawn.Add(biscuit);
                }
            }
            return biscuitsToDespawn;
        }

        public static void DespawnBiscuit(params Biscuit[] biscuits)
        {
            foreach (var biscuit in biscuits)
            {
                if (biscuit != null)
                {
                    GameManager.Instance.DestroyBisquit(biscuit);
                }
            }
        }

        // Draw sphere gizmo in editor
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(DespawnCenterPoint, DespawnRadius);
        }
    }
}