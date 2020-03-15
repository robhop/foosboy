using System.Collections.Generic;
using System.Linq;
using backend.Models;
using backend.Repositories;
using backend.Resolvers;
using HotChocolate;
using HotChocolate.Types;

namespace backend.Types
{
    public class MatchType : ObjectType<Match>
    {
        protected override void Configure(IObjectTypeDescriptor<Match> descriptor)
        {
            descriptor.BindFieldsExplicitly();

            descriptor.Name("Match");

            descriptor.AsNode()
                .IdField(t => t.Id)
                .NodeResolver((ctx, id) => ctx.Service<MatchRepository>().GetMatchByIdAsync(id));

            descriptor.Field(m => m.Timestamp);

            descriptor.Field<MatchResolver>(r => r.GetType(null));

            descriptor.Field<MatchResolver>(r => r.GetPlayers(null, null))
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>();

            descriptor.Field<MatchResolver>(r => r.GetWinners(null, null))
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>();

            descriptor.Field<MatchResolver>(r => r.GetLoosers(null, null))
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>();


        }
    }
}