using CoreTests;
using DataShareCore.Schedulers;
using NUnit.Framework;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DataShareCoreTests.Schedulers
{
    [TestFixture]
    internal class QuartzHelperTests
    {
        #region Public and private fields and properties

        private static readonly QuartzEntity _quartz = QuartzEntity.Instance;

        #endregion

        internal void Method([CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            TestContext.WriteLine($"{DateTime.Now}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}");
        }

        [Test]
        public void OpenClose_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(async () =>
            {
                TestContext.WriteLine("Open");
                _quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { Method(); }, "jobTest");

                await Task.Delay(TimeSpan.FromSeconds(7)).ConfigureAwait(true);
                _quartz.Close();
                TestContext.WriteLine("Close");
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }

        [Test]
        public void OpenClose_Throw()
        {
            TestsUtils.MethodStart();

            Assert.Throws<ArgumentException>(async () =>
            {
                TestContext.WriteLine("Open");
                _quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { Method(); }, "jobTest");

                await Task.Delay(TimeSpan.FromSeconds(7)).ConfigureAwait(true);
                TestContext.WriteLine("Close");
                _quartz.Close();
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }
    }
}
