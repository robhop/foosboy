
using backend.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace backend.Types
{

    public class PlayerType : ObjectType<Player>
    {
        protected override void Configure(IObjectTypeDescriptor<Player> descriptor)
        {
            descriptor.AsNode()
                .IdField(t => t.Id)
                .NodeResolver((ctx, id) => ctx.Service<PlayerRepository>().GetPlayerByIdAsync(id));

            descriptor.Field(t => t.Name)
                .Type<NonNullType<StringType>>();

            descriptor.Field(t => t.Avatar)
                .Type<NonNullType<StringType>>();

        }
    }
}