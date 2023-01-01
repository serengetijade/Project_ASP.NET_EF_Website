namespace TheatreCMS3.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.IO;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using TheatreCMS3.Models;
    using TheatreCMS3.Areas.Blog.Models;


    // This file is added when Enable-Migrations is run in the Package Manager Console
    internal sealed class Configuration : DbMigrationsConfiguration<TheatreCMS3.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        
        protected override void Seed(ApplicationDbContext db)
        {
            CommentModerator.Seed(db); //Required Namespace: using TheatreCMS3.Areas.Blog.Models; 
        }
    }
}
