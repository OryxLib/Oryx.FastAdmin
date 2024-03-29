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
using System.Collections.Generic;

using Aliyun.Acs.Core;

namespace Aliyun.Acs.imm.Model.V20170906
{
	public class FindImagesByTagNamesResponse : AcsResponse
	{

		private string setId;

		private string nextMarker;

		private string requestId;

		private List<FindImagesByTagNames_Frames> images;

		public string SetId
		{
			get
			{
				return setId;
			}
			set	
			{
				setId = value;
			}
		}

		public string NextMarker
		{
			get
			{
				return nextMarker;
			}
			set	
			{
				nextMarker = value;
			}
		}

		public string RequestId
		{
			get
			{
				return requestId;
			}
			set	
			{
				requestId = value;
			}
		}

		public List<FindImagesByTagNames_Frames> Images
		{
			get
			{
				return images;
			}
			set	
			{
				images = value;
			}
		}

		public class FindImagesByTagNames_Frames
		{

			private string facesModifyTime;

			private string oCRModifyTime;

			private string oCRStatus;

			private string sourcePosition;

			private string exif;

			private string imageUri;

			private int? imageWidth;

			private string imageFormat;

			private string sourceType;

			private string modifyTime;

			private int? fileSize;

			private string sourceUri;

			private string createTime;

			private string facesStatus;

			private string remarksA;

			private int? imageHeight;

			private string remarksB;

			private string imageTime;

			private string orientation;

			private string location;

			private string oCRFailReason;

			private string facesFailReason;

			private string tagsFailReason;

			private string tagsModifyTime;

			private string celebrityStatus;

			private string celebrityModifyTime;

			private string celebrityFailReason;

			private string tagsStatus;

			private string externalId;

			private string remarksC;

			private string remarksD;

			private string addressModifyTime;

			private string addressStatus;

			private string addressFailReason;

			private List<FindImagesByTagNames_FacesItem> faces;

			private List<FindImagesByTagNames_TagsItem> tags;

			private List<FindImagesByTagNames_OCRItem> oCR;

			private List<FindImagesByTagNames_CelebrityItem> celebrity;

			private FindImagesByTagNames_Address address;

			public string FacesModifyTime
			{
				get
				{
					return facesModifyTime;
				}
				set	
				{
					facesModifyTime = value;
				}
			}

			public string OCRModifyTime
			{
				get
				{
					return oCRModifyTime;
				}
				set	
				{
					oCRModifyTime = value;
				}
			}

			public string OCRStatus
			{
				get
				{
					return oCRStatus;
				}
				set	
				{
					oCRStatus = value;
				}
			}

			public string SourcePosition
			{
				get
				{
					return sourcePosition;
				}
				set	
				{
					sourcePosition = value;
				}
			}

			public string Exif
			{
				get
				{
					return exif;
				}
				set	
				{
					exif = value;
				}
			}

			public string ImageUri
			{
				get
				{
					return imageUri;
				}
				set	
				{
					imageUri = value;
				}
			}

			public int? ImageWidth
			{
				get
				{
					return imageWidth;
				}
				set	
				{
					imageWidth = value;
				}
			}

			public string ImageFormat
			{
				get
				{
					return imageFormat;
				}
				set	
				{
					imageFormat = value;
				}
			}

			public string SourceType
			{
				get
				{
					return sourceType;
				}
				set	
				{
					sourceType = value;
				}
			}

			public string ModifyTime
			{
				get
				{
					return modifyTime;
				}
				set	
				{
					modifyTime = value;
				}
			}

			public int? FileSize
			{
				get
				{
					return fileSize;
				}
				set	
				{
					fileSize = value;
				}
			}

			public string SourceUri
			{
				get
				{
					return sourceUri;
				}
				set	
				{
					sourceUri = value;
				}
			}

			public string CreateTime
			{
				get
				{
					return createTime;
				}
				set	
				{
					createTime = value;
				}
			}

			public string FacesStatus
			{
				get
				{
					return facesStatus;
				}
				set	
				{
					facesStatus = value;
				}
			}

			public string RemarksA
			{
				get
				{
					return remarksA;
				}
				set	
				{
					remarksA = value;
				}
			}

			public int? ImageHeight
			{
				get
				{
					return imageHeight;
				}
				set	
				{
					imageHeight = value;
				}
			}

			public string RemarksB
			{
				get
				{
					return remarksB;
				}
				set	
				{
					remarksB = value;
				}
			}

			public string ImageTime
			{
				get
				{
					return imageTime;
				}
				set	
				{
					imageTime = value;
				}
			}

