using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Tracing;
using System.ServiceModel.Channels;
using System.Web.Http;

namespace BlueZero.Air
{
    public sealed class Log4NetTraceWriter : ITraceWriter
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Log4NetTraceWriter));

        private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> _loggingLevelMap =
            new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>>
            {
                { TraceLevel.Info, _log.Info },
                { TraceLevel.Debug, _log.Debug },
                { TraceLevel.Error, _log.Error },
                { TraceLevel.Fatal, _log.Fatal },
                { TraceLevel.Warn, _log.Warn }
            });

        private Dictionary<TraceLevel, Action<string>> Logger
        {
            get { return _loggingLevelMap.Value; }
        }

        public bool IsEnabled(string category, TraceLevel level)
        {
            return true;
        }

        public void Trace(HttpRequestMessage request, string category, TraceLevel level, Action<TraceRecord> traceAction)
        {
            if (level == TraceLevel.Off)
                return;

            var record = new TraceRecord(request, category, level);
            traceAction(record);
            Log(record);
        }        

        private void Log(TraceRecord record)
        {
            var message = new StringBuilder();

            if (record.Request != null)
            {
                if (record.Request.Method != null)
                    message.Append(" ").Append(record.Request.Method.Method);

                if (record.Request.RequestUri != null)
                    message.Append(" ").Append(record.Request.RequestUri.AbsoluteUri);

                if (record.Request.GetClientIpAddress() != null)
                    message.Append(" ").Append(record.Request.GetClientIpAddress());
            }

            if (!String.IsNullOrWhiteSpace(record.Category))
                message.Append(" ").Append(record.Category);

            if (!String.IsNullOrWhiteSpace(record.Operator))
                message.Append(" ").Append(record.Operator).Append(" ").Append(record.Operation);

            if (!String.IsNullOrWhiteSpace(record.Message))
                message.Append(" ").Append(record.Message);

            if (record.Exception != null && !String.IsNullOrEmpty(record.Exception.GetBaseException().Message))
                message.Append(" ").AppendLine(record.Exception.GetBaseException().Message);

            Logger[record.Level](message.ToString());
        }        
    }
}