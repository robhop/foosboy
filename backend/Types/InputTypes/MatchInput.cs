using System.Collections.Generic;
using System.Linq;
using HotChocolate.Types.Relay;

namespace backend.Models
{
    public class MatchInput
    {
        public List<string> winners { get; set; }
        public List<string> loosers { get; set; }
        public IEnumerable<int> Winners()
        {
            IdSerializer id = new IdSerializer();
            return winners.Select(w => (int)id.Deserialize(w).Value);
        }
        public IEnumerable<int> Loosers()
        {
            IdSerializer id = new IdSerializer();
            return loosers.Select(w => (int)id.Deserialize(w).Value);
        }
    }
}