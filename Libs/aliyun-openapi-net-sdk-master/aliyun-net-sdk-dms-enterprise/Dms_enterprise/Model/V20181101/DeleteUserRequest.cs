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
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Http;
using Aliyun.Acs.Core.Transform;
using Aliyun.Acs.Core.Utils;
using Aliyun.Acs.dms_enterprise.Transform;
using Aliyun.Acs.dms_enterprise.Transform.V20181101;
using System.Collections.Generic;

namespace Aliyun.Acs.dms_enterprise.Model.V20181101
{
    public class DeleteUserRequest : RpcAcsRequest<DeleteUserResponse>
    {
        public DeleteUserRequest()
            : base("dms_enterprise", "2018-11-01", "DeleteUser", "dmsenterprise", "openAPI")
        {
        }

		private long? uid;

		private long? tid;

		public long? Uid
		{
			get
			{
				return uid;
			}
			set	
			{
				uid = value;
				DictionaryUtil.Add(QueryParameters, "Uid", value.ToString());
			}
		}

		public long? Tid
		{
			get
			{
				return tid;
			}
			set	
			{
				tid = value;
				DictionaryUtil.Add(QueryParameters, "Tid", value.ToString());
			}
		}

        public override DeleteUserResponse GetResponse(Core.Transform.UnmarshallerContext unmarshallerContext)
        {
            return DeleteUserResponseUnmarshaller.Unmarshall(unmarshallerContext);
        }
    }
}