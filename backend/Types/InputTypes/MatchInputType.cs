using System.Collections.Generic;
using System.Linq;
using backend.Models;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
namespace backend.Types
{
    public class MatchInputType : InputObjectType<MatchInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MatchInput> descriptor)
        {
            descriptor.BindFieldsExplicitly();
            descriptor.Field(t => t.winners).Type<ListType<NonNullType<IdType>>>();
            descriptor.Field(t => t.loosers).Type<ListType<NonNullType<IdType>>>();
        }
    }

}