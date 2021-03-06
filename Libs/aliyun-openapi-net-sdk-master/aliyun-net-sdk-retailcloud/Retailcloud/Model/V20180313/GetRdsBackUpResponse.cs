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

namespace Aliyun.Acs.retailcloud.Model.V20180313
{
	public class GetRdsBackUpResponse : AcsResponse
	{

		private int? code;

		private string errMsg;

		private string requestId;

		private GetRdsBackUp_Result result;

		public int? Code
		{
			get
			{
				return code;
			}
			set	
			{
				code = value;
			}
		}

		public string ErrMsg
		{
			get
			{
				return errMsg;
			}
			set	
			{
				errMsg = value;
			}
		}

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

		public GetRdsBackUp_Result Result
		{
			get
			{
				return result;
			}
			set	
			{
				result = value;
			}
		}

		public class GetRdsBackUp_Result
		{

			private string totalRecordCount;

			private string pageNumber;

			private string pageRecordCount;

			private long? totalBackupSize;

			private List<GetRdsBackUp_Backup> items;

			public string TotalRecordCount
			{
				get
				{
					return totalRecordCount;
				}
				set	
				{
					totalRecordCount = value;
				}
			}

			public string PageNumber
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

			public string PageRecordCount
			{
				get
				{
					return pageRecordCount;
				}
				set	
				{
					pageRecordCount = value;
				}
			}

			public long? TotalBackupSize
			{
				get
				{
					return totalBackupSize;
				}
				set	
				{
					totalBackupSize = value;
				}
			}

			public List<GetRdsBackUp_Backup> Items
			{
				get
				{
					return items;
				}
				set	
				{
					items = value;
				}
			}

			public class GetRdsBackUp_Backup
			{

				private string backupId;

				private string dBInstanceId;

				private string backupStatus;

				private string backupStartTime;

				private string backupEndTime;

				private string backupType;

				private string backupMode;

				private string backupMethod;

				private string backupLocation;

				private string backupExtractionStatus;

				private string backupScale;

				private string backupDBNames;

				private long? totalBackupSize;

				private long? backupSize;

				private string hostInstanceID;

				private string storeStatus;

				private string metaStatus;

				public string BackupId
				{
					get
					{
						return backupId;
					}
					set	
					{
						backupId = value;
					}
				}

				public string DBInstanceId
				{
					get
					{
						return dBInstanceId;
					}
					set	
					{
						dBInstanceId = value;
					}
				}

				public string BackupStatus
				{
					get
					{
						return backupStatus;
					}
					set	
					{
						backupStatus = value;
					}
				}

				public string BackupStartTime
				{
					get
					{
						return backupStartTime;
					}
					set	
					{
						backupStartTime = value;
					}
				}

				public string BackupEndTime
				{
					get
					{
						return backupEndTime;
					}
					set	
					{
						backupEndTime = value;
					}
				}

				public string BackupType
				{
					get
					{
						return backupType;
					}
					set	
					{
						backupType = value;
					}
				}

				public string BackupMode
				{
					get
					{
						return backupMode;
					}
					set	
					{
						backupMode = value;
					}
				}

				public string BackupMethod
				{
					get
					{
						return backupMethod;
					}
					set	
					{
						backupMethod = value;
					}
				}

				public string BackupLocation
				{
					get
					{
						return backupLocation;
					}
					set	
					{
						backupLocation = value;
					}
				}

				public string BackupExtractionStatus
				{
					get
					{
						return backupExtractionStatus;
					}
					set	
					{
						backupExtractionStatus = value;
					}
				}

				public string BackupScale
				{
					get
					{
						return backupScale;
					}
					set	
					{
						backupScale = value;
					}
				}

				public string BackupDBNames
				{
					get
					{
						return backupDBNames;
					}
					set	
					{
						backupDBNames = value;
					}
				}

				public long? TotalBackupSize
				{
					get
					{
						return totalBackupSize;
					}
					set	
					{
						totalBackupSize = value;
					}
				}

				public long? BackupSize
				{
					get
					{
						return backupSize;
					}
					set	
					{
						backupSize = value;
					}
				}

				public string HostInstanceID
				{
					get
					{
						return hostInstanceID;
					}
					set	
					{
						hostInstanceID = value;
					}
				}

				public string StoreStatus
				{
					get
					{
						return storeStatus;
					}
					set	
					{
						storeStatus = value;
					}
				}

				public string MetaStatus
				{
					get
					{
						return metaStatus;
					}
					set	
					{
						metaStatus = value;
					}
				}
			}
		}
	}
}
