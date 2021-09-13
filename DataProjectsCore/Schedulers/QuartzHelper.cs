// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Nito.AsyncEx;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataProjectsCore.Schedulers
{
    public class QuartzHelper : IDisposable
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static QuartzHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static QuartzHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private fields and properties

        private readonly AsyncLock _mutex = new AsyncLock();
        public StdSchedulerFactory? _factory = null;
        public StdSchedulerFactory Factory
        {
            get
            {
                if (_factory == null)
                    _factory = new StdSchedulerFactory();
                return _factory;
            }
        }
        public IScheduler Scheduler { get; private set; }
        public IJobDetail JobCheckNextDay { get; private set; }
        public ITrigger? _trigger60sec = null;
        public ITrigger Trigger60sec
        {
            get
            {
                if (_trigger60sec == null)
                    _trigger60sec = TriggerBuilder.Create()
                        .WithIdentity(nameof(Trigger60sec))
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(10)
                            .RepeatForever())
                        .Build();
                return _trigger60sec;
            }
        }

        #endregion

        #region Constructor and destructor

        public QuartzHelper()
        {
            //
        }

        #endregion

        #region Public and private methods

        public async Task OpenAsync()
        {
            // AsyncLock can be locked asynchronously
            using (await _mutex.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromMilliseconds(1));

                Scheduler = Factory.GetScheduler().GetAwaiter().GetResult();
                _ = Scheduler.Start();
                JobBuilder jobBuilder = JobBuilder.Create<QuartzJobEntity>()
                    .WithIdentity(nameof(JobCheckNextDay));
                //foreach (QuartzParamEntity<T> item in parameters)
                //{
                //    jobBuilder.UsingJobData(item.Name, item.Value != null ? item.Value.ToString() : string.Empty);
                //}
                JobCheckNextDay = jobBuilder.Build();
                _ = Scheduler.ScheduleJob(JobCheckNextDay, Trigger60sec);
            }
        }

        public async Task CloseAsync()
        {
            await Scheduler.Shutdown();
        }

        public void Dispose()
        {
            _ = Task.Run(async () =>
            {
                await CloseAsync();
            });
        }

        #endregion
    }
}
