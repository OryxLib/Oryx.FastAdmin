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
using Aliyun.Acs.cloudwf.Transform;
using Aliyun.Acs.cloudwf.Transform.V20170328;

namespace Aliyun.Acs.cloudwf.Model.V20170328
{
    public class ProfileHistoryRequest : RpcAcsRequest<ProfileHistoryResponse>
    {
        public ProfileHistoryRequest()
            : base("cloudwf", "2017-03-28", "ProfileHistory", "cloudwf", "openAPI")
        {
        }

		private long? idtype;

		private string endMonth;

		private string beginMonth;

		private string accessKeyId;

		private long? agsid;

		public long? Idtype
		{
			get
			{
				return idtype;
			}
			set	
			{
				idtype = value;
				DictionaryUtil.Add(QueryParameters, "Idtype", value.ToString());
			}
		}

		public string EndMonth
		{
			get
			{
				return endMonth;
			}
			set	
			{
				endMonth = value;
				DictionaryUtil.Add(QueryParameters, "EndMonth", value);
			}
		}

		public string BeginMonth
		{
			get
			{
				return beginMonth;
			}
			set	
			{
				beginMonth = value;
				DictionaryUtil.Add(QueryParameters, "BeginMonth", value);
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

		public long? Agsid
		{
			get
			{
				return agsid;
			}
			set	
			{
				agsid = value;
				DictionaryUtil.Add(QueryParameters, "Agsid", value.ToString());
			}
		}

        public override ProfileHistoryResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return ProfileHistoryResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
