
using System;
using System.Collections.Generic;
using System.Text;

namespace Oryx.Utilities.JPush
{
    //Install-Package Jiguang.JPush -Version 1.2.2
    public class JPushHelper
    {
        const string AppKey = "4ca1123b24fb4f9dcac5a8c2";
        const string MasterSecret = "4a172a04d23ce5ec7a9a9bfc";

        public void Push(string registionId, Jiguang.JPush.Model.Message message)
        {
            Jiguang.JPush.JPushClient client = new Jiguang.JPush.JPushClient(AppKey, MasterSecret);
            client.SendPush(new Jiguang.JPush.Model.PushPayload
            {
                Platform = "all", //"all",["android","ios"],
                Audience = AudienceType.All,
                CId = registionId,
                Message = message,
                Notification = new Jiguang.JPush.Model.Notification
                {
                    Alert = "alert"
                }
            });
        }

        public void BroadCast(string message)
        {

        }
    }

    public class AudienceType
    {
        public const string All = "all";

        /// <summary>
        /// 数组。多个标签之间是 OR 的关系，即取并集。
        /// 用标签来进行大规模的设备属性、用户属性分群。 一次推送最多 20 个。
        /// </summary>
        public const string Tag = "tag";

        /// <summary>
        /// 数组。多个标签之间是 AND 关系，即取交集。
        /// 一次推送最多 20 个。
        /// </summary>
        public const string TagEnd = "tag_and";

        /// <summary>
        /// 数组。多个标签之间，先取多标签的并集，再对该结果取补集。
        /// 一次推送最多 20 个。
        /// </summary>
        public const string TagNot = "tag_not";

        /// <summary>
        /// 数组。多个别名之间是 OR 关系，即取并集。
        /// 用别名来标识一个用户。一个设备只能绑定一个别名，但多个设备可以绑定同一个别名。一次推送最多 1000 个。
        /// </summary>
        public const string Alias = "alias";

        /// <summary>
        /// 数组。多个注册 ID 之间是 OR 关系，即取并集
        /// </summary>
        public const string RegistrationId = "registration_id";


        /// <summary>
        /// 在页面创建的用户分群的 ID。定义为数组，但目前限制一次只能推送一个。
        /// </summary>
        public const string Segment = "segment";

        /// <summary>
        /// 在页面创建的 A/B 测试的 ID。定义为数组，但目前限制是一次只能推送一个。
        /// </summary>
        public const string AB = "abtest";
    }
}
