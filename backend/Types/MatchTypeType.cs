using HotChocolate.Types;

public class MatchTypeType : EnumType<MatchEnum>
{
}

public enum MatchEnum
{
    SINGLE,
    DOUBLE
}