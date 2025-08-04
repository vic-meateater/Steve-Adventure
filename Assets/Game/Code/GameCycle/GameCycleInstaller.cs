using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public class GameCycleInstaller : MonoInstaller<GameCycleInstaller>
    {
        [SerializeField] private GameState _gameState = GameState.None;
        private GameCycle _gameCycle;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameCycle>()
                .FromMethod(CreateGameCycle)
                .AsSingle()
                .NonLazy();
        }
        
        private GameCycle CreateGameCycle(InjectContext context)
        {
            _gameCycle = new GameCycle();
            foreach (var listener in context.Container.ResolveAll<IGameListener>())
            {
                _gameCycle.AddListener(listener);
            }

            return _gameCycle;
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