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
using Aliyun.Acs.ivpd.Model.V20190625;

namespace Aliyun.Acs.ivpd.Transform.V20190625
{
    public class ChangeImageSizeResponseUnmarshaller
    {
        public static ChangeImageSizeResponse Unmarshall(UnmarshallerContext context)
        {
			ChangeImageSizeResponse changeImageSizeResponse = new ChangeImageSizeResponse();

			changeImageSizeResponse.HttpResponse = context.HttpResponse;
			changeImageSizeResponse.RequestId = context.StringValue("ChangeImageSize.RequestId");
			changeImageSizeResponse.Code = context.StringValue("ChangeImageSize.Code");
			changeImageSizeResponse.Message = context.StringValue("ChangeImageSize.Message");

			ChangeImageSizeResponse.ChangeImageSize_Data data = new ChangeImageSizeResponse.ChangeImageSize_Data();
			data.Url = context.StringValue("ChangeImageSize.Data.Url");
			changeImageSizeResponse.Data = data;
        
			return changeImageSizeResponse;
        }
    }
}
