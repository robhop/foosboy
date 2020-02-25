using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace backend.Types
{
    public class PlayerDeleteType : InputObjectType<PlayerDelete>
    {
        protected override void Configure(IInputObjectTypeDescriptor<PlayerDelete> descriptor)
        {
            descriptor.Field(t => t.Id)
                .Type<NonNullType<IdType>>();
        }
    }

    public class PlayerDelete
    {
        public string Id {get;set;} 
        public int GetId() {
           var value = (new IdSerializer()).Deserialize(Id).Value;
           return (int) value;
        }
    }
}