			public string Orientation
			{
				get
				{
					return orientation;
				}
				set	
				{
					orientation = value;
				}
			}

			public string Location
			{
				get
				{
					return location;
				}
				set	
				{
					location = value;
				}
			}

			public string OCRFailReason
			{
				get
				{
					return oCRFailReason;
				}
				set	
				{
					oCRFailReason = value;
				}
			}

			public string FacesFailReason
			{
				get
				{
					return facesFailReason;
				}
				set	
				{
					facesFailReason = value;
				}
			}

			public string TagsFailReason
			{
				get
				{
					return tagsFailReason;
				}
				set	
				{
					tagsFailReason = value;
				}
			}

			public string TagsModifyTime
			{
				get
				{
					return tagsModifyTime;
				}
				set	
				{
					tagsModifyTime = value;
				}
			}

			public string CelebrityStatus
			{
				get
				{
					return celebrityStatus;
				}
				set	
				{
					celebrityStatus = value;
				}
			}

			public string CelebrityModifyTime
			{
				get
				{
					return celebrityModifyTime;
				}
				set	
				{
					celebrityModifyTime = value;
				}
			}

			public string CelebrityFailReason
			{
				get
				{
					return celebrityFailReason;
				}
				set	
				{
					celebrityFailReason = value;
				}
			}

			public string TagsStatus
			{
				get
				{
					return tagsStatus;
				}
				set	
				{
					tagsStatus = value;
				}
			}

			public string ExternalId
			{
				get
				{
					return externalId;
				}
				set	
				{
					externalId = value;
				}
			}

			public string RemarksC
			{
				get
				{
					return remarksC;
				}
				set	
				{
					remarksC = value;
				}
			}

			public string RemarksD
			{
				get
				{
					return remarksD;
				}
				set	
				{
					remarksD = value;
				}
			}

			public string AddressModifyTime
			{
				get
				{
					return addressModifyTime;
				}
				set	
				{
					addressModifyTime = value;
				}
			}

			public string AddressStatus
			{
				get
				{
					return addressStatus;
				}
				set	
				{
					addressStatus = value;
				}
			}

			public string AddressFailReason
			{
				get
				{
					return addressFailReason;
				}
				set	
				{
					addressFailReason = value;
				}
			}

			public List<FindImagesByTagNames_FacesItem> Faces
			{
				get
				{
					return faces;
				}
				set	
				{
					faces = value;
				}
			}

			public List<FindImagesByTagNames_TagsItem> Tags
			{
				get
				{
					return tags;
				}
				set	
				{
					tags = value;
				}
			}

			public List<FindImagesByTagNames_OCRItem> OCR
			{
				get
				{
					return oCR;
				}
				set	
				{
					oCR = value;
				}
			}

			public List<FindImagesByTagNames_CelebrityItem> Celebrity
			{
				get
				{
					return celebrity;
				}
				set	
				{
					celebrity = value;
				}
			}

			public FindImagesByTagNames_Address Address
			{
				get
				{
					return address;
				}
				set	
				{
					address = value;
				}
			}

			public class FindImagesByTagNames_FacesItem
			{

				private int? age;

				private float? genderConfidence;

				private float? attractive;

				private string gender;

				private float? faceConfidence;

				private string emotion;

				private string faceId;

				private float? emotionConfidence;

				private string groupId;

				private float? faceQuality;

				private FindImagesByTagNames_EmotionDetails emotionDetails;

				private FindImagesByTagNames_FaceAttributes faceAttributes;

				public int? Age
				{
					get
					{
						return age;
					}
					set	
					{
						age = value;
					}
				}

				public float? GenderConfidence
				{
					get
					{
						return genderConfidence;
					}
					set	
					{
						genderConfidence = value;
					}
				}

				public float? Attractive
				{
					get
					{
						return attractive;
					}
					set	
					{
						attractive = value;
					}
				}

				public string Gender
				{
					get
					{
						return gender;
					}
					set	
					{
						gender = value;
					}
				}

				public float? FaceConfidence
				{
					get
					{
						return faceConfidence;
					}
					set	
					{
						faceConfidence = value;
					}
				}

				public string Emotion
				{
					get
					{
						return emotion;
					}
					set	
					{
						emotion = value;
					}
				}

				public string FaceId
				{
					get
					{
						return faceId;
					}
					set	
					{
						faceId = value;
					}
				}

				public float? EmotionConfidence
				{
					get
					{
						return emotionConfidence;
					}
					set	
					{
						emotionConfidence = value;
					}
				}

				public string GroupId
				{
					get
					{
						return groupId;
					}
					set	
					{
						groupId = value;
					}
				}

				public float? FaceQuality
				{
					get
					{
						return faceQuality;
					}
					set	
					{
						faceQuality = value;
					}
				}

				public FindImagesByTagNames_EmotionDetails EmotionDetails
				{
					get
					{
						return emotionDetails;
					}
					set	
					{
						emotionDetails = value;
					}
				}

				public FindImagesByTagNames_FaceAttributes FaceAttributes
				{
					get
					{
						return faceAttributes;
					}
					set	
					{
						faceAttributes = value;
					}
				}

				public class FindImagesByTagNames_EmotionDetails
				{

					private float? sAD;

					private float? cALM;

					private float? aNGRY;

					private float? hAPPY;

					private float? sCARED;

					private float? dISGUSTED;

					private float? sURPRISED;

					public float? SAD
					{
						get
						{
							return sAD;
						}
						set	
						{
							sAD = value;
						}
					}

					public float? CALM
					{
						get
						{
							return cALM;
						}
						set	
						{
							cALM = value;
						}
					}

					public float? ANGRY
					{
						get
						{
							return aNGRY;
						}
						set	
						{
							aNGRY = value;
						}
					}

					public float? HAPPY
					{
						get
						{
							return hAPPY;
						}
						set	
						{
							hAPPY = value;
						}
					}

					public float? SCARED
					{
						get
						{
							return sCARED;
						}
						set	
						{
							sCARED = value;
						}
					}

					public float? DISGUSTED
					{
						get
						{
							return dISGUSTED;
						}
						set	
						{
							dISGUSTED = value;
						}
					}

					public float? SURPRISED
					{
						get
						{
							return sURPRISED;
						}
						set	
						{
							sURPRISED = value;
						}
					}
				}

				public class FindImagesByTagNames_FaceAttributes
				{

					private float? glassesConfidence;

					private string glasses;

					private float? raceConfidence;

					private string beard;

					private float? maskConfidence;

					private string race;

					private float? beardConfidence;

					private string mask;

					private FindImagesByTagNames_FaceBoundary faceBoundary;

					private FindImagesByTagNames_HeadPose headPose;

					public float? GlassesConfidence
					{
						get
						{
							return glassesConfidence;
						}
						set	
						{
							glassesConfidence = value;
						}
					}

					public string Glasses
					{
						get
						{
							return glasses;
						}
						set	
						{
							glasses = value;
						}
					}

					public float? RaceConfidence
					{
						get
						{
							return raceConfidence;
						}
						set	
						{
							raceConfidence = value;
						}
					}

					public string Beard
					{
						get
						{
							return beard;
						}
						set	
						{
							beard = value;
						}
					}

					public float? MaskConfidence
					{
						get
						{
							return maskConfidence;
						}
						set	
						{
							maskConfidence = value;
						}
					}

					public string Race
					{
						get
						{
							return race;
						}
						set	
						{
							race = value;
						}
					}

					public float? BeardConfidence
					{
						get
						{
							return beardConfidence;
						}
						set	
						{
							beardConfidence = value;
						}
					}

					public string Mask
					{
						get
						{
							return mask;
						}
						set	
						{
							mask = value;
						}
					}

					public FindImagesByTagNames_FaceBoundary FaceBoundary
					{
						get
						{
							return faceBoundary;
						}
						set	
						{
							faceBoundary = value;
						}
					}

					public FindImagesByTagNames_HeadPose HeadPose
					{
						get
						{
							return headPose;
						}
						set	
						{
							headPose = value;
						}
					}

					public class FindImagesByTagNames_FaceBoundary
					{

						private int? top;

						private int? height;

						private int? width;

						private int? left;

						public int? Top
						{
							get
							{
								return top;
							}
							set	
							{
								top = value;
							}
						}

						public int? Height
						{
							get
							{
								return height;
							}
							set	
							{
								height = value;
							}
						}

						public int? Width
						{
							get
							{
								return width;
							}
							set	
							{
								width = value;
							}
						}

						public int? Left
						{
							get
							{
								return left;
							}
							set	
							{
								left = value;
							}
						}
					}

					public class FindImagesByTagNames_HeadPose
					{

						private float? pitch;

						private float? roll;

						private float? yaw;

						public float? Pitch
						{
							get
							{
								return pitch;
							}
							set	
							{
								pitch = value;
							}
						}

						public float? Roll
						{
							get
							{
								return roll;
							}
							set	
							{
								roll = value;
							}
						}

