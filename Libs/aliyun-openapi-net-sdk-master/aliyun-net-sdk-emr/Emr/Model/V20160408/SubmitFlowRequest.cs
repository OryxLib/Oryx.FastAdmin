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
using Aliyun.Acs.Emr.Transform;
using Aliyun.Acs.Emr.Transform.V20160408;

namespace Aliyun.Acs.Emr.Model.V20160408
{
    public class SubmitFlowRequest : RpcAcsRequest<SubmitFlowResponse>
    {
        public SubmitFlowRequest()
            : base("Emr", "2016-04-08", "SubmitFlow", "emr", "openAPI")
        {
        }

		private string regionId;

		private string conf;

		private string projectId;

		private string flowId;

		public string RegionId
		{
			get
			{
				return regionId;
			}
			set	
			{
				regionId = value;
				DictionaryUtil.Add(QueryParameters, "RegionId", value);
			}
		}

		public string Conf
		{
			get
			{
				return conf;
			}
			set	
			{
				conf = value;
				DictionaryUtil.Add(QueryParameters, "Conf", value);
			}
		}

		public string ProjectId
		{
			get
			{
				return projectId;
			}
			set	
			{
				projectId = value;
				DictionaryUtil.Add(QueryParameters, "ProjectId", value);
			}
		}

		public string FlowId
		{
			get
			{
				return flowId;
			}
			set	
			{
				flowId = value;
				DictionaryUtil.Add(QueryParameters, "FlowId", value);
			}
		}

        public override SubmitFlowResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return SubmitFlowResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
