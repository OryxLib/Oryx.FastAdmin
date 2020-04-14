using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Oryx.Utilities.Hangfire
{
    public class TaskRunnerTool
    {
        public static void RecurringJobs(Expression<Action> actionExpression, string corn)
        {
            RecurringJob.AddOrUpdate(actionExpression, corn);
        }

        /// <summary>
        /// 计划执行任务, 并返回任务id
        /// </summary>
        /// <param name="actionExpression">方法表达式</param>
        /// <param name="dateTime">执行的时间点</param>
        /// <returns></returns>
        public static string Scheduler(Expression<Action> actionExpression, DateTime dateTime)
        {
            return BackgroundJob.Schedule(actionExpression, dateTime - DateTime.Now);
        }
    }
}
