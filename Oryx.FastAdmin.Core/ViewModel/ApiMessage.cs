using System;
using System.Threading.Tasks;

namespace Oryx.FastAdmin.Core.ViewModel
{
    public class ApiMessage
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "success!";

        public string Code { get; set; } = "0";

        public object Data { get; set; }

        public int Count { get; set; }

        public static async Task<ApiMessage> Wrap(Func<Task> action)
        {
            var apiMsg = new ApiMessage();
            try
            {
                await action();
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc);
            }

            return apiMsg;
        }

        public static async Task<ApiMessage> Wrap<T>(Func<Task<T>> func)
        {
            var apiMsg = new ApiMessage();
            try
            {
                apiMsg.Data = await func();
            }
            catch (Exception exc)
            {
                apiMsg.SetFault(exc.StackTrace);
            }
            return apiMsg;
        }

        public static async Task<T> WrapData<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception exc)
            {
                return default(T);
            }
        }

        public void SetFault(Exception exc)
        {
            SetFault(exc.Message);
        }

        public void SetFault(string msg)
        {

            Success = false;
            Message = msg;
        }
    }
}