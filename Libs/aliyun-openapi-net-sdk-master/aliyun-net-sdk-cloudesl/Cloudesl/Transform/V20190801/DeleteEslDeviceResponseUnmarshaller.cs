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
using Aliyun.Acs.cloudesl.Model.V20190801;

namespace Aliyun.Acs.cloudesl.Transform.V20190801
{
    public class DeleteEslDeviceResponseUnmarshaller
    {
        public static DeleteEslDeviceResponse Unmarshall(UnmarshallerContext context)
        {
			DeleteEslDeviceResponse deleteEslDeviceResponse = new DeleteEslDeviceResponse();

			deleteEslDeviceResponse.HttpResponse = context.HttpResponse;
			deleteEslDeviceResponse.ErrorMessage = context.StringValue("DeleteEslDevice.ErrorMessage");
			deleteEslDeviceResponse.ErrorCode = context.StringValue("DeleteEslDevice.ErrorCode");
			deleteEslDeviceResponse.Message = context.StringValue("DeleteEslDevice.Message");
			deleteEslDeviceResponse.DynamicCode = context.StringValue("DeleteEslDevice.DynamicCode");
			deleteEslDeviceResponse.Code = context.StringValue("DeleteEslDevice.Code");
			deleteEslDeviceResponse.DynamicMessage = context.StringValue("DeleteEslDevice.DynamicMessage");
			deleteEslDeviceResponse.RequestId = context.StringValue("DeleteEslDevice.RequestId");
			deleteEslDeviceResponse.Success = context.BooleanValue("DeleteEslDevice.Success");
        
			return deleteEslDeviceResponse;
        }
    }
}
