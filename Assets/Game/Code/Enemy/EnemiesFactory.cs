using Zenject;

namespace SteveAdventure
{
    public sealed class EnemyFactory : IFactory<EnemyConfig, Enemy>
    {
        private DiContainer _container;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
        
        public Enemy Create(EnemyConfig config)
        {
            EnemyPool pool = _container.ResolveId<EnemyPool>(config.name);
            return pool.Spawn(config, pool);
        }
    }
}