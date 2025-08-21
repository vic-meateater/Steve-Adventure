using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace SteveAdventure
{
    public enum GameState
    {
        None,
        //Starting,
        Running,
        Paused,
        Finished,
        GameOver
    }

    public sealed class GameCycle : IInitializable, IDisposable
    {
        private List<IGameListener> _gameListeners = new();
        private List<IGameUpdateListener> _gameUpdateListeners = new();
        private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        private GameState _gameState;

        public void StartGame()
        {
            if (_gameState is not (GameState.None or GameState.Finished))
                return;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameStartListener gameStartListener)
                {
                    gameStartListener.OnGameStart();
                }
            }

            _gameState = GameState.Running;
            Debug.Log("GameCycle Started : "  + _gameState);
        }

        public void PauseGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGamePauseListener gamePauseListener)
                {
                    gamePauseListener.OnGamePause();
                }
            }

            _gameState = GameState.Paused;
        }

        public void ResumeGame()
        {
            if (_gameState is not GameState.Paused)
                return;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameResumeListener gameResumeListener)
                {
                    gameResumeListener.OnGameResume();
                }
            }

            _gameState = GameState.Running;
        }
        
        public void GameOver()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGameOverListener gameOverListener)
                {
                    gameOverListener.OnGameOver();
                }
            }

            _gameState = GameState.GameOver;
        }

        public void FinishGame()
        {
            foreach (var listener in _gameListeners)
            {
                if (listener is IGameFinishListener gameEndListener)
                {
                    gameEndListener.OnGameFinish();
                }
            }

            _gameState = GameState.Finished;
        }

        public void AddListener(IGameListener listener)
        {
            if (listener == null || _gameListeners.Contains(listener))
                return;

            _gameListeners.Add(listener);

            if (listener is IGameUpdateListener gameUpdateListener)
                _gameUpdateListeners.Add(gameUpdateListener);
            
            if (listener is IGameFixedUpdateListener gameFixedUpdateListener)
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);
        }
        
        
        public void RemoveListener(IGameListener listener)
        {
            if (listener == null || !_gameListeners.Contains(listener))
                return;

            _gameListeners.Remove(listener);

            if (listener is IGameUpdateListener gameUpdateListener)
                _gameUpdateListeners.Remove(gameUpdateListener);
            
            if (listener is IGameFixedUpdateListener gameFixedUpdateListener)
                _gameFixedUpdateListeners.Remove(gameFixedUpdateListener);
        }

        public void OnGameUpdate(float deltaTime)
        {
            if (_gameState is not GameState.Running) return;

            foreach (IGameUpdateListener gameUpdateListener in _gameUpdateListeners)
            {
                gameUpdateListener.OnGameUpdate(deltaTime);
            }
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_gameState is not (GameState.Running)) return;

            foreach (IGameFixedUpdateListener gameFixedUpdateListener in _gameFixedUpdateListeners)
            {
                gameFixedUpdateListener.OnGameFixedUpdate(fixedDeltaTime);
            }
        }

        public void Initialize()
        {
            Debug.Log("GameCycle Initialized");
            _gameState = GameState.Running;
            StartGame();
        }

        public void Dispose()
        {
            _gameState = GameState.Finished;
            FinishGame();
        }
    }
}