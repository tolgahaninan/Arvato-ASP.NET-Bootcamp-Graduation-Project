using GradBootcamp_Tolgahaninan.Data.Redis.IRedis;
using GradBootcamp_Tolgahaninan.Repository.IRepository;

namespace GradBootcamp_Tolgahaninan.BackgroundJobWorker
{
    public class BackgroundJobWorker : BackgroundService
    {
        // Custom background job worker which implements Background service interface
        private readonly ILogger<BackgroundJobWorker> _logger; // To log needed information
        private readonly IServiceProvider _serviceProvider; // Since the background service is in form singeleton to reach scoped services service provider used
        public BackgroundJobWorker(ILogger<BackgroundJobWorker> logger , IServiceProvider serviceProvider ) // Constructor for dependency injection
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
       
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken) // This method of interface works until cancellation token comes
        {
            while (!stoppingToken.IsCancellationRequested) // while cancellation is requested
            {
                using(var scope = _serviceProvider.CreateScope()) // Creating a mock scope to run scoped services in singeleton background service
                {
                    var scopedRedisService = scope.ServiceProvider.GetRequiredService<IRedisHelper>(); // To create redis service 
                    var scopedMovieViewRepositoryService = scope.ServiceProvider.GetRequiredService<IMovieViewRepository>(); // To create Movie View Repository

                     
                    var movieViewList = scopedMovieViewRepositoryService.GetMovieViewList(); // To get MovieView database elements

                    int totalMovieViewHolder = 0 ; // To keep total movie view

                    if(movieViewList != null) // If there is Clicked movies
                    {
                        foreach (var movieView in movieViewList) 
                        {
                            totalMovieViewHolder = totalMovieViewHolder + movieView.ClickCounter; // To add Movie Views' click counters to total movie view
                        }

                    }
                    if(scopedRedisService != null)
                    {
                        await scopedRedisService.SetKeyAsync("TotalMovieView", totalMovieViewHolder.ToString()); // To Set key in redis and hold total movie view
                        var x = await scopedRedisService.GetKeyAsync("MovieViewList"); // To get total movie view
                    }
                   
                  
                    _logger.LogInformation("Background Job Worker : Execute Async {DateTime}", DateTime.Now);
                    
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // To make all operations in every minutes

                }
               
            }
           
            
        }

        public override Task StopAsync(CancellationToken cancellationToken) // To Stop backgrond worker
        {
            _logger.LogInformation("Background Job Worker : Stop Async {DateTime}",DateTime.Now);
            return base.StopAsync(cancellationToken);
        }
    }
}
