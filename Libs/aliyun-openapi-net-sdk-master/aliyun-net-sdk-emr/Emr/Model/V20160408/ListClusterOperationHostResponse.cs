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

namespace Aliyun.Acs.Emr.Model.V20160408
{
	public class ListClusterOperationHostResponse : AcsResponse
	{

		private string requestId;

		private int? totalCount;

		private int? pageNumber;

		private int? pageSize;

		private List<ListClusterOperationHost_ClusterOperationHost> clusterOperationHostList;

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

		public int? PageNumber
		{
			get
			{
				return pageNumber;
			}
			set	
			{
				pageNumber = value;
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
			}
		}

		public List<ListClusterOperationHost_ClusterOperationHost> ClusterOperationHostList
		{
			get
			{
				return clusterOperationHostList;
			}
			set	
			{
				clusterOperationHostList = value;
			}
		}

		public class ListClusterOperationHost_ClusterOperationHost
		{

			private string hostId;

			private string hostName;

			private string status;

			private string percentage;

			public string HostId
			{
				get
				{
					return hostId;
				}
				set	
				{
					hostId = value;
				}
			}

			public string HostName
			{
				get
				{
					return hostName;
				}
				set	
				{
					hostName = value;
				}
			}

			public string Status
			{
				get
				{
					return status;
				}
				set	
				{
					status = value;
				}
			}

			public string Percentage
			{
				get
				{
					return percentage;
				}
				set	
				{
					percentage = value;
				}
			}
		}
	}
}
