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
    public class UntagResourcesRequest : RpcAcsRequest<UntagResourcesResponse>
    {
        public UntagResourcesRequest()
            : base("Cbn", "2017-09-12", "UntagResources", "cbn", "openAPI")
        {
        }

		private long? resourceOwnerId;

		private List<string> resourceIds;

		private string resourceOwnerAccount;

		private string ownerAccount;

		private long? tagOwnerUid;

		private string action;

		private string tagOwnerBid;

		private long? ownerId;

		private List<string> tagKeys;

		private string resourceType;

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

		public List<string> ResourceIds
		{
			get
			{
				return resourceIds;
			}

			set
			{
				resourceIds = value;
				for (int i = 0; i < resourceIds.Count; i++)
				{
					DictionaryUtil.Add(QueryParameters,"ResourceId." + (i + 1) , resourceIds[i]);
				}
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

		public long? TagOwnerUid
		{
			get
			{
				return tagOwnerUid;
			}
			set	
			{
				tagOwnerUid = value;
				DictionaryUtil.Add(QueryParameters, "TagOwnerUid", value.ToString());
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

		public string TagOwnerBid
		{
			get
			{
				return tagOwnerBid;
			}
			set	
			{
				tagOwnerBid = value;
				DictionaryUtil.Add(QueryParameters, "TagOwnerBid", value);
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

		public List<string> TagKeys
		{
			get
			{
				return tagKeys;
			}

			set
			{
				tagKeys = value;
				for (int i = 0; i < tagKeys.Count; i++)
				{
					DictionaryUtil.Add(QueryParameters,"TagKey." + (i + 1) , tagKeys[i]);
				}
			}
		}

		public string ResourceType
		{
			get
			{
				return resourceType;
			}
			set	
			{
				resourceType = value;
				DictionaryUtil.Add(QueryParameters, "ResourceType", value);
			}
		}

        public override UntagResourcesResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return UntagResourcesResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
