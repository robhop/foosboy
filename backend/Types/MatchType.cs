using System.Collections.Generic;
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

            descriptor.Field("winners")
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>()
                .Resolver(ctx =>
                {
                    var a = ctx.Parent<Match>().Winner.PlayerA;
                    var b = ctx.Parent<Match>().Winner.PlayerB;
                    if (a == b || b == null)
                        return new List<Player> { a };
                    else
                        return new List<Player> { a, b };
                });

            descriptor.Field("loosers")
                .Type<NonNullType<ListType<NonNullType<PlayerType>>>>()
                .Resolver(ctx =>
                {
                    var a = ctx.Parent<Match>().Looser.PlayerA;
                    var b = ctx.Parent<Match>().Looser.PlayerB;
                    if (a == b || b == null)
                        return new List<Player> { a };
                    else
                        return new List<Player> { a, b };
                });





        }
    }
}