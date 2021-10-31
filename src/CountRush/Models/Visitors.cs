using FaunaDB.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaunaDB.Client;
using FaunaDB.Types;
using FaunaDB.Query;

namespace CountRush.Models
{
    public class Visitors
    {
        [FaunaField("repositoryname")]
        public string RepositoryName { get; set; }

        [FaunaField("count")]
        public int Count { get; set; }

        [FaunaConstructor]
        public Visitors(string repositoryName, int count)
        {
            RepositoryName = repositoryName;
            Count = count;
        }
    }
}
