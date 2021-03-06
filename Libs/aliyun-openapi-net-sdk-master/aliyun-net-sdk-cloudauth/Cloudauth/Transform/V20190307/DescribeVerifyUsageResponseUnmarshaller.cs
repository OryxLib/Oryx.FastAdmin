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
using Aliyun.Acs.Cloudauth.Model.V20190307;

namespace Aliyun.Acs.Cloudauth.Transform.V20190307
{
    public class DescribeVerifyUsageResponseUnmarshaller
    {
        public static DescribeVerifyUsageResponse Unmarshall(UnmarshallerContext context)
        {
			DescribeVerifyUsageResponse describeVerifyUsageResponse = new DescribeVerifyUsageResponse();

			describeVerifyUsageResponse.HttpResponse = context.HttpResponse;
			describeVerifyUsageResponse.RequestId = context.StringValue("DescribeVerifyUsage.RequestId");
			describeVerifyUsageResponse.TotalCount = context.IntegerValue("DescribeVerifyUsage.TotalCount");

			List<DescribeVerifyUsageResponse.DescribeVerifyUsage_VerifyUsage> describeVerifyUsageResponse_verifyUsageList = new List<DescribeVerifyUsageResponse.DescribeVerifyUsage_VerifyUsage>();
			for (int i = 0; i < context.Length("DescribeVerifyUsage.VerifyUsageList.Length"); i++) {
				DescribeVerifyUsageResponse.DescribeVerifyUsage_VerifyUsage verifyUsage = new DescribeVerifyUsageResponse.DescribeVerifyUsage_VerifyUsage();
				verifyUsage.BizType = context.StringValue("DescribeVerifyUsage.VerifyUsageList["+ i +"].BizType");
				verifyUsage.Date = context.StringValue("DescribeVerifyUsage.VerifyUsageList["+ i +"].Date");
				verifyUsage.TotalCount = context.LongValue("DescribeVerifyUsage.VerifyUsageList["+ i +"].TotalCount");
				verifyUsage.PassCount = context.LongValue("DescribeVerifyUsage.VerifyUsageList["+ i +"].PassCount");
				verifyUsage.FailCount = context.LongValue("DescribeVerifyUsage.VerifyUsageList["+ i +"].FailCount");

				describeVerifyUsageResponse_verifyUsageList.Add(verifyUsage);
			}
			describeVerifyUsageResponse.VerifyUsageList = describeVerifyUsageResponse_verifyUsageList;
        
			return describeVerifyUsageResponse;
        }
    }
}
