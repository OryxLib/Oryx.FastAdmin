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
using Aliyun.Acs.Cbn.Transform;
using Aliyun.Acs.Cbn.Transform.V20170912;

namespace Aliyun.Acs.Cbn.Model.V20170912
{
    public class DescribeCenGeographicSpanRemainingBandwidthRequest : RpcAcsRequest<DescribeCenGeographicSpanRemainingBandwidthResponse>
    {
        public DescribeCenGeographicSpanRemainingBandwidthRequest()
            : base("Cbn", "2017-09-12", "DescribeCenGeographicSpanRemainingBandwidth", "cbn", "openAPI")
        {
        }

		private string geographicRegionBId;

		private long? resourceOwnerId;

		private string geographicRegionAId;

		private string resourceOwnerAccount;

		private string cenId;

		private string ownerAccount;

		private int? pageSize;

		private string action;

		private long? ownerId;

		private int? pageNumber;

		public string GeographicRegionBId
		{
			get
			{
				return geographicRegionBId;
			}
			set	
			{
				geographicRegionBId = value;
				DictionaryUtil.Add(QueryParameters, "GeographicRegionBId", value);
			}
		}

		public long? ResourceOwnerId
		{
			get
			{
				return resourceOwnerId;
			}
			set	
			{
				resourceOwnerId = value;
				DictionaryUtil.Add(QueryParameters, "ResourceOwnerId", value.ToString());
			}
		}

		public string GeographicRegionAId
		{
			get
			{
				return geographicRegionAId;
			}
			set	
			{
				geographicRegionAId = value;
				DictionaryUtil.Add(QueryParameters, "GeographicRegionAId", value);
			}
		}

		public string ResourceOwnerAccount
		{
			get
			{
				return resourceOwnerAccount;
			}
			set	
			{
				resourceOwnerAccount = value;
				DictionaryUtil.Add(QueryParameters, "ResourceOwnerAccount", value);
			}
		}

		public string CenId
		{
			get
			{
				return cenId;
			}
			set	
			{
				cenId = value;
				DictionaryUtil.Add(QueryParameters, "CenId", value);
			}
		}

		public string OwnerAccount
		{
			get
			{
				return ownerAccount;
			}
			set	
			{
				ownerAccount = value;
				DictionaryUtil.Add(QueryParameters, "OwnerAccount", value);
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
				DictionaryUtil.Add(QueryParameters, "PageSize", value.ToString());
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

		public int? PageNumber
		{
			get
			{
				return pageNumber;
			}
			set	
			{
				pageNumber = value;
				DictionaryUtil.Add(QueryParameters, "PageNumber", value.ToString());
			}
		}

        public override DescribeCenGeographicSpanRemainingBandwidthResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return DescribeCenGeographicSpanRemainingBandwidthResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
