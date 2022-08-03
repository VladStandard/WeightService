//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using Nito.AsyncEx;
//using Quartz;
//using Quartz.Impl;
//using System;
//using System.Threading;
//using System.Threading.Tasks;

//namespace DataCore.Schedulers
//{
//    public class QuartzHelper : IDisposable
//    {
//        #region Design pattern "Lazy Singleton"

//#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//        private static QuartzHelper _instance;
//#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
//        public static QuartzHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

//        #endregion

//        #region Public and private fields, properties, constructor

//        private readonly AsyncLock _mutex = new();
//        public StdSchedulerFactory? _factory = null;
//        public StdSchedulerFactory Factory
//        {
//            get
//            {
//                if (_factory == null)
//                    _factory = new StdSchedulerFactory();
//                return _factory;
//            }
//        }
//        public IScheduler? _scheduler = null;
//        public IScheduler Scheduler
//        {
//            get
//            {
//                if (_scheduler == null)
//                    //_scheduler = await Factory.GetScheduler().ConfigureAwait(false);
//                    _scheduler = Factory.GetScheduler().Result;
//                return _scheduler;
//            }
//        }

//        #endregion

//        #region Public and private methods

//        private async Task AddJobTemplateAsync(QuartzEnums.Interval interval, int length, TimeSpan timeSpan, string cronExpression,
//            bool repeatForever, Action action, string jobName)
//        {
//            // AsyncLock can be locked asynchronously
//            using (await _mutex.LockAsync())
//            {
//                // It's safe to await while the lock is held
//                await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
//                ITrigger? trigger = interval switch
//                {
//                    QuartzEnums.Interval.TimeSpan => QuartzUtils.CreateTrigger(timeSpan, repeatForever),
//                    QuartzEnums.Interval.Cron => QuartzUtils.CreateTrigger(cronExpression, repeatForever),
//                    _ => QuartzUtils.CreateTrigger(interval, length, repeatForever),
//                };
//                if (trigger == null)
//                    return;

//                // Job.
//                IJobDetail? jobDetail = QuartzUtils.CreateJobDetail(interval, cronExpression, action, jobName);
//                if (jobDetail == null)
//                    return;

//                await StartAsync().ConfigureAwait(false);
//                await Scheduler.ScheduleJob(jobDetail, trigger).ConfigureAwait(false);
//            }
//        }

//        public async Task AddJobAsync(QuartzEnums.Interval interval, int length, bool repeatForever, Action action, string jobName) => 
//            await AddJobTemplateAsync(interval, length, new TimeSpan(0), string.Empty, repeatForever, action, jobName).ConfigureAwait(false);

//        public async Task AddJobAsync(TimeSpan timeSpan, bool repeatForever, Action action, string jobName) =>
//            await AddJobTemplateAsync(QuartzEnums.Interval.TimeSpan, 0, timeSpan, string.Empty, repeatForever, action, jobName).ConfigureAwait(false);

//        public async Task AddJobAsync(string cronExpression, Action action, string jobName) =>
//            await AddJobTemplateAsync(QuartzEnums.Interval.Cron, 0, new TimeSpan(0), cronExpression, false, action, jobName).ConfigureAwait(false);

//        public void AddJob(QuartzEnums.Interval interval, int length, bool repeatForever, Action action, string jobName) =>
//            AddJobAsync(interval, length, repeatForever, action, jobName).ConfigureAwait(false);

//        public void AddJob(TimeSpan timeSpan, bool repeatForever, Action action, string jobName) =>
//            AddJobAsync(timeSpan, repeatForever, action, jobName).ConfigureAwait(false);

//        public void AddJob(string cronExpression, Action action, string jobName) =>
//            AddJobAsync(cronExpression, action, jobName).ConfigureAwait(false);

//        public async Task CloseAsync()
//        {
//            if (!Scheduler.IsShutdown)
//                await Scheduler.Shutdown().ConfigureAwait(false);
//        }

//        public void Close() => CloseAsync().ConfigureAwait(false);

//        public void Dispose()
//        {
//            Close();
//        }

//        public async Task StartAsync()
//        {
//            if (!Scheduler.IsStarted)
//                await Scheduler.Start().ConfigureAwait(false);
//            //ITrigger trigger = .GetAwaiter().GetResult();
//            //trigger?.GetTriggerBuilder().StartNow();
//        }

//        public void Start()
//        {
//            StartAsync().ConfigureAwait(false);
//        }

//        #endregion
//    }
//}
