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
using Aliyun.Acs.LinkFace.Model.V20180720;
using System;
using System.Collections.Generic;

namespace Aliyun.Acs.LinkFace.Transform.V20180720
{
    public class CreateGroupResponseUnmarshaller
    {
        public static CreateGroupResponse Unmarshall(UnmarshallerContext context)
        {
			CreateGroupResponse createGroupResponse = new CreateGroupResponse();

			createGroupResponse.HttpResponse = context.HttpResponse;
			createGroupResponse.RequestId = context.StringValue("CreateGroup.RequestId");
			createGroupResponse.Code = context.IntegerValue("CreateGroup.Code");
			createGroupResponse.Message = context.StringValue("CreateGroup.Message");
			createGroupResponse.Success = context.BooleanValue("CreateGroup.Success");
        
			return createGroupResponse;
        }
    }
}