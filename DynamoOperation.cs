using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace _301109137_kandru__LAB03
{


    public class DynamoOperation
        {

            public DynamoDBContext context { get; }
            public BasicAWSCredentials credentials { get; }

            public static AmazonDynamoDBClient client = null;

            public DynamoOperation()
            {
                Directory.SetCurrentDirectory(AppContext.BaseDirectory);
                var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("AppSettings.Json", optional: true, reloadOnChange: true);

                var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
                var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

                var credentials = new BasicAWSCredentials(accessKeyID, secretKey);
                client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
                context = new DynamoDBContext(client);
            }

            //public async Task<Movie> AddMovie(Movie movie)
            //{

            //Table movieTable = Table.LoadTable(client, "movies");
            //ScanFilter scanFilter = new ScanFilter();
            //scanFilter.AddCondition("Id", ScanOperator.BeginsWith, "MOVIE_");
            //scanFilter.AddCondition("SortId", ScanOperator.BeginsWith, "MOVIE_");
            //Search search = movieTable.Scan(scanFilter);
            //int numOfMovies = search.Count;

            //movie.MovieId = "MOVIE_" + (numOfMovies + 1).ToString();
            //movie.SortId = movie.MovieId;
            //await context.SaveAsync(movie, default(System.Threading.CancellationToken));
            //return movie;
            //}

            //public Task<List<Movie>> GetMovies(int title)
            //{
            //    List<ScanCondition> ScanConditions = new List<ScanCondition>() { new ScanCondition("MovieId", ScanOperator.Equal, title) };
            //    var moviesRetrieved = context.ScanAsync<Movie>(ScanConditions);


            //    return moviesRetrieved.GetNextSetAsync();
            //}

            //public async Task UpdateMovieAsync(Movie movie)
            //{
            //    await context.(movie);
            //}

            //public void DeleteMovie(String userId, String movieTitle)
            //{
            //    context.DeleteAsync<Movie>(userId, movieTitle);
            //}

        
    }
}
