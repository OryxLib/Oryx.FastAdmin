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

namespace Aliyun.Acs.Sas.Model.V20181203
{
	public class DescribeEmgVulGroupResponse : AcsResponse
	{

		private string requestId;

		private int? totalCount;

		private List<DescribeEmgVulGroup_EmgVulGroup> emgVulGroupList;

		public string RequestId
		{
			get
			{
				return requestId;
			}
			set	
			{
				requestId = value;
			}
		}

		public int? TotalCount
		{
			get
			{
				return totalCount;
			}
			set	
			{
				totalCount = value;
			}
		}

		public List<DescribeEmgVulGroup_EmgVulGroup> EmgVulGroupList
		{
			get
			{
				return emgVulGroupList;
			}
			set	
			{
				emgVulGroupList = value;
			}
		}

		public class DescribeEmgVulGroup_EmgVulGroup
		{

			private string aliasName;

			private int? pendingCount;

			private string name;

			private long? gmtPublish;

			private string description;

			private string type;

			public string AliasName
			{
				get
				{
					return aliasName;
				}
				set	
				{
					aliasName = value;
				}
			}

			public int? PendingCount
			{
				get
				{
					return pendingCount;
				}
				set	
				{
					pendingCount = value;
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
				}
			}

			public long? GmtPublish
			{
				get
				{
					return gmtPublish;
				}
				set	
				{
					gmtPublish = value;
				}
			}

			public string Description
			{
				get
				{
					return description;
				}
				set	
				{
					description = value;
				}
			}

			public string Type
			{
				get
				{
					return type;
				}
				set	
				{
					type = value;
				}
			}
		}
	}
}
