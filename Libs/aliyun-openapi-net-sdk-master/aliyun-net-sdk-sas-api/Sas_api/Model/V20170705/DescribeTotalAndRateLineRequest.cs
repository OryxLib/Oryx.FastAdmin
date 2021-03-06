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
using Aliyun.Acs.Sas_api.Transform;
using Aliyun.Acs.Sas_api.Transform.V20170705;

namespace Aliyun.Acs.Sas_api.Model.V20170705
{
    public class DescribeTotalAndRateLineRequest : RpcAcsRequest<DescribeTotalAndRateLineResponse>
    {
        public DescribeTotalAndRateLineRequest()
            : base("Sas_api", "2017-07-05", "DescribeTotalAndRateLine", "sas-api", "openAPI")
        {
        }

		private string sourceIp;

		private int? apiType;

		public string SourceIp
		{
			get
			{
				return sourceIp;
			}
			set	
			{
				sourceIp = value;
				DictionaryUtil.Add(QueryParameters, "SourceIp", value);
			}
		}

		public int? ApiType
		{
			get
			{
				return apiType;
			}
			set	
			{
				apiType = value;
				DictionaryUtil.Add(QueryParameters, "ApiType", value.ToString());
			}
		}

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override DescribeTotalAndRateLineResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return DescribeTotalAndRateLineResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
