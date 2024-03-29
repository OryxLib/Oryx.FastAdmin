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
	public class ListAlertDingDingGroupResponse : AcsResponse
	{

		private List<ListAlertDingDingGroup_AlertDingDingGroup> alertDingDingGroupList;

		public List<ListAlertDingDingGroup_AlertDingDingGroup> AlertDingDingGroupList
		{
			get
			{
				return alertDingDingGroupList;
			}
			set	
			{
				alertDingDingGroupList = value;
			}
		}

		public class ListAlertDingDingGroup_AlertDingDingGroup
		{

			private long? id;

			private string gmtCreate;

			private string gmtModified;

			private string bizId;

			private string name;

			private string description;

			private string webHookUrl;

			public long? Id
			{
				get
				{
					return id;
				}
				set	
				{
					id = value;
				}
			}

			public string GmtCreate
			{
				get
				{
					return gmtCreate;
				}
				set	
				{
					gmtCreate = value;
				}
			}

			public string GmtModified
			{
				get
				{
					return gmtModified;
				}
				set	
				{
					gmtModified = value;
				}
			}

			public string BizId
			{
				get
				{
					return bizId;
				}
				set	
				{
					bizId = value;
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

			public string WebHookUrl
			{
				get
				{
					return webHookUrl;
				}
				set	
				{
					webHookUrl = value;
				}
			}
		}
	}
}
