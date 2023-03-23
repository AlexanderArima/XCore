using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using XCore.PMS.WebAPI.Model;
using XCore.PMS.WebAPI.Model_ORM;

namespace XCore.PMS.WebAPI.Service
{
    public class ExportCheckInService : BackgroundService
    {
        private readonly db_hotelContext _context;

        private readonly ILogger<ExportCheckInService> _logger;

        private readonly IServiceScope _serviceScope;

        public ExportCheckInService(IServiceScopeFactory scopeFactory)
        {
            this._serviceScope = scopeFactory.CreateScope();
            var sp = this._serviceScope.ServiceProvider;
            this._context = sp.GetRequiredService<db_hotelContext>();
            this._logger = sp.GetRequiredService<ILogger<ExportCheckInService>>();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(stoppingToken.IsCancellationRequested == false)
            {
                try
                {
                    this.Export();
                    await Task.Delay(5000);
                }
                catch(Exception ex)
                {
                    this._logger.LogError(ex, "获取用户统计数据失败");
                    await Task.Delay(1000);
                }
            }
        }

        private async void Export()
        {
            // 获取在住人员总数
            var count = this._context.TOrders.Count(m => m.Status == "1");
            await File.WriteAllTextAsync(
                string.Format(@"{0}Export\CheckIn\{1}.txt", PathHelper.ApplicationPath, DateTime.Now.ToString("yyyyMMddHHmmss")),
                string.Format("在住人员：{0}", count));
            this._logger.LogInformation("导出在住旅客人数完成");
        }
    }
}
