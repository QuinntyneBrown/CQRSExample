using System;
using CQRSExample.Core.Entities;

namespace CQRSExample.API.Features.DigitalAssets
{
    public class DigitalAssetApiModel
    {
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? FileModified { get; set; }
        public string Size { get; set; }
        public string ContentType { get; set; }
        public bool? IsSecure { get; set; }
        public string RelativeUrl { get { return $"api/digitalassets/serve?digitalassetid={DigitalAssetId}"; } }
        public string Url { get { return $"http://localhost:26903/{RelativeUrl}";  } }
        public byte[] Bytes { get; set; } = new byte[0];
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UploadedOn { get; set; }
        public string UploadedBy { get; set; }
        public static DigitalAssetApiModel FromDigitalAsset(DigitalAsset digitalAsset)
        {
            var model = new DigitalAssetApiModel();
            model.DigitalAssetId = digitalAsset.DigitalAssetId;
            model.Folder = digitalAsset.Folder;
            model.Name = digitalAsset.Name;            
            model.FileName = digitalAsset.FileName;
            model.Description = digitalAsset.Description;
            model.Created = digitalAsset.Created;
            model.FileModified = digitalAsset.FileModified;
            model.Size = digitalAsset.Size;
            model.Bytes = digitalAsset.Bytes;
            model.ContentType = digitalAsset.ContentType;
            model.IsSecure = digitalAsset.IsSecure;
            model.CreatedOn = digitalAsset.CreatedOn;
            model.CreatedBy = digitalAsset.CreatedBy;
            model.UploadedOn = string.Format("{0:yyyy-MM-dd HH:mm}", digitalAsset.UploadedOn);
            model.UploadedBy = digitalAsset.UploadedBy;
            return model;
        }
    }
}