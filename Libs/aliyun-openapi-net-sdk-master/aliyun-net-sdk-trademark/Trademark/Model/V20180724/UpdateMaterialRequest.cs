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
using Aliyun.Acs.Trademark.Transform;
using Aliyun.Acs.Trademark.Transform.V20180724;

namespace Aliyun.Acs.Trademark.Model.V20180724
{
    public class UpdateMaterialRequest : RpcAcsRequest<UpdateMaterialResponse>
    {
        public UpdateMaterialRequest()
            : base("Trademark", "2018-07-24", "UpdateMaterial", "trademark", "openAPI")
        {
        }

		private string contactEmail;

		private string contactAddress;

		private string eAddress;

		private string legalNoticeOssKey;

		private string address;

		private string town;

		private string contactNumber;

		private string city;

		private string idCardOssKey;

		private string contactName;

		private string passportOssKey;

		private string contactZipcode;

		private string eName;

		private string province;

		private string businessLicenceOssKey;

		private string name;

		private long? id;

		private string cardNumber;

		private long? loaId;

		private string loaOssKey;

		public string ContactEmail
		{
			get
			{
				return contactEmail;
			}
			set	
			{
				contactEmail = value;
				DictionaryUtil.Add(QueryParameters, "ContactEmail", value);
			}
		}

		public string ContactAddress
		{
			get
			{
				return contactAddress;
			}
			set	
			{
				contactAddress = value;
				DictionaryUtil.Add(QueryParameters, "ContactAddress", value);
			}
		}

		public string EAddress
		{
			get
			{
				return eAddress;
			}
			set	
			{
				eAddress = value;
				DictionaryUtil.Add(QueryParameters, "EAddress", value);
			}
		}

		public string LegalNoticeOssKey
		{
			get
			{
				return legalNoticeOssKey;
			}
			set	
			{
				legalNoticeOssKey = value;
				DictionaryUtil.Add(QueryParameters, "LegalNoticeOssKey", value);
			}
		}

		public string Address
		{
			get
			{
				return address;
			}
			set	
			{
				address = value;
				DictionaryUtil.Add(QueryParameters, "Address", value);
			}
		}

		public string Town
		{
			get
			{
				return town;
			}
			set	
			{
				town = value;
				DictionaryUtil.Add(QueryParameters, "Town", value);
			}
		}

		public string ContactNumber
		{
			get
			{
				return contactNumber;
			}
			set	
			{
				contactNumber = value;
				DictionaryUtil.Add(QueryParameters, "ContactNumber", value);
			}
		}

		public string City
		{
			get
			{
				return city;
			}
			set	
			{
				city = value;
				DictionaryUtil.Add(QueryParameters, "City", value);
			}
		}

		public string IdCardOssKey
		{
			get
			{
				return idCardOssKey;
			}
			set	
			{
				idCardOssKey = value;
				DictionaryUtil.Add(QueryParameters, "IdCardOssKey", value);
			}
		}

		public string ContactName
		{
			get
			{
				return contactName;
			}
			set	
			{
				contactName = value;
				DictionaryUtil.Add(QueryParameters, "ContactName", value);
			}
		}

		public string PassportOssKey
		{
			get
			{
				return passportOssKey;
			}
			set	
			{
				passportOssKey = value;
				DictionaryUtil.Add(QueryParameters, "PassportOssKey", value);
			}
		}

		public string ContactZipcode
		{
			get
			{
				return contactZipcode;
			}
			set	
			{
				contactZipcode = value;
				DictionaryUtil.Add(QueryParameters, "ContactZipcode", value);
			}
		}

		public string EName
		{
			get
			{
				return eName;
			}
			set	
			{
				eName = value;
				DictionaryUtil.Add(QueryParameters, "EName", value);
			}
		}

		public string Province
		{
			get
			{
				return province;
			}
			set	
			{
				province = value;
				DictionaryUtil.Add(QueryParameters, "Province", value);
			}
		}

		public string BusinessLicenceOssKey
		{
			get
			{
				return businessLicenceOssKey;
			}
			set	
			{
				businessLicenceOssKey = value;
				DictionaryUtil.Add(QueryParameters, "BusinessLicenceOssKey", value);
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

		public long? Id
		{
			get
			{
				return id;
			}
			set	
			{
				id = value;
				DictionaryUtil.Add(QueryParameters, "Id", value.ToString());
			}
		}

		public string CardNumber
		{
			get
			{
				return cardNumber;
			}
			set	
			{
				cardNumber = value;
				DictionaryUtil.Add(QueryParameters, "CardNumber", value);
			}
		}

		public long? LoaId
		{
			get
			{
				return loaId;
			}
			set	
			{
				loaId = value;
				DictionaryUtil.Add(QueryParameters, "LoaId", value.ToString());
			}
		}

		public string LoaOssKey
		{
			get
			{
				return loaOssKey;
			}
			set	
			{
				loaOssKey = value;
				DictionaryUtil.Add(QueryParameters, "LoaOssKey", value);
			}
		}

        public override UpdateMaterialResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return UpdateMaterialResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
