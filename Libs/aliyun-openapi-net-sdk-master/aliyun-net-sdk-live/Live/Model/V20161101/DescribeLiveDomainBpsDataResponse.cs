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

namespace Aliyun.Acs.live.Model.V20161101
{
	public class DescribeLiveDomainBpsDataResponse : AcsResponse
	{

		private string requestId;

		private string domainName;

		private string startTime;

		private string endTime;

		private string dataInterval;

		private List<DescribeLiveDomainBpsData_DataModule> bpsDataPerInterval;

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

		public string DomainName
		{
			get
			{
				return domainName;
			}
			set	
			{
				domainName = value;
			}
		}

		public string StartTime
		{
			get
			{
				return startTime;
			}
			set	
			{
				startTime = value;
			}
		}

		public string EndTime
		{
			get
			{
				return endTime;
			}
			set	
			{
				endTime = value;
			}
		}

		public string DataInterval
		{
			get
			{
				return dataInterval;
			}
			set	
			{
				dataInterval = value;
			}
		}

		public List<DescribeLiveDomainBpsData_DataModule> BpsDataPerInterval
		{
			get
			{
				return bpsDataPerInterval;
			}
			set	
			{
				bpsDataPerInterval = value;
			}
		}

		public class DescribeLiveDomainBpsData_DataModule
		{

			private string timeStamp;

			private string bpsValue;

			private string httpBpsValue;

			private string httpsBpsValue;

			public string TimeStamp
			{
				get
				{
					return timeStamp;
				}
				set	
				{
					timeStamp = value;
				}
			}

			public string BpsValue
			{
				get
				{
					return bpsValue;
				}
				set	
				{
					bpsValue = value;
				}
			}

			public string HttpBpsValue
			{
				get
				{
					return httpBpsValue;
				}
				set	
				{
					httpBpsValue = value;
				}
			}

			public string HttpsBpsValue
			{
				get
				{
					return httpsBpsValue;
				}
				set	
				{
					httpsBpsValue = value;
				}
			}
		}
	}
}
