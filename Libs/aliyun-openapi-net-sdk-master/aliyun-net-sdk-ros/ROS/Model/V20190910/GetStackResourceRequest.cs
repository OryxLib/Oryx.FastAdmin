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
using Aliyun.Acs.ROS;
using Aliyun.Acs.ROS.Transform;
using Aliyun.Acs.ROS.Transform.V20190910;

namespace Aliyun.Acs.ROS.Model.V20190910
{
    public class GetStackResourceRequest : RpcAcsRequest<GetStackResourceResponse>
    {
        public GetStackResourceRequest()
            : base("ROS", "2019-09-10", "GetStackResource")
        {
            if (this.GetType().GetProperty("ProductEndpointMap") != null && this.GetType().GetProperty("ProductEndpointType") != null)
            {
                this.GetType().GetProperty("ProductEndpointMap").SetValue(this, Endpoint.endpointMap, null);
                this.GetType().GetProperty("ProductEndpointType").SetValue(this, Endpoint.endpointRegionalType, null);
            }
        }

		private string clientToken;

		private string stackId;

		private string logicalResourceId;

		private bool? showResourceAttributes;

		public string ClientToken
		{
			get
			{
				return clientToken;
			}
			set	
			{
				clientToken = value;
				DictionaryUtil.Add(QueryParameters, "ClientToken", value);
			}
		}

		public string StackId
		{
			get
			{
				return stackId;
			}
			set	
			{
				stackId = value;
				DictionaryUtil.Add(QueryParameters, "StackId", value);
			}
		}

		public string LogicalResourceId
		{
			get
			{
				return logicalResourceId;
			}
			set	
			{
				logicalResourceId = value;
				DictionaryUtil.Add(QueryParameters, "LogicalResourceId", value);
			}
		}

		public bool? ShowResourceAttributes
		{
			get
			{
				return showResourceAttributes;
			}
			set	
			{
				showResourceAttributes = value;
				DictionaryUtil.Add(QueryParameters, "ShowResourceAttributes", value.ToString());
			}
		}

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override GetStackResourceResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return GetStackResourceResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
