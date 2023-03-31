using Microsoft.Extensions.Diagnostics.HealthChecks;
using static TowerOfDaedalus_WebApp_DiscordBot.DiscordBot;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    public class DockerHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            AppState appState = DiscordBot.getAppState();

            switch (appState)
            {
                case AppState.SHUTDOWN:
                    return Task.FromResult(
                        new HealthCheckResult(
                            context.Registration.FailureStatus, "The bot is shut down"));
                case AppState.STARTING:
                    return Task.FromResult(
                        new HealthCheckResult(
                            context.Registration.FailureStatus, "The bot is starting up"));
                case AppState.HEALTHY:
                    return Task.FromResult(
                        HealthCheckResult.Healthy("The bot is running"));
                case AppState.UNHEALTHY:
                    return Task.FromResult(
                        new HealthCheckResult(
                            context.Registration.FailureStatus, "The bot is in an unhealthy state"));
                case AppState.STOPPING:
                    return Task.FromResult(
                        new HealthCheckResult(
                            context.Registration.FailureStatus, "The bot is shutting down"));
                default:
                    return Task.FromResult(
                        new HealthCheckResult(
                            context.Registration.FailureStatus, "The bot is in an unknown state"));
            }
        }
    }
}
