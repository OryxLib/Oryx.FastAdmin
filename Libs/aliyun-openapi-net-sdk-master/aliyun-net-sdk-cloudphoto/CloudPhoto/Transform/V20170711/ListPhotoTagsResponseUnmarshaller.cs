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
using Aliyun.Acs.CloudPhoto.Model.V20170711;

namespace Aliyun.Acs.CloudPhoto.Transform.V20170711
{
    public class ListPhotoTagsResponseUnmarshaller
    {
        public static ListPhotoTagsResponse Unmarshall(UnmarshallerContext context)
        {
			ListPhotoTagsResponse listPhotoTagsResponse = new ListPhotoTagsResponse();

			listPhotoTagsResponse.HttpResponse = context.HttpResponse;
			listPhotoTagsResponse.Code = context.StringValue("ListPhotoTags.Code");
			listPhotoTagsResponse.Message = context.StringValue("ListPhotoTags.Message");
			listPhotoTagsResponse.RequestId = context.StringValue("ListPhotoTags.RequestId");
			listPhotoTagsResponse.Action = context.StringValue("ListPhotoTags.Action");

			List<ListPhotoTagsResponse.ListPhotoTags_Tag> listPhotoTagsResponse_tags = new List<ListPhotoTagsResponse.ListPhotoTags_Tag>();
			for (int i = 0; i < context.Length("ListPhotoTags.Tags.Length"); i++) {
				ListPhotoTagsResponse.ListPhotoTags_Tag tag = new ListPhotoTagsResponse.ListPhotoTags_Tag();
				tag.Id = context.LongValue("ListPhotoTags.Tags["+ i +"].Id");
				tag.IdStr = context.StringValue("ListPhotoTags.Tags["+ i +"].IdStr");
				tag.IsSubTag = context.BooleanValue("ListPhotoTags.Tags["+ i +"].IsSubTag");
				tag.Name = context.StringValue("ListPhotoTags.Tags["+ i +"].Name");
				tag.ParentTag = context.StringValue("ListPhotoTags.Tags["+ i +"].ParentTag");

				listPhotoTagsResponse_tags.Add(tag);
			}
			listPhotoTagsResponse.Tags = listPhotoTagsResponse_tags;
        
			return listPhotoTagsResponse;
        }
    }
}
