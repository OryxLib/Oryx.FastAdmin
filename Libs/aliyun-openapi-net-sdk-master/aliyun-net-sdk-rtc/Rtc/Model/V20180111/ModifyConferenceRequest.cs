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
using Aliyun.Acs.rtc.Transform;
using Aliyun.Acs.rtc.Transform.V20180111;

namespace Aliyun.Acs.rtc.Model.V20180111
{
    public class ModifyConferenceRequest : RpcAcsRequest<ModifyConferenceResponse>
    {
        public ModifyConferenceRequest()
            : base("rtc", "2018-01-11", "ModifyConference", "rtc", "openAPI")
        {
        }

		private string startTime;

		private string type;

		private string conferenceId;

		private string conferenceName;

		private long? ownerId;

		private string appId;

		private int? remindNotice;

		public string StartTime
		{
			get
			{
				return startTime;
			}
			set	
			{
				startTime = value;
				DictionaryUtil.Add(QueryParameters, "StartTime", value);
			}
		}

		public string Type
		{
			get
			{
				return type;
			}
			set	
			{
				type = value;
				DictionaryUtil.Add(QueryParameters, "Type", value);
			}
		}

		public string ConferenceId
		{
			get
			{
				return conferenceId;
			}
			set	
			{
				conferenceId = value;
				DictionaryUtil.Add(QueryParameters, "ConferenceId", value);
			}
		}

		public string ConferenceName
		{
			get
			{
				return conferenceName;
			}
			set	
			{
				conferenceName = value;
				DictionaryUtil.Add(QueryParameters, "ConferenceName", value);
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

		public string AppId
		{
			get
			{
				return appId;
			}
			set	
			{
				appId = value;
				DictionaryUtil.Add(QueryParameters, "AppId", value);
			}
		}

		public int? RemindNotice
		{
			get
			{
				return remindNotice;
			}
			set	
			{
				remindNotice = value;
				DictionaryUtil.Add(QueryParameters, "RemindNotice", value.ToString());
			}
		}

        public override ModifyConferenceResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return ModifyConferenceResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
