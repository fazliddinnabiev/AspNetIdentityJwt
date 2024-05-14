using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JwtAuthApi.extensions;

/// <summary>
/// Represents Custom health check for "C:" disk drive.
/// </summary>
public class HealthCheckExtensions : IHealthCheck
{
    private const long DriveUnhealthyThresholdBytes = 200 * 1024 * 1024 * 1024L;
    private const long DriveHealthyThresholdBytes = 100 * 1024 * 1024 * 1024L;


    /// <summary>
    /// Checks  health of "c:" drive.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        DriveInfo driveInfo = new DriveInfo("C:");

        if (IsDriveUnhealthy(driveInfo))
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(description: "Drive C space is less than 100 GB"));
        }
        else if (IsDriveDegraded(driveInfo))
        {
            return Task.FromResult(HealthCheckResult.Degraded(description: "Drive C space is between 100GB and 200 GB"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Healthy(description: "Drive C space is more than 200 GB"));
        }
    }

    private static bool IsDriveUnhealthy(DriveInfo driveInfo)
        => driveInfo.TotalFreeSpace <= DriveUnhealthyThresholdBytes;

    private static bool IsDriveDegraded(DriveInfo driveInfo)
        => driveInfo.TotalFreeSpace <= DriveHealthyThresholdBytes && driveInfo.TotalFreeSpace > DriveUnhealthyThresholdBytes;
}
