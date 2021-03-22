
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

#if NET35 || NET40
using System.Data.Entity;
#elif NETCORE
using Microsoft.EntityFrameworkCore;
#endif

namespace System.Linq.Dynamic.Tests.Helpers.Entities
{
    public class BlogContext : DbContext
    {
#if NET35 || NET40
        public BlogContext(DbConnection connection)
            : base(connection, true)
        {

        }
#elif NETCORE
        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {

        }
#endif

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
