using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace Reindeer.Web.Tests.Fixtures;

public class ReindeerApiFixture : IDisposable
{
    public readonly Uri ServerUri = new("http://localhost:6192");

    private Process? _process;
    public ReindeerApiFixture()
    {
        var dll = typeof(Program).Assembly;
        ProcessStartInfo psi = new ProcessStartInfo{
            FileName = "dotnet",
            Arguments = $"exec {dll.Location} --urls {ServerUri}",
            Environment = { ["ASPNETCORE_ENVIRONMENT"] = "Development" },
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        _process = Process.Start(psi);
        // Some time for server to start
        Thread.Sleep(TimeSpan.FromSeconds(5));
    }
    public void Dispose()
    {
        _process?.Kill();
        _process?.Dispose();
    }
}

