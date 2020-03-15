using System;
using backend.Resolvers;
using HotChocolate.Types;

namespace backend.Types
{
    public class MutationType : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {

            descriptor.Name("Mutation");

            descriptor.Field<PlayerResolver>(r => r.CreatePlayerAsync(null, null))
                .Name("createPlayer")
                .Type<NonNullType<PlayerType>>()
                .Argument("input", a => a.Type<NonNullType<PlayerInputType>>());

            descriptor.Field<PlayerResolver>(r => r.DeletePlayerAsync(null, null))
                .Name("deletePlayer")
                .Type<NonNullType<IntType>>()
                .Argument("input", a => a.Type<NonNullType<PlayerDeleteType>>());

            descriptor.Field<MatchResolver>(r => r.DeleteMatchAsync(null, null))
                .Name("deleteMatch")
                .Type<NonNullType<IntType>>()
                .Argument("input", a => a.Type<NonNullType<MatchDeleteType>>());

            descriptor.Field<MatchResolver>(r => r.CreateMatch(null, null))
                .Name("createMatch")
                .Type<NonNullType<MatchType>>()
                .Argument("input", a => a.Type<NonNullType<MatchInputType>>());
        }
    }
}
