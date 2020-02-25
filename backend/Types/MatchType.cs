using backend.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace backend.Types
{
    public class MatchType : ObjectType<Match>
    {
        protected override void Configure(IObjectTypeDescriptor<Match> descriptor)
        {
            descriptor.AsNode()
                .IdField(t => t.Id)
                .NodeResolver((ctx, id) => ctx.Service<MatchRepository>().GetMatchByIdAsync(id));
        }
    }
}