using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
  public class ImageDB
  {
    public int ImageId { get; set; }
    public string ImageName { get; set; }
    public string GroupName { get; set; }
    public string UserName { get; set; }
    public string Description { get; set; }
    public byte[] ImageObj { get; set; }

    public ImageDB(int imageId, string imageName, string groupName, string userName, string description, byte[] image)
    {
      ImageId = imageId;
      ImageName = imageName;
      GroupName = groupName;
      UserName = userName;
      Description = description;
      ImageObj = image;
    }
  }
}
