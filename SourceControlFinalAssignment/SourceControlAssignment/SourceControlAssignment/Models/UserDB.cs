namespace SourceControlAssignment.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class UserDB : DbContext
    {
        // Your context has been configured to use a 'UserDB' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'SourceControlAssignment.Models.UserDB' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'UserDB' 
        // connection string in the application configuration file.
        public UserDB()
            : base("name=UserDB")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<UserImage> UserImages { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}