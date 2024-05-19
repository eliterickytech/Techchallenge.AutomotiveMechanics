namespace TechChallenge.AutomotiveMechanics.Presentation.Worker
{
    public class NotifyOrderConsumer : BackgroundService
    {
        private readonly ILogger<NotifyOrderConsumer> _logger;
        private readonly IServiceProvider _serviceProvider;

        public NotifyOrderConsumer(ILogger<NotifyOrderConsumer> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
