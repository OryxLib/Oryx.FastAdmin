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
using Aliyun.Acs.CCC.Model.V20170705;

namespace Aliyun.Acs.CCC.Transform.V20170705
{
    public class GetRecordOssUploadParamResponseUnmarshaller
    {
        public static GetRecordOssUploadParamResponse Unmarshall(UnmarshallerContext context)
        {
			GetRecordOssUploadParamResponse getRecordOssUploadParamResponse = new GetRecordOssUploadParamResponse();

			getRecordOssUploadParamResponse.HttpResponse = context.HttpResponse;
			getRecordOssUploadParamResponse.RequestId = context.StringValue("GetRecordOssUploadParam.RequestId");
			getRecordOssUploadParamResponse.Success = context.BooleanValue("GetRecordOssUploadParam.Success");
			getRecordOssUploadParamResponse.Code = context.StringValue("GetRecordOssUploadParam.Code");
			getRecordOssUploadParamResponse.Message = context.StringValue("GetRecordOssUploadParam.Message");
			getRecordOssUploadParamResponse.HttpStatusCode = context.IntegerValue("GetRecordOssUploadParam.HttpStatusCode");
			getRecordOssUploadParamResponse.OssAccessKeyId = context.StringValue("GetRecordOssUploadParam.OssAccessKeyId");
			getRecordOssUploadParamResponse.Policy = context.StringValue("GetRecordOssUploadParam.Policy");
			getRecordOssUploadParamResponse.Signature = context.StringValue("GetRecordOssUploadParam.Signature");
			getRecordOssUploadParamResponse.Expires = context.StringValue("GetRecordOssUploadParam.Expires");
			getRecordOssUploadParamResponse.Dir = context.StringValue("GetRecordOssUploadParam.Dir");
			getRecordOssUploadParamResponse.Host = context.StringValue("GetRecordOssUploadParam.Host");
			getRecordOssUploadParamResponse.OssFileName = context.StringValue("GetRecordOssUploadParam.OssFileName");
        
			return getRecordOssUploadParamResponse;
        }
    }
}
