using Zenject;

namespace SteveAdventure
{
    public sealed class EnemiesFactory : IFactory<EnemyConfig, Enemy>
    {
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public Enemy Create(EnemyConfig config)
        {
            EnemyPool pool = _container.ResolveId<EnemyPool>(config);
            return pool.Spawn(config, pool);
        }
    }
}