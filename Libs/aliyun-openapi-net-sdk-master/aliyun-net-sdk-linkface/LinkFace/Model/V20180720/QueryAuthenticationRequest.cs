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
using Aliyun.Acs.LinkFace.Transform;
using Aliyun.Acs.LinkFace.Transform.V20180720;
using System.Collections.Generic;

namespace Aliyun.Acs.LinkFace.Model.V20180720
{
    public class QueryAuthenticationRequest : RpcAcsRequest<QueryAuthenticationResponse>
    {
        public QueryAuthenticationRequest()
            : base("LinkFace", "2018-07-20", "QueryAuthentication")
        {
			Protocol = ProtocolType.HTTPS;
			Method = MethodType.POST;
        }

		private int? licenseType;

		private string iotId;

		private int? pageSize;

		private int? currentPage;

		private string deviceName;

		private string productKey;

		public int? LicenseType
		{
			get
			{
				return licenseType;
			}
			set	
			{
				licenseType = value;
				DictionaryUtil.Add(BodyParameters, "LicenseType", value.ToString());
			}
		}

		public string IotId
		{
			get
			{
				return iotId;
			}
			set	
			{
				iotId = value;
				DictionaryUtil.Add(BodyParameters, "IotId", value);
			}
		}

		public int? PageSize
		{
			get
			{
				return pageSize;
			}
			set	
			{
				pageSize = value;
				DictionaryUtil.Add(BodyParameters, "PageSize", value.ToString());
			}
		}

		public int? CurrentPage
		{
			get
			{
				return currentPage;
			}
			set	
			{
				currentPage = value;
				DictionaryUtil.Add(BodyParameters, "CurrentPage", value.ToString());
			}
		}

		public string DeviceName
		{
			get
			{
				return deviceName;
			}
			set	
			{
				deviceName = value;
				DictionaryUtil.Add(BodyParameters, "DeviceName", value);
			}
		}

		public string ProductKey
		{
			get
			{
				return productKey;
			}
			set	
			{
				productKey = value;
				DictionaryUtil.Add(BodyParameters, "ProductKey", value);
			}
		}

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override QueryAuthenticationResponse GetResponse(Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return QueryAuthenticationResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}