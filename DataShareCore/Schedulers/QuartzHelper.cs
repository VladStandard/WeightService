// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Nito.AsyncEx;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DataShareCore.Schedulers
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

        private readonly AsyncLock _mutex = new();
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
        public IScheduler? Scheduler { get; private set; } = null;

        #endregion

        #region Constructor and destructor

        public QuartzHelper()
        {
            //
        }

        #endregion

        #region Public and private methods

        private async Task AddJobTemplateAsync(QuartzEnums.Interval interval, int length, TimeSpan timeSpan, string cronExpression,
            bool repeatForever, Action action)
        {
            // AsyncLock can be locked asynchronously
            using (await _mutex.LockAsync())
            {
                // It's safe to await while the lock is held
                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);

                // Trigger.
                ITrigger? trigger;
                switch (interval)
                {
                    case QuartzEnums.Interval.TimeSpan:
                        trigger = QuartzUtils.CreateTrigger(timeSpan, repeatForever);
                        break;
                    case QuartzEnums.Interval.Cron:
                        trigger = QuartzUtils.CreateTrigger(cronExpression, repeatForever);
                        break;
                    default:
                        trigger = QuartzUtils.CreateTrigger(interval, length, repeatForever);
                        break;
                }
                if (trigger == null)
                    return;
                // Job.
                IJobDetail? jobDetail = QuartzUtils.CreateJobDetail(action);
                if (jobDetail == null)
                    return;

                if (Scheduler == null)
                {
                    Scheduler = await Factory.GetScheduler().ConfigureAwait(true);
                    await StartAsync().ConfigureAwait(true);
                }
                await Scheduler.ScheduleJob(jobDetail, trigger).ConfigureAwait(true);
            }
        }

        public async Task AddJobAsync(QuartzEnums.Interval interval, int length, bool repeatForever, Action action) => 
            await AddJobTemplateAsync(interval, length, new TimeSpan(0), string.Empty, repeatForever, action).ConfigureAwait(true);

        public async Task AddJobAsync(TimeSpan timeSpan, bool repeatForever, Action action) =>
            await AddJobTemplateAsync(QuartzEnums.Interval.TimeSpan, 0, timeSpan, string.Empty, repeatForever, action).ConfigureAwait(true);

        public async Task AddJobAsync(string cronExpression, Action action) =>
            await AddJobTemplateAsync(QuartzEnums.Interval.Cron, 0, new TimeSpan(0), cronExpression, false, action).ConfigureAwait(true);

        public void AddJob(QuartzEnums.Interval interval, int length, bool repeatForever, Action action) =>
            AddJobAsync(interval, length, repeatForever, action).ConfigureAwait(true);

        public void AddJob(TimeSpan timeSpan, bool repeatForever, Action action) =>
            AddJobAsync(timeSpan, repeatForever, action).ConfigureAwait(true);

        public void AddJob(string cronExpression, Action action) =>
            AddJobAsync(cronExpression, action).ConfigureAwait(true);

        public async Task CloseAsync()
        {
            if (Scheduler != null)
                await Scheduler.Shutdown().ConfigureAwait(true);
        }

        public void Close() =>
            CloseAsync().ConfigureAwait(true);

        public void Dispose()
        {
            Close();
        }

        public async Task StartAsync()
        {
            if (Scheduler == null)
                return;
            await Scheduler.Start().ConfigureAwait(true);
            //ITrigger trigger = .GetAwaiter().GetResult();
            //trigger?.GetTriggerBuilder().StartNow();
        }

        public void Start()
        {
            StartAsync().ConfigureAwait(true);
        }

        #endregion
    }
}
