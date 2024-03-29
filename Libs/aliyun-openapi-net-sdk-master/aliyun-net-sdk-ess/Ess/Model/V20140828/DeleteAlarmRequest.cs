/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */
using System.Collections.Generic;

using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Core.Utils;
using Aliyun.Acs.Ess.Transform;
using Aliyun.Acs.Ess.Transform.V20140828;

namespace Aliyun.Acs.Ess.Model.V20140828
{
    public class DeleteAlarmRequest : RpcAcsRequest<DeleteAlarmResponse>
    {
        public DeleteAlarmRequest()
            : base("Ess", "2014-08-28", "DeleteAlarm", "ess", "openAPI")
        {
        }

		private string resourceOwnerAccount;

		private long? ownerId;

		private string alarmTaskId;

		public string ResourceOwnerAccount
		{
			get
			{
				return resourceOwnerAccount;
			}
			set	
			{
				resourceOwnerAccount = value;
				DictionaryUtil.Add(QueryParameters, "ResourceOwnerAccount", value);
			}
		}

		public long? OwnerId
		{
			get
			{
				return ownerId;
			}
			set	
			{
				ownerId = value;
				DictionaryUtil.Add(QueryParameters, "OwnerId", value.ToString());
			}
		}

		public string AlarmTaskId
		{
			get
			{
				return alarmTaskId;
			}
			set	
			{
				alarmTaskId = value;
				DictionaryUtil.Add(QueryParameters, "AlarmTaskId", value);
			}
		}

        public override DeleteAlarmResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return DeleteAlarmResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
