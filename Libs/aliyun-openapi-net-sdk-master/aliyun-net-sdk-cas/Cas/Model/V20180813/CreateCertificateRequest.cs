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
using Aliyun.Acs.cas.Transform;
using Aliyun.Acs.cas.Transform.V20180813;

namespace Aliyun.Acs.cas.Model.V20180813
{
    public class CreateCertificateRequest : RpcAcsRequest<CreateCertificateResponse>
    {
        public CreateCertificateRequest()
            : base("cas", "2018-08-13", "CreateCertificate", "cas_esign_fdd", "openAPI")
        {
        }

		private string sourceIp;

		private string name;

		private string cert;

		private string lang;

		private string key;

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

		public string Name
		{
			get
			{
				return name;
			}
			set	
			{
				name = value;
				DictionaryUtil.Add(QueryParameters, "Name", value);
			}
		}

		public string Cert
		{
			get
			{
				return cert;
			}
			set	
			{
				cert = value;
				DictionaryUtil.Add(QueryParameters, "Cert", value);
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

		public string Key
		{
			get
			{
				return key;
			}
			set	
			{
				key = value;
				DictionaryUtil.Add(QueryParameters, "Key", value);
			}
		}

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override CreateCertificateResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return CreateCertificateResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
