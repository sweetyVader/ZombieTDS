using System;
using UnityEngine;

namespace TDS.Game.PickUp
{
    [Serializable]
    public class PickUpInfo
    {
        [HideInInspector]
        public string name;
        public PickUpBase PickUpPrefab;
        public int SpawnChance;

        public void UpdateName() =>
            name = PickUpPrefab == null ? string.Empty : PickUpPrefab.name;
    }
}