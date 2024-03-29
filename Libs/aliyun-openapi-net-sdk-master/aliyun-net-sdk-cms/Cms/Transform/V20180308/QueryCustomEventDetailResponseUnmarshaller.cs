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
    public class QueryCustomEventDetailResponseUnmarshaller
    {
        public static QueryCustomEventDetailResponse Unmarshall(UnmarshallerContext context)
        {
			QueryCustomEventDetailResponse queryCustomEventDetailResponse = new QueryCustomEventDetailResponse();

			queryCustomEventDetailResponse.HttpResponse = context.HttpResponse;
			queryCustomEventDetailResponse.Code = context.StringValue("QueryCustomEventDetail.Code");
			queryCustomEventDetailResponse.Message = context.StringValue("QueryCustomEventDetail.Message");
			queryCustomEventDetailResponse.Data = context.StringValue("QueryCustomEventDetail.Data");
			queryCustomEventDetailResponse.RequestId = context.StringValue("QueryCustomEventDetail.RequestId");
			queryCustomEventDetailResponse.Success = context.StringValue("QueryCustomEventDetail.Success");
        
			return queryCustomEventDetailResponse;
        }
    }
}
