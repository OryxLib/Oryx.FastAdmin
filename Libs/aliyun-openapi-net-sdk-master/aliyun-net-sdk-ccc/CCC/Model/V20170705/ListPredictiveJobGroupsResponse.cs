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

namespace Aliyun.Acs.CCC.Model.V20170705
{
	public class ListPredictiveJobGroupsResponse : AcsResponse
	{

		private string requestId;

		private bool? success;

		private string code;

		private string message;

		private int? httpStatusCode;

		private ListPredictiveJobGroups_JobGroups jobGroups;

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

		public bool? Success
		{
			get
			{
				return success;
			}
			set	
			{
				success = value;
			}
		}

		public string Code
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

		public string Message
		{
			get
			{
				return message;
			}
			set	
			{
				message = value;
			}
		}

		public int? HttpStatusCode
		{
			get
			{
				return httpStatusCode;
			}
			set	
			{
				httpStatusCode = value;
			}
		}

		public ListPredictiveJobGroups_JobGroups JobGroups
		{
			get
			{
				return jobGroups;
			}
			set	
			{
				jobGroups = value;
			}
		}

		public class ListPredictiveJobGroups_JobGroups
		{

			private int? totalCount;

			private int? pageNumber;

			private int? pageSize;

			private List<ListPredictiveJobGroups_JobGroup> list;

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

			public List<ListPredictiveJobGroups_JobGroup> List
			{
				get
				{
					return list;
				}
				set	
				{
					list = value;
				}
			}

			public class ListPredictiveJobGroups_JobGroup
			{

				private string jobGroupId;

				private string instanceId;

				private string skillGroupId;

				private string skillGroupName;

				private string taskType;

				private string occupancyRate;

				private string startTime;

				private string endTime;

				private string name;

				private string description;

				private long? creationTime;

				private ListPredictiveJobGroups_Strategy strategy;

				private ListPredictiveJobGroups_Progress progress;

				public string JobGroupId
				{
					get
					{
						return jobGroupId;
					}
					set	
					{
						jobGroupId = value;
					}
				}

				public string InstanceId
				{
					get
					{
						return instanceId;
					}
					set	
					{
						instanceId = value;
					}
				}

				public string SkillGroupId
				{
					get
					{
						return skillGroupId;
					}
					set	
					{
						skillGroupId = value;
					}
				}

				public string SkillGroupName
				{
					get
					{
						return skillGroupName;
					}
					set	
					{
						skillGroupName = value;
					}
				}

				public string TaskType
				{
					get
					{
						return taskType;
					}
					set	
					{
						taskType = value;
					}
				}

				public string OccupancyRate
				{
					get
					{
						return occupancyRate;
					}
					set	
					{
						occupancyRate = value;
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

				public long? CreationTime
				{
					get
					{
						return creationTime;
					}
					set	
					{
						creationTime = value;
					}
				}

				public ListPredictiveJobGroups_Strategy Strategy
				{
					get
					{
						return strategy;
					}
					set	
					{
						strategy = value;
					}
				}

				public ListPredictiveJobGroups_Progress Progress
				{
					get
					{
						return progress;
					}
					set	
					{
						progress = value;
					}
				}

				public class ListPredictiveJobGroups_Strategy
				{

					private string strategyId;

					private long? startTime;

					private long? endTime;

					private int? maxAttemptsPerDay;

					private int? minAttemptInterval;

					private List<ListPredictiveJobGroups_TimeFrame> workingTime;

					public string StrategyId
					{
						get
						{
							return strategyId;
						}
						set	
						{
							strategyId = value;
						}
					}

					public long? StartTime
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

					public long? EndTime
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

					public int? MaxAttemptsPerDay
					{
						get
						{
							return maxAttemptsPerDay;
						}
						set	
						{
							maxAttemptsPerDay = value;
						}
					}

					public int? MinAttemptInterval
					{
						get
						{
							return minAttemptInterval;
						}
						set	
						{
							minAttemptInterval = value;
						}
					}

					public List<ListPredictiveJobGroups_TimeFrame> WorkingTime
					{
						get
						{
							return workingTime;
						}
						set	
						{
							workingTime = value;
						}
					}

					public class ListPredictiveJobGroups_TimeFrame
					{

						private string beginTime;

						private string endTime;

						public string BeginTime
						{
							get
							{
								return beginTime;
							}
							set	
							{
								beginTime = value;
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
					}
				}

				public class ListPredictiveJobGroups_Progress
				{

					private int? totalJobs;

					private string status;

					private int? totalNotAnswered;

					private int? totalCompleted;

					private long? startTime;

					private int? duration;

					private List<ListPredictiveJobGroups_KeyValuePair> categories;

					public int? TotalJobs
					{
						get
						{
							return totalJobs;
						}
						set	
						{
							totalJobs = value;
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

					public int? TotalNotAnswered
					{
						get
						{
							return totalNotAnswered;
						}
						set	
						{
							totalNotAnswered = value;
						}
					}

					public int? TotalCompleted
					{
						get
						{
							return totalCompleted;
						}
						set	
						{
							totalCompleted = value;
						}
					}

					public long? StartTime
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

					public int? Duration
					{
						get
						{
							return duration;
						}
						set	
						{
							duration = value;
						}
					}

					public List<ListPredictiveJobGroups_KeyValuePair> Categories
					{
						get
						{
							return categories;
						}
						set	
						{
							categories = value;
						}
					}

					public class ListPredictiveJobGroups_KeyValuePair
					{

						private string key;

						private string _value;

						public string Key
						{
							get
							{
								return key;
							}
							set	
							{
								key = value;
							}
						}

						public string _Value
						{
							get
							{
								return _value;
							}
							set	
							{
								_value = value;
							}
						}
					}
				}
			}
		}
	}
}
