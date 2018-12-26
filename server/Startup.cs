using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TRNServer
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // var connection = @"Server=(localdb)\mssqllocaldb;Database=EFGetStarted.AspNetCore.NewDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<TRNContext>(options => options.UseInMemoryDatabase(Configuration.GetConnectionString("InMemory")));
            _logger.LogInformation("DBContex created");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            // ONLY FOR TEST: FILL DATABASE
            using(var scope = app.ApplicationServices.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<TRNContext>();

                // Ensure the database is created.
                db.Database.EnsureCreated();

                try
                {
                    // Seed the database with test data.
                    AddTestData(db);
                    _logger.LogInformation("Seeded the database.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred seeding the "
                        + "database with test messages. Error: {ex.Message}");
                }
            }
        }

        private static void AddTestData(TRNContext context)
        {
            var testUser1 = new Blog
            {
                Id = 1,
                Url = "test"
            };

            context.Blogs.Add(testUser1);

            var testPost1 = new Post
            {
                Id = 1,
                BlogId = testUser1.Id,
                Title = "This is post",
                Content = "What a piece of junk!"
            };
            var testPost2 = new Post
            {
                Id = 2,
                BlogId = testUser1.Id,
                Title = "This is post #2",
                Content = "wo wo wo!"
            };

            context.Posts.AddRange(new [] { testPost1, testPost2 });

            context.SaveChanges();
        }
    }
}