using HotChocolate.Types;
using HotChocolate.Types.Relay;

namespace backend.Types
{

    public class MatchInputType : InputObjectType<MatchInput>
    {
        protected override void Configure(IInputObjectTypeDescriptor<MatchInput> descriptor)
        {
        }
    }

    public class MatchInput
    {
        public string winnerA { get; set; }
        public string winnerB { get; set; }
        public string looserA { get; set; }
        public string looserB { get; set; }
        public int WinnerA() {
           var value = (new IdSerializer()).Deserialize(winnerA).Value;
           return (int) value;
        }
        public int WinnerB() {
           var value = (new IdSerializer()).Deserialize(winnerB).Value;
           return (int) value;
        }
        public int LooserA() {
           var value = (new IdSerializer()).Deserialize(looserA).Value;
           return (int) value;
        }
        public int LooserB() {
           var value = (new IdSerializer()).Deserialize(looserB).Value;
           return (int) value;
        }
    }
}