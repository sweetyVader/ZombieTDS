using System;
using UnityEngine;

namespace TDS.Game.Objects
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Transform _follow;

        private Transform _cachedTransform;

        private void Awake()
        {
            _cachedTransform = transform;
        }

        private void Update()
        { 
            Vector3 followPosition = _follow.position;
            followPosition.z = _cachedTransform.position.z;
            _cachedTransform.position = followPosition;
        }
    }
}