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
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Baas.Model.V20180731;
using System;
using System.Collections.Generic;

namespace Aliyun.Acs.Baas.Transform.V20180731
{
    public class DownloadOrganizationSDKResponseUnmarshaller
    {
        public static DownloadOrganizationSDKResponse Unmarshall(UnmarshallerContext context)
        {
			DownloadOrganizationSDKResponse downloadOrganizationSDKResponse = new DownloadOrganizationSDKResponse();

			downloadOrganizationSDKResponse.HttpResponse = context.HttpResponse;
			downloadOrganizationSDKResponse.RequestId = context.StringValue("DownloadOrganizationSDK.RequestId");
			downloadOrganizationSDKResponse.Success = context.BooleanValue("DownloadOrganizationSDK.Success");
			downloadOrganizationSDKResponse.ErrorCode = context.IntegerValue("DownloadOrganizationSDK.ErrorCode");

			List<DownloadOrganizationSDKResponse.DownloadOrganizationSDK_ResultItem> downloadOrganizationSDKResponse_result = new List<DownloadOrganizationSDKResponse.DownloadOrganizationSDK_ResultItem>();
			for (int i = 0; i < context.Length("DownloadOrganizationSDK.Result.Length"); i++) {
				DownloadOrganizationSDKResponse.DownloadOrganizationSDK_ResultItem resultItem = new DownloadOrganizationSDKResponse.DownloadOrganizationSDK_ResultItem();
				resultItem.Content = context.StringValue("DownloadOrganizationSDK.Result["+ i +"].Content");
				resultItem.Path = context.StringValue("DownloadOrganizationSDK.Result["+ i +"].Path");

				downloadOrganizationSDKResponse_result.Add(resultItem);
			}
			downloadOrganizationSDKResponse.Result = downloadOrganizationSDKResponse_result;
        
			return downloadOrganizationSDKResponse;
        }
    }
}