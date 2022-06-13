using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TakeIt.Domain.Models;
using Windows.Storage;

namespace TakeIt.Infra.Data.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<BorrowedItem> BorrowedItems { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"DataSource={Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db")}");
    }
}
