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
using System;
using System.Collections.Generic;

using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Sas.Model.V20181203;

namespace Aliyun.Acs.Sas.Transform.V20181203
{
    public class DescribeSecurityCheckScheduleConfigResponseUnmarshaller
    {
        public static DescribeSecurityCheckScheduleConfigResponse Unmarshall(UnmarshallerContext context)
        {
			DescribeSecurityCheckScheduleConfigResponse describeSecurityCheckScheduleConfigResponse = new DescribeSecurityCheckScheduleConfigResponse();

			describeSecurityCheckScheduleConfigResponse.HttpResponse = context.HttpResponse;
			describeSecurityCheckScheduleConfigResponse.RequestId = context.StringValue("DescribeSecurityCheckScheduleConfig.RequestId");

			DescribeSecurityCheckScheduleConfigResponse.DescribeSecurityCheckScheduleConfig_RiskCheckJobConfig riskCheckJobConfig = new DescribeSecurityCheckScheduleConfigResponse.DescribeSecurityCheckScheduleConfig_RiskCheckJobConfig();
			riskCheckJobConfig.StartTime = context.IntegerValue("DescribeSecurityCheckScheduleConfig.RiskCheckJobConfig.StartTime");
			riskCheckJobConfig.EndTime = context.IntegerValue("DescribeSecurityCheckScheduleConfig.RiskCheckJobConfig.EndTime");
			riskCheckJobConfig.DaysOfWeek = context.StringValue("DescribeSecurityCheckScheduleConfig.RiskCheckJobConfig.DaysOfWeek");
			describeSecurityCheckScheduleConfigResponse.RiskCheckJobConfig = riskCheckJobConfig;
        
			return describeSecurityCheckScheduleConfigResponse;
        }
    }
}
