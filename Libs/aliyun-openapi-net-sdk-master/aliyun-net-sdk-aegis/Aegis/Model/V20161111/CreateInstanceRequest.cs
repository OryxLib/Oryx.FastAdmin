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
using Aliyun.Acs.aegis.Transform;
using Aliyun.Acs.aegis.Transform.V20161111;

namespace Aliyun.Acs.aegis.Model.V20161111
{
    public class CreateInstanceRequest : RpcAcsRequest<CreateInstanceResponse>
    {
        public CreateInstanceRequest()
            : base("aegis", "2016-11-11", "CreateInstance", "vipaegis", "openAPI")
        {
        }

		private int? duration;

		private bool? isAutoRenew;

		private string clientToken;

		private int? vmNumber;

		private long? ownerId;

		private int? versionCode;

		private string pricingCycle;

		private int? autoRenewDuration;

		public int? Duration
		{
			get
			{
				return duration;
			}
			set	
			{
				duration = value;
				DictionaryUtil.Add(QueryParameters, "Duration", value.ToString());
			}
		}

		public bool? IsAutoRenew
		{
			get
			{
				return isAutoRenew;
			}
			set	
			{
				isAutoRenew = value;
				DictionaryUtil.Add(QueryParameters, "IsAutoRenew", value.ToString());
			}
		}

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

		public int? VmNumber
		{
			get
			{
				return vmNumber;
			}
			set	
			{
				vmNumber = value;
				DictionaryUtil.Add(QueryParameters, "VmNumber", value.ToString());
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

		public int? VersionCode
		{
			get
			{
				return versionCode;
			}
			set	
			{
				versionCode = value;
				DictionaryUtil.Add(QueryParameters, "VersionCode", value.ToString());
			}
		}

		public string PricingCycle
		{
			get
			{
				return pricingCycle;
			}
			set	
			{
				pricingCycle = value;
				DictionaryUtil.Add(QueryParameters, "PricingCycle", value);
			}
		}

		public int? AutoRenewDuration
		{
			get
			{
				return autoRenewDuration;
			}
			set	
			{
				autoRenewDuration = value;
				DictionaryUtil.Add(QueryParameters, "AutoRenewDuration", value.ToString());
			}
		}

        public override CreateInstanceResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return CreateInstanceResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
