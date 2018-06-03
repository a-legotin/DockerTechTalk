using System;
using System.Drawing;
using System.Net;
using System.Threading;

namespace HTTPListenerSimple
{
  public class PerfMeter
  {
    private readonly IOutputWriter _outputWriter = ConsoleOutputWriter.GetOutputWriter();
    private readonly IRepository<HttpListenerRequest> _requestRepo;
    private readonly int _statisticInterval;
    private readonly CancellationTokenSource _token;
    private int _lastKnownRequestsCount;

    public PerfMeter(int statisticInterval, IRepository<HttpListenerRequest> requestRepo,
      CancellationTokenSource cancellationToken)
    {
      if (statisticInterval < 1) throw new ArgumentException("statisticInterval less than 1");
      _statisticInterval = statisticInterval;
      _token = cancellationToken;
      _requestRepo = requestRepo;
      if (cancellationToken != null) Run();
    }

    private void Run()
    {
      while (!_token.IsCancellationRequested)
      {
        var requestsCount = _requestRepo.CountItems();
        _outputWriter.Write(
          $"avg requests {(requestsCount - _lastKnownRequestsCount) * 1000 / _statisticInterval} per second",
          Color.Blue);
        _lastKnownRequestsCount = requestsCount;
        Thread.Sleep(_statisticInterval);
      }
    }
  }
}