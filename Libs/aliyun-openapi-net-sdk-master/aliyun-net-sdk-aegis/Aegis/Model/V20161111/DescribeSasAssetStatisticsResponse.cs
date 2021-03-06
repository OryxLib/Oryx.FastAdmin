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

namespace Aliyun.Acs.aegis.Model.V20161111
{
	public class DescribeSasAssetStatisticsResponse : AcsResponse
	{

		private string requestId;

		private int? pageSize;

		private int? currentPage;

		private int? totalCount;

		private List<DescribeSasAssetStatistics_Asset> assetList;

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

		public int? CurrentPage
		{
			get
			{
				return currentPage;
			}
			set	
			{
				currentPage = value;
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

		public List<DescribeSasAssetStatistics_Asset> AssetList
		{
			get
			{
				return assetList;
			}
			set	
			{
				assetList = value;
			}
		}

		public class DescribeSasAssetStatistics_Asset
		{

			private int? healthCheckCount;

			private int? vulCount;

			private int? safeEventCount;

			private string uuid;

			public int? HealthCheckCount
			{
				get
				{
					return healthCheckCount;
				}
				set	
				{
					healthCheckCount = value;
				}
			}

			public int? VulCount
			{
				get
				{
					return vulCount;
				}
				set	
				{
					vulCount = value;
				}
			}

			public int? SafeEventCount
			{
				get
				{
					return safeEventCount;
				}
				set	
				{
					safeEventCount = value;
				}
			}

			public string Uuid
			{
				get
				{
					return uuid;
				}
				set	
				{
					uuid = value;
				}
			}
		}
	}
}
