using System;
using UnityEngine;

namespace SteveAdventure
{
    [RequireComponent(typeof(Mover), typeof(WaypointsMoveController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyStats _stats = new EnemyStats();
        
        private void OnValidate()
        {
            _stats.Validate();
        }
    }


    [Serializable]
    public struct EnemyStats
    {
        //спорно
        [Tooltip("Скорость передвижения врага")]
        [Range(0.1f, 10f)]
        public float Speed;

        public void Validate()
        {
            Speed = Mathf.Max(Speed, 0.1f);
        }
    }
}