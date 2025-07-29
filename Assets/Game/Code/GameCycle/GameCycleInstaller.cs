using UnityEngine;
using UnityEngine.Serialization;

namespace SteveAdventure
{
    public class GameCycleInstaller : MonoBehaviour
    {
        [SerializeField] private GameState _gameState = GameState.None;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private MoveController _moveController;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private Player _player;
        
        private GameCycle _gameCycle;
        private void Awake()
        {
            _gameCycle = new GameCycle(_gameState);
            _gameCycle.AddListener(_enemy);
            _gameCycle.AddListener(_moveController);
            _gameCycle.AddListener(_inputHandler);
            _gameCycle.AddListener(_player);
            
            GameCycleService.Register(_gameCycle);
        }
        
        private void Start()
        {
            _gameCycle.StartGame();
        }
        
        private void OnDestroy()
        {
            _gameCycle.FinishGame();
            GameCycleService.Unregister();
        }
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            
            _gameCycle.OnGameUpdate(deltaTime);
        }
        
        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            
            _gameCycle.OnGameFixedUpdate(fixedDeltaTime);
        }
    }
}