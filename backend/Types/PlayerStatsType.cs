using HotChocolate.Types;

namespace backend.Types
{

    public class PlayerStatsType : ObjectType<PlayerStats>
    {
        protected override void Configure(IObjectTypeDescriptor<PlayerStats> descriptor)
        {
            descriptor.Name("PlayerStats");
        }
    }

    public class PlayerStats
    {
        public int GameCount { get; set; }
        public int GameWinCount { get; set; }
        public int GameLooseCount { get; set; }
    }
}