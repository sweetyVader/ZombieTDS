using UnityEngine;

namespace TDS.Game.Objects
{
    public class CameraMover : SingletonMonoBehaviour<CameraMover>
    {
        #region Variables

        [SerializeField] private Transform _follow;

        private Transform _cachedTransform;

        #endregion


        #region Unity lifecycle

        protected override void Awake()
        {
            _cachedTransform = transform;
        }

        private void Update()
        {
            Vector3 followPosition = _follow.position;
            followPosition.z = _cachedTransform.position.z;
            _cachedTransform.position = followPosition;
        }

        #endregion
    }
}