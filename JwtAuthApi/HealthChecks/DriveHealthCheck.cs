using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace JwtAuthApi.HealthChecks;

/// <summary>
/// Represents a health check, which can be used to check the status of a disk drive.
/// </summary>
public class DriveHealthCheck : IHealthCheck
{
    private string _driveName;
    private long _driveUnhealthyThresholdBytes;
    private long _driveHealthyThresholdBytes;

    private int _driveHealthyThresholdInGb;
    private int _driveUnhealthyThresholdInGb;


    public DriveHealthCheck(string driveName, int healthyThresholdGb, int unhealthyThresholdGb)
    {
        _driveHealthyThresholdInGb = healthyThresholdGb;
        _driveUnhealthyThresholdInGb = unhealthyThresholdGb;
        _driveName = driveName;
        _driveHealthyThresholdBytes = healthyThresholdGb * 1024 * 1024 * 1024L;
        _driveUnhealthyThresholdBytes = unhealthyThresholdGb * 1024 * 1024 * 1024L;
    }


    /// <inheritdoc/>>
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        DriveInfo driveInfo = new DriveInfo(_driveName);

        if (IsDriveUnhealthy(driveInfo))
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(description: $"Drive {_driveName} space is less than {_driveUnhealthyThresholdInGb} GB"));
        }
        else if (IsDriveDegraded(driveInfo))
        {
            return Task.FromResult(HealthCheckResult.Degraded(description: $"Drive {_driveName} space is between {_driveUnhealthyThresholdInGb} and {_driveHealthyThresholdInGb} GB"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Healthy(description: $"Drive {_driveName} space is more than {_driveHealthyThresholdInGb} GB"));
        }
    }

    private bool IsDriveUnhealthy(DriveInfo driveInfo)
        => driveInfo.TotalFreeSpace <= _driveHealthyThresholdBytes;

    private bool IsDriveDegraded(DriveInfo driveInfo)
        => driveInfo.TotalFreeSpace <= _driveHealthyThresholdBytes && driveInfo.TotalFreeSpace > _driveUnhealthyThresholdBytes;
}
