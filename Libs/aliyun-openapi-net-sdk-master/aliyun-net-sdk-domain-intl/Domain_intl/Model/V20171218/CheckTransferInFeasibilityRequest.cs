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
using Aliyun.Acs.Domain_intl.Transform;
using Aliyun.Acs.Domain_intl.Transform.V20171218;

namespace Aliyun.Acs.Domain_intl.Model.V20171218
{
    public class CheckTransferInFeasibilityRequest : RpcAcsRequest<CheckTransferInFeasibilityResponse>
    {
        public CheckTransferInFeasibilityRequest()
            : base("Domain-intl", "2017-12-18", "CheckTransferInFeasibility", "domain", "openAPI")
        {
        }

		private string transferAuthorizationCode;

		private string userClientIp;

		private string domainName;

		private string lang;

		public string TransferAuthorizationCode
		{
			get
			{
				return transferAuthorizationCode;
			}
			set	
			{
				transferAuthorizationCode = value;
				DictionaryUtil.Add(QueryParameters, "TransferAuthorizationCode", value);
			}
		}

		public string UserClientIp
		{
			get
			{
				return userClientIp;
			}
			set	
			{
				userClientIp = value;
				DictionaryUtil.Add(QueryParameters, "UserClientIp", value);
			}
		}

		public string DomainName
		{
			get
			{
				return domainName;
			}
			set	
			{
				domainName = value;
				DictionaryUtil.Add(QueryParameters, "DomainName", value);
			}
		}

		public string Lang
		{
			get
			{
				return lang;
			}
			set	
			{
				lang = value;
				DictionaryUtil.Add(QueryParameters, "Lang", value);
			}
		}

        public override CheckTransferInFeasibilityResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return CheckTransferInFeasibilityResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
