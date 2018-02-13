using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.Extensions.Configuration;
using TestApi.Models;

namespace TestApi.DataAccess
{
    public class TestDbContext
    {
        private readonly IConfigurationRoot _config;

        public string DatabaseName { get; set; } = "Test";
        public bool IsSSL { get; set; } = false;

        private IMongoDatabase _database { get; }

        private readonly ILogger _logger;

        public TestDbContext(IConfigurationRoot config,
                                      ILogger<TestDbContext> logger)
        {
            _config = config;
            _logger = logger;


            var mongoDBConnectionString = _config["ConnectionStrings:mongoDb"];
            MongoClient mongoClient = null;
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(mongoDBConnectionString));
                if (IsSSL)
                {
                    settings.SslSettings = new SslSettings { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
                }
                mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                LogException(ex);
                throw new Exception("Can not access to mongo db server.", ex);
            }
        }

        public IMongoCollection<Product> Products
        {
            get
            {
                return _database.GetCollection<Product>("Products");
            }
        }
        

        /// </// <summary>
        /// HELPER return and log exception
        /// </summary>
        public void LogException(Exception exception)
        {
            Guid errNo = Guid.NewGuid();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(errNo.ToString());
            sb.AppendLine(exception.Message);
            if (exception.InnerException != null)
                sb.AppendLine(exception.InnerException.Message);
            sb.AppendLine(exception.StackTrace);

            _logger.LogCritical(sb.ToString());
        }
    }
}