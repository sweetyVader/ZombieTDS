using System;
using TDS.Game.Player;
using TMPro;
using UnityEngine;

namespace TDS.Game.UI
{
    public class BulletBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentBullet;
        private const string BulletPath = "BulletIcon";
        private PlayerAttack _playerAttack;

        private int _numBullets;
        private int _maxBullets = 50;

        private float _bias = 0.5f;

        public event Action<bool> EmptyBullet;

        private void Start()
        {
            _playerAttack = FindObjectOfType<PlayerAttack>();
            _numBullets = _playerAttack.NumBullet;


            _playerAttack.OnShoot += BulletChanged;
            //  BulletChanged(_numBullets);
            // BulletChanged(_numBullets);
            //
            // GameManager.Instance.OnLifeChanged += BulletChanged;
        }

        private void Update()
        {
            _currentBullet.text = _numBullets.ToString();
        }

        private void BulletChanged()
        {
            _numBullets--;
            if (_numBullets <= 0)
            {
                EmptyBullet?.Invoke(true);
            }
        }

        private void OnDestroy()
        {
            _playerAttack.OnShoot -= BulletChanged;
        }

        public void AddBullet(int num)
        {
            _numBullets += num;
            if (_numBullets >= _maxBullets)
                _numBullets = _maxBullets;
        }
        // private void BulletChanged(int life)
        // {
        //     // foreach (GameObject bulletUI in GameObject.FindGameObjectsWithTag(Tags.BulletIcon))
        //     //     Destroy(bulletUI);
        //     GameObject prefab = Resources.Load<GameObject>(BulletPath);
        //     Vector2 startPosition = new Vector2(-11.5f, 6f);
        //     for (int i = 0; i < life; i++)
        //     {
        //         Vector2 bulletPosition = startPosition;
        //         bulletPosition.x += _bias * i;
        //         LeanPool.Spawn(prefab, bulletPosition, prefab.transform.rotation);
        //     }
        // }
    }
}