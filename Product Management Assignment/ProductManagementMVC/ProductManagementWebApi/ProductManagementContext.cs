namespace ProductManagementWebApi
{
    using ProductManagementModels;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ProductManagementContext : DbContext
    {
        // Your context has been configured to use a 'ProductManagementContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ProductManagementWebApi.ProductManagementContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ProductManagementContext' 
        // connection string in the application configuration file.
        public ProductManagementContext()
            : base("name=ProductManagementContext")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }

        public virtual DbSet<User> UserEntities { get; set; }
        public virtual DbSet<Product> ProductEntities { get; set; }
        public virtual DbSet<Login> LoginEntities { get; set; }

        public virtual DbSet<Category> CategoryEntities { get; set; }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}