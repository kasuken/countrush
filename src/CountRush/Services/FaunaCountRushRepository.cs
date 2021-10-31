using FaunaDB.Client;
using FaunaDB.Types;
using FaunaDB.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FaunaDB.Query.Language;
using CountRush.Models;
using Microsoft.Extensions.Configuration;

namespace CountRush.Services
{
    public class FaunaCountRushRepository : ICountRushRepository
    {
        private readonly IConfiguration Configuration;

        public FaunaCountRushRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public async Task<int> RetrieveVisitors(string repositoryname)
        {
            var client = new FaunaClient(endpoint: Configuration["FaunaRegion"], secret: Configuration["FaunaKey"]);

            try
            {
                var result = await client.Query(
                        Get(
                            Match(
                                Index("repository"),
                                repositoryname
                            )
                        )
                    );

                var _ref = result.At("ref").To<RefV>();
                var id = _ref.Value.Id;

                var v = result.At("data").To<Visitors>();
                var visitors = new Visitors(repositoryname, v.Value.Count);

                visitors.Count += 1;

                var update = await client.Query(
                      Update(
                            Ref(Collection("visitors"), id),
                            Obj("data", FaunaDB.Types.Encoder.Encode(visitors))
                        )
                );

            }
            catch (Exception ex)
            {
                var visitors = new Visitors(repositoryname, 1);

                await client.Query(
                    Create(
                        Collection("visitors"),
                        Obj("data", FaunaDB.Types.Encoder.Encode(visitors))
                    )
                );
            }

            return 1;
        }
    }
}
