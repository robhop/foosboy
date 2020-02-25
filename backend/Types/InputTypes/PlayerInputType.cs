using HotChocolate.Types;

namespace backend.Types
{

    public class PlayerInputType : InputObjectType<PlayerInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<PlayerInput> descriptor)
        {
            descriptor
                .Field(t => t.Name)
                .Type<NonNullType<StringType>>();
        }
    }

    public class PlayerInput
    {
        public string Name { get; set; }
    }
}