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
using Aliyun.Acs.Dds.Transform;
using Aliyun.Acs.Dds.Transform.V20151201;

namespace Aliyun.Acs.Dds.Model.V20151201
{
    public class DescribeReplicationGroupRequest : RpcAcsRequest<DescribeReplicationGroupResponse>
    {
        public DescribeReplicationGroupRequest()
            : base("Dds", "2015-12-01", "DescribeReplicationGroup", "Dds", "openAPI")
        {
        }

		private string destinationInstanceIds;

		private long? resourceOwnerId;

		private string securityToken;

		private string resourceOwnerAccount;

		private string replicationGroupId;

		private string ownerAccount;

		private string action;

		private string sourceInstanceId;

		private long? ownerId;

		private string accessKeyId;

		public string DestinationInstanceIds
		{
			get
			{
				return destinationInstanceIds;
			}
			set	
			{
				destinationInstanceIds = value;
				DictionaryUtil.Add(QueryParameters, "DestinationInstanceIds", value);
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

		public string SecurityToken
		{
			get
			{
				return securityToken;
			}
			set	
			{
				securityToken = value;
				DictionaryUtil.Add(QueryParameters, "SecurityToken", value);
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

		public string ReplicationGroupId
		{
			get
			{
				return replicationGroupId;
			}
			set	
			{
				replicationGroupId = value;
				DictionaryUtil.Add(QueryParameters, "ReplicationGroupId", value);
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

		public string SourceInstanceId
		{
			get
			{
				return sourceInstanceId;
			}
			set	
			{
				sourceInstanceId = value;
				DictionaryUtil.Add(QueryParameters, "SourceInstanceId", value);
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

		public override bool CheckShowJsonItemName()
		{
			return false;
		}

        public override DescribeReplicationGroupResponse GetResponse(UnmarshallerContext unmarshallerContext)
        {
            return DescribeReplicationGroupResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}
