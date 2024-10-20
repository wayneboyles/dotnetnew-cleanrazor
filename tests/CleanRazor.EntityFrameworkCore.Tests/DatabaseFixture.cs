using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CleanRazor.EntityFrameworkCore.Tests
{
    public class DatabaseFixture : IDisposable, IAsyncDisposable
    {
        protected AppDbContext Context { get; private set; }

        public DatabaseFixture()
        {
            var contextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDB");

            Context = new AppDbContext(contextOptions.Options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Context.DisposeAsync();
        }
    }
}
