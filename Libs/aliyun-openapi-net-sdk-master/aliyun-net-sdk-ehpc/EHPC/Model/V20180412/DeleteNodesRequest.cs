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
using Aliyun.Acs.EHPC.Transform;
using Aliyun.Acs.EHPC.Transform.V20180412;

namespace Aliyun.Acs.EHPC.Model.V20180412
{
    public class DeleteNodesRequest : RpcAcsRequest<DeleteNodesResponse>
    {
        public DeleteNodesRequest()
            : base("EHPC", "2018-04-12", "DeleteNodes", "ehs", "openAPI")
        {
        }

		private bool? releaseInstance;

		private List<Instance> instances;

		private string action;

		private string clusterId;

		private string accessKeyId;

		public bool? ReleaseInstance
		{
			get
			{
				return releaseInstance;
			}
			set	
			{
				releaseInstance = value;
				DictionaryUtil.Add(QueryParameters, "ReleaseInstance", value.ToString());
			}
		}

		public List<Instance> Instances
		{
			get
			{
				return instances;
			}

			set
			{
				instances = value;
				for (int i = 0; i < instances.Count; i++)
				{
					DictionaryUtil.Add(QueryParameters,"Instance." + (i + 1) + ".Id", instances[i].Id);
				}
			}
		}

		public string Action
		{
			get
			{
				return action;
			}
			set	
			{
				action = value;
				DictionaryUtil.Add(QueryParameters, "Action", value);
			}
		}

		public string ClusterId
		{
			get
			{
				return clusterId;
			}
			set	
			{
				clusterId = value;
				DictionaryUtil.Add(QueryParameters, "ClusterId", value);
			}
		}

		public string AccessKeyId
		{
			get
			{
				return accessKeyId;
			}
			set	
			{
				accessKeyId = value;
				DictionaryUtil.Add(QueryParameters, "AccessKeyId", value);
			}
		}

		public class Instance
		{

			private string id;

			public string Id
			{
				get
				{
					return id;
				}
				set	
				{
					id = value;
				}
			}
		}

        public override DeleteNodesResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return DeleteNodesResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
