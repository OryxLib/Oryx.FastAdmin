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
using Aliyun.Acs.Cms.Transform;
using Aliyun.Acs.Cms.Transform.V20180308;

namespace Aliyun.Acs.Cms.Model.V20180308
{
    public class StopTasksRequest : RpcAcsRequest<StopTasksResponse>
    {
        public StopTasksRequest()
            : base("Cms", "2018-03-08", "StopTasks", "cms", "openAPI")
        {
        }

		private string taskIds;

		public string TaskIds
		{
			get
			{
				return taskIds;
			}
			set	
			{
				taskIds = value;
				DictionaryUtil.Add(QueryParameters, "TaskIds", value);
			}
		}

        public override StopTasksResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return StopTasksResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
