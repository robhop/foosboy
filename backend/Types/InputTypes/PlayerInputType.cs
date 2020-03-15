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

            descriptor
                .Field(t => t.Avatar)
                .Type<StringType>();
        }
    }

    public class PlayerInput
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}