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
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Core.Utils;
using Aliyun.Acs.TeslaStream.Transform;
using Aliyun.Acs.TeslaStream.Transform.V20180115;
using System.Collections.Generic;

namespace Aliyun.Acs.TeslaStream.Model.V20180115
{
    public class GetJobTopologyRequest : RpcAcsRequest<GetJobTopologyResponse>
    {
        public GetJobTopologyRequest()
            : base("TeslaStream", "2018-01-15", "GetJobTopology", "teslastream", "openAPI")
        {
        }

		private string jobName;

		public string JobName
		{
			get
			{
				return jobName;
			}
			set	
			{
				jobName = value;
				DictionaryUtil.Add(QueryParameters, "JobName", value);
			}
		}

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override GetJobTopologyResponse GetResponse(Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return GetJobTopologyResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}