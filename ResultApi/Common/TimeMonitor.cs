using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Diagnostics;

namespace ResultApi.Common
{
    public class TimeMonitor : IDisposable
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();        
        private HttpContext _context;

        public TimeMonitor(HttpContext context)
        {            
            _context = context;

            _stopwatch.Start();
        }

        #region IDisposable members
        private bool _disposed = false;

        ~TimeMonitor()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {

            if (!_disposed)
            {
                if (disposing)
                {
                    if (_stopwatch.IsRunning)
                    {
                        _stopwatch.Stop();
                        
                        Log.Information($"{_context.Request.Path} elapsed: {_stopwatch.ElapsedMilliseconds}ms, from: {_context.Connection.RemoteIpAddress}");
                    }
                }

                this._disposed = true;
            }
        }

        #endregion
    }
}
