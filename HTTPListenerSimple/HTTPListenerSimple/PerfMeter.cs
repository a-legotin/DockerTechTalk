using System;
using System.Drawing;
using System.Net;
using System.Threading;

namespace HTTPListenerSimple
{
    public class PerfMeter
    {
        private readonly CancellationTokenSource _token;
        private readonly IOutputWriter _outputWriter = ConsoleOutputWriter.GetOutputWriter();
        private readonly IRepository<HttpListenerRequest> _requestRepo = null;
        private readonly int _statisticInterval;
        private int _lastKnownRequestsCount = 0;
        public PerfMeter(int statisticInterval, IRepository<HttpListenerRequest> requestRepo, CancellationTokenSource cancellationToken)
        {
            if (statisticInterval < 1) throw new ArgumentException("statisticInterval less than 1");
            _statisticInterval = statisticInterval;
            _token = cancellationToken;
            _requestRepo = requestRepo;
            if (cancellationToken != null) Run();
        }

        void Run()
        {
            while (!_token.IsCancellationRequested)
            {
                int requestsCount = _requestRepo.CountItems();
                _outputWriter.Write($"avg requests {(requestsCount - _lastKnownRequestsCount) * 1000 / _statisticInterval  } per second", Color.DarkRed);
                _lastKnownRequestsCount = requestsCount;
                Thread.Sleep(_statisticInterval);
            }
        }
    }
}
