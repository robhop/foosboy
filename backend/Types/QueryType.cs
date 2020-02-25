using backend.Resolvers;
using HotChocolate.Types;

namespace backend.Types
{

    public class QueryType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("Query");

            descriptor
                .Name("players")
                .Field<PlayerResolver>(t => t.GetPlayers(default, default))
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>();
            
            descriptor
                .Name("teams")
                .Field<TeamResolver>(t => t.GetTeams(default, default))
                .Type<NonNullType<ListType<NonNullType<TeamType>>>>();

            descriptor
                .Name("matches")
                .Field<MatchResolver>(t => t.GetMatches(default, default))
                .Type<NonNullType<ListType<NonNullType<MatchType>>>>();
        
        }
    }
}