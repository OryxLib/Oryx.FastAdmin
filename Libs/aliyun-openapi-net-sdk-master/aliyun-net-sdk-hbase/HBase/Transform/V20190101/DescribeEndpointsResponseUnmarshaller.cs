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
using Aliyun.Acs.HBase.Model.V20190101;

namespace Aliyun.Acs.HBase.Transform.V20190101
{
    public class DescribeEndpointsResponseUnmarshaller
    {
        public static DescribeEndpointsResponse Unmarshall(UnmarshallerContext context)
        {
			DescribeEndpointsResponse describeEndpointsResponse = new DescribeEndpointsResponse();

			describeEndpointsResponse.HttpResponse = context.HttpResponse;
			describeEndpointsResponse.RequestId = context.StringValue("DescribeEndpoints.RequestId");
			describeEndpointsResponse.NetType = context.StringValue("DescribeEndpoints.NetType");
			describeEndpointsResponse.VpcId = context.StringValue("DescribeEndpoints.VpcId");
			describeEndpointsResponse.VSwitchId = context.StringValue("DescribeEndpoints.VSwitchId");
			describeEndpointsResponse.Engine = context.StringValue("DescribeEndpoints.Engine");

			List<DescribeEndpointsResponse.DescribeEndpoints_ConnAddrInfo> describeEndpointsResponse_connAddrs = new List<DescribeEndpointsResponse.DescribeEndpoints_ConnAddrInfo>();
			for (int i = 0; i < context.Length("DescribeEndpoints.ConnAddrs.Length"); i++) {
				DescribeEndpointsResponse.DescribeEndpoints_ConnAddrInfo connAddrInfo = new DescribeEndpointsResponse.DescribeEndpoints_ConnAddrInfo();
				connAddrInfo.ConnType = context.StringValue("DescribeEndpoints.ConnAddrs["+ i +"].ConnType");
				connAddrInfo.ConnAddr = context.StringValue("DescribeEndpoints.ConnAddrs["+ i +"].ConnAddr");
				connAddrInfo.ConnAddrPort = context.StringValue("DescribeEndpoints.ConnAddrs["+ i +"].ConnAddrPort");
				connAddrInfo.NetType = context.StringValue("DescribeEndpoints.ConnAddrs["+ i +"].NetType");

				describeEndpointsResponse_connAddrs.Add(connAddrInfo);
			}
			describeEndpointsResponse.ConnAddrs = describeEndpointsResponse_connAddrs;
        
			return describeEndpointsResponse;
        }
    }
}
