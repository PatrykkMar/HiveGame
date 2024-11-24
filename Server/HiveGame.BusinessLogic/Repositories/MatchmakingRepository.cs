using HiveGame.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// niepotrzebne usings można usunąć

namespace HiveGame.BusinessLogic.Repositories
{
    // Można oddzielić interfejs do oddzielnego folderu, np. Contract/IMatchmakingRepository.cs
    public interface IMatchmakingRepository
    {
        long Count { get; }

        void Add(Player item);

        IEnumerable<Player> GetAll();

        Player? GetByPlayerId(string playerId);

        bool Update(string playerId, Player updatedItem);

        List<Player> GetAndRemoveFirstTwo();

        bool Remove(string playerId);
    }

    public class MatchmakingRepository : IMatchmakingRepository
    {
        private readonly List<Player> _items;

        public MatchmakingRepository()
        {
            // można użyć nowszej składni new()
            _items = new List<Player>();
        }

        public long Count
        {
            get { return _items.Count; }
        }

        public void Add(Player item)
        {
            _items.Add(item);
        }

        public IEnumerable<Player> GetAll()
        {
            return _items;
        }

        public Player? GetByPlayerId(string playerId)
        {
            return _items.FirstOrDefault(x => x.PlayerId == playerId);
        }

        public bool Update(string playerId, Player updatedItem)
        {
            var index = _items.FindIndex(x => x.PlayerId == playerId);
            if (index == -1)
            {
                return false;
            }

            _items[index] = updatedItem;
            return true;
        }


        public List<Player> GetAndRemoveFirstTwo()
        {
            var firstTwoItems = _items.Take(2).ToList();
            foreach (var item in firstTwoItems)
            {
                // w liście znajdue się metoda _items.RemoveAt(int index), w której podaje się nr indeksu
                _items.Remove(item);
            }
            return firstTwoItems;
        }

        public bool Remove(string playerId)
        {
            var item = _items.FirstOrDefault(x => x.PlayerId == playerId);
            if (item == null)
            {
                return false;
            }

            _items.Remove(item);
            return true;
        }
    }
}