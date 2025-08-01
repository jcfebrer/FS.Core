using System;

namespace FSMail
{
    public class MimeType
    {
        public MimeType()
        {
        }

        public struct MediaTypeCvt
        {
            public MediaType nMediaType;
            public string pszSubType;
            public string pszFileExt;
            public MediaTypeCvt(MediaType nMediaType, string pszSubType, string pszFileExt)
            {
                this.nMediaType = nMediaType;
                this.pszSubType = pszSubType;
                this.pszFileExt = pszFileExt;
            }
        }

        public enum MediaType
        {
            MEDIA_TEXT = 0, MEDIA_IMAGE, MEDIA_AUDIO, MEDIA_VIDEO, MEDIA_APPLICATION,
            MEDIA_MULTIPART, MEDIA_MESSAGE,
            MEDIA_UNKNOWN
        }

        public static readonly string[] TypeTable = { "text", "image", "audio", "video", "application", "multipart", "message", null };

        public static readonly MediaTypeCvt[] TypeCvtTable =
            new MediaTypeCvt[] {
								   // media-type, sub-type, file extension
								   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "xml", "xml" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "msword", "doc" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "rtf", "rtf" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "vnd.ms-excel", "xls" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "vnd.ms-powerpoint", "ppt" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "pdf", "pdf" ),
                                   new MediaTypeCvt( MediaType.MEDIA_APPLICATION, "zip", "zip" ),

                                   new MediaTypeCvt( MediaType.MEDIA_IMAGE, "jpeg", "jpeg" ),
                                   new MediaTypeCvt( MediaType.MEDIA_IMAGE, "jpeg", "jpg" ),
                                   new MediaTypeCvt( MediaType.MEDIA_IMAGE, "gif", "gif" ),
                                   new MediaTypeCvt( MediaType.MEDIA_IMAGE, "tiff", "tif" ),
                                   new MediaTypeCvt( MediaType.MEDIA_IMAGE, "tiff", "tiff" ),

                                   new MediaTypeCvt( MediaType.MEDIA_AUDIO, "basic", "wav" ),
                                   new MediaTypeCvt( MediaType.MEDIA_AUDIO, "basic", "mp3" ),

                                   new MediaTypeCvt( MediaType.MEDIA_VIDEO, "mpeg", "mpg" ),
                                   new MediaTypeCvt( MediaType.MEDIA_VIDEO, "mpeg", "mpeg" ),

                                   new MediaTypeCvt( MediaType.MEDIA_UNKNOWN, "", "" )		// add new subtypes before this line
							   };
    }
}
