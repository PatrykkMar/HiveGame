﻿using HiveGame.BusinessLogic.Repositories;
using HiveGame.DataAccess.Repositories;
using System.Collections.Concurrent;

namespace HiveGame.Managers
{

    public interface IConnectionManager
    {
        public void AddPlayerConnection(string playerId, string connectionId);
        public void RemovePlayerConnection(string connectionId);
        public void UpdatePlayerConnection(string playerId, string connectionId);
        public string? GetConnectionId(string playerId);
    }

    public class ConnectionManager : IConnectionManager
    {
        private static readonly ConcurrentDictionary<string, string> PlayerConnectionDict = new();

        public ConnectionManager(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        private IGameRepository _gameRepository { get; set; }
        public void AddPlayerConnection(string playerId, string connectionId)
        {
            PlayerConnectionDict.TryAdd(playerId, connectionId);
        }

        public void RemovePlayerConnection(string connectionId)
        {
            var keyToRemove = PlayerConnectionDict.Where(kvp => kvp.Value.Equals(connectionId)).Select(kvp => kvp.Key).FirstOrDefault();


            //quick solution when player exits the game
            var game = _gameRepository.GetByPlayerId(keyToRemove);
            if(game != null) 
                _gameRepository.Remove(game.Id);

            if(keyToRemove != null)
            {
                PlayerConnectionDict.TryRemove(keyToRemove, out _);
            }
        }

        public string? GetConnectionId(string playerId) => PlayerConnectionDict.TryGetValue(playerId, out var connectionId) ? connectionId : null;

        public void UpdatePlayerConnection(string playerId, string connectionId)
        {
            PlayerConnectionDict[playerId] = connectionId;
        }
    }
}