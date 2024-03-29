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
using Aliyun.Acs.Cms.Model.V20180308;

namespace Aliyun.Acs.Cms.Transform.V20180308
{
    public class QueryTaskMonitorDataResponseUnmarshaller
    {
        public static QueryTaskMonitorDataResponse Unmarshall(UnmarshallerContext context)
        {
			QueryTaskMonitorDataResponse queryTaskMonitorDataResponse = new QueryTaskMonitorDataResponse();

			queryTaskMonitorDataResponse.HttpResponse = context.HttpResponse;
			queryTaskMonitorDataResponse.Code = context.StringValue("QueryTaskMonitorData.Code");
			queryTaskMonitorDataResponse.Message = context.StringValue("QueryTaskMonitorData.Message");
			queryTaskMonitorDataResponse.Success = context.StringValue("QueryTaskMonitorData.Success");
			queryTaskMonitorDataResponse.RequestId = context.StringValue("QueryTaskMonitorData.RequestId");
			queryTaskMonitorDataResponse.TraceId = context.StringValue("QueryTaskMonitorData.TraceId");
			queryTaskMonitorDataResponse.Data = context.StringValue("QueryTaskMonitorData.Data");
			queryTaskMonitorDataResponse.Cursor = context.StringValue("QueryTaskMonitorData.Cursor");
        
			return queryTaskMonitorDataResponse;
        }
    }
}