						public float? Yaw
						{
							get
							{
								return yaw;
							}
							set	
							{
								yaw = value;
							}
						}
					}
				}
			}

			public class FindImagesByTagNames_TagsItem
			{

				private float? tagConfidence;

				private int? tagLevel;

				private string tagName;

				private string parentTagName;

				public float? TagConfidence
				{
					get
					{
						return tagConfidence;
					}
					set	
					{
						tagConfidence = value;
					}
				}

				public int? TagLevel
				{
					get
					{
						return tagLevel;
					}
					set	
					{
						tagLevel = value;
					}
				}

				public string TagName
				{
					get
					{
						return tagName;
					}
					set	
					{
						tagName = value;
					}
				}

				public string ParentTagName
				{
					get
					{
						return parentTagName;
					}
					set	
					{
						parentTagName = value;
					}
				}
			}

			public class FindImagesByTagNames_OCRItem
			{

				private string oCRContents;

				private float? oCRConfidence;

				private FindImagesByTagNames_OCRBoundary oCRBoundary;

				public string OCRContents
				{
					get
					{
						return oCRContents;
					}
					set	
					{
						oCRContents = value;
					}
				}

				public float? OCRConfidence
				{
					get
					{
						return oCRConfidence;
					}
					set	
					{
						oCRConfidence = value;
					}
				}

				public FindImagesByTagNames_OCRBoundary OCRBoundary
				{
					get
					{
						return oCRBoundary;
					}
					set	
					{
						oCRBoundary = value;
					}
				}

				public class FindImagesByTagNames_OCRBoundary
				{

					private int? left;

					private int? left1;

					private int? width;

					private int? height;

					public int? Left
					{
						get
						{
							return left;
						}
						set	
						{
							left = value;
						}
					}

					public int? Left1
					{
						get
						{
							return left1;
						}
						set	
						{
							left1 = value;
						}
					}

					public int? Width
					{
						get
						{
							return width;
						}
						set	
						{
							width = value;
						}
					}

					public int? Height
					{
						get
						{
							return height;
						}
						set	
						{
							height = value;
						}
					}
				}
			}

			public class FindImagesByTagNames_CelebrityItem
			{

				private string celebrityName;

				private string celebrityGender;

				private float? celebrityConfidence;

				private string celebrityLibraryName;

				private FindImagesByTagNames_CelebrityBoundary celebrityBoundary;

				public string CelebrityName
				{
					get
					{
						return celebrityName;
					}
					set	
					{
						celebrityName = value;
					}
				}

				public string CelebrityGender
				{
					get
					{
						return celebrityGender;
					}
					set	
					{
						celebrityGender = value;
					}
				}

				public float? CelebrityConfidence
				{
					get
					{
						return celebrityConfidence;
					}
					set	
					{
						celebrityConfidence = value;
					}
				}

				public string CelebrityLibraryName
				{
					get
					{
						return celebrityLibraryName;
					}
					set	
					{
						celebrityLibraryName = value;
					}
				}

				public FindImagesByTagNames_CelebrityBoundary CelebrityBoundary
				{
					get
					{
						return celebrityBoundary;
					}
					set	
					{
						celebrityBoundary = value;
					}
				}

				public class FindImagesByTagNames_CelebrityBoundary
				{

					private int? left;

					private int? top;

					private int? width;

					private int? height;

					public int? Left
					{
						get
						{
							return left;
						}
						set	
						{
							left = value;
						}
					}

					public int? Top
					{
						get
						{
							return top;
						}
						set	
						{
							top = value;
						}
					}

					public int? Width
					{
						get
						{
							return width;
						}
						set	
						{
							width = value;
						}
					}

					public int? Height
					{
						get
						{
							return height;
						}
						set	
						{
							height = value;
						}
					}
				}
			}

			public class FindImagesByTagNames_Address
			{

				private string addressLine;

				private string country;

				private string province;

				private string city;

				private string district;

				private string township;

				public string AddressLine
				{
					get
					{
						return addressLine;
					}
					set	
					{
						addressLine = value;
					}
				}

				public string Country
				{
					get
					{
						return country;
					}
					set	
					{
						country = value;
					}
				}

				public string Province
				{
					get
					{
						return province;
					}
					set	
					{
						province = value;
					}
				}

				public string City
				{
					get
					{
						return city;
					}
					set	
					{
						city = value;
					}
				}

				public string District
				{
					get
					{
						return district;
					}
					set	
					{
						district = value;
					}
				}

				public string Township
				{
					get
					{
						return township;
					}
					set	
					{
						township = value;
					}
				}
			}
		}
	}
}
