// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Quartz;
using System;
using System.Threading.Tasks;

namespace DataProjectsCore.Schedulers
{
    public class QuartzJobEntity : IJob
    {
        #region Public and private fields and properties

        #endregion

        #region Public and private methods


        public async Task Execute(IJobExecutionContext context)
        {
            //await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);

            //JobKey key = context.JobDetail.Key;
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            //foreach (object? value in dataMap.Values)
            //{
            //}
            //string value = dataMap.GetString("jobSays");
            await Console.Out.WriteLineAsync($"{DateTime.Now}. ").ConfigureAwait(false);
            //_log.Information($"{nameof(QuartzJobEntity)} Execute. jobSays: {jobSays}");
        }

        #endregion
    }
}
