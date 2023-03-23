using log4net.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace XCore.PMS.WebAPI.Service
{
    public class DemoBgService : BackgroundService
    {
        private readonly ILogger<DemoBgService> _logger;

        public DemoBgService(ILogger<DemoBgService> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 执行服务.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                this._logger.LogInformation("开始执行DemoBgService服务");
                await Task.Delay(5000);
                this._logger.LogInformation("结束执行DemoBgService服务");
            }
            catch(Exception ex)
            {
                this._logger.LogError(string.Format("DemoBgService：ExecuteAsync出错：{0}", ex.Message), ex);
            }
        }

        /// <summary>
        /// 服务关闭后，进行资源释放.
        /// </summary>
        public override void Dispose()
        {
            this._logger.LogInformation("DemoBgService服务关闭，释放资源");
            base.Dispose();
        }
    }
}
