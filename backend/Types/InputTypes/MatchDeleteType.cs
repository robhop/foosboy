using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace backend.Types
{
    public class MatchDeleteType : InputObjectType<MatchDelete>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MatchDelete> descriptor)
        {
            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();
        }
    }

    public class MatchDelete
    {
        public string Id {get;set;} 
        public int GetId() {
           var value = (new IdSerializer()).Deserialize(Id).Value;
           return (int) value;
        }
    }
}
