using System.Collections.Concurrent;

namespace BlazorChat.Services
{
    public class UserService
    {
        private readonly ConcurrentDictionary<string, string> _users = new ConcurrentDictionary<string, string>();

        public void Add(string connectionId, string username)
        {
            _users.TryAdd(connectionId, username);
        }

        public void RemoveByName(string username)
        {
            var item = _users.FirstOrDefault(x => x.Value == username);
            if (!item.Equals(default(KeyValuePair<string, string>)))
            {
                _users.TryRemove(item.Key, out _);
            }
        }

        public string GetConnectionIdByName(string username)
        {
            var item = _users.FirstOrDefault(x => x.Value == username);
            return item.Key;
        }

        public IEnumerable<(string ConnectionId, string Username)> GetAll()
        {
            return _users.Select(x => (x.Key, x.Value));
        }
    }
}