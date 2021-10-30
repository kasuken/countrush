using FaunaDB.Client;
using FaunaDB.Types;
using FaunaDB.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static FaunaDB.Query.Language;

namespace CountRush.Services
{
    public class FaunaCountRushRepository : ICountRushRepository
    {
        static readonly string ENDPOINT = Environment.GetEnvironmentVariable("FaunaRegion");
        static readonly string SECRET = Environment.GetEnvironmentVariable("FaunaKey");

        public async Task<int> RetrieveVisitors(string repositoryname)
        {
            var client = new FaunaClient(endpoint: ENDPOINT, secret: SECRET);

            try
            {
                var result = await client.Query(
                        Get(
                            Match(
                                Index("repositoryname"),
                                repositoryname
                            )
                        )
                    );

            }
            catch (Exception ex)
            {
                await client.Query(
                    Create(
                        Collection("visitors"),
                        Obj("data", 1)
                    )
                );
            }

            return 1;
        }
    }
}
