using System;
using System.Collections.Generic;
using UnityEngine;

namespace SteveAdventure
{
    public enum GameState
    {
        None,
        Starting,
        Running,
        Paused,
        Finished
    }

    public class GameCycle
    {
        private List<IGameListener> _gameListeners = new();
        private List<IGameUpdateListener> _gameUpdateListeners = new();
        private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

        private GameState _gameState;

        public GameCycle(GameState gameState)
        {
            _gameState = gameState;
        }

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
            if (_gameState is not (GameState.Starting or GameState.Running)) return;

            foreach (IGameUpdateListener gameUpdateListener in _gameUpdateListeners)
            {
                gameUpdateListener.OnGameUpdate(deltaTime);
            }
        }

        public void OnGameFixedUpdate(float fixedDeltaTime)
        {
            if (_gameState is not (GameState.Starting or GameState.Running)) return;

            foreach (IGameFixedUpdateListener gameFixedUpdateListener in _gameFixedUpdateListeners)
            {
                gameFixedUpdateListener.OnGameFixedUpdate(fixedDeltaTime);
            }
        }
    }
}