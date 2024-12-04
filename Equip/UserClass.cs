using System;
using System.Drawing;
using System.IO;

namespace Equip
{
    [Serializable]
    public class User
    {
        public string Name { get; set; }
        public string EquipmentInCharge { get; set; }

        // Store the image as byte array for serialization
        public byte[] PhotoData { get; set; }

        // Property to convert byte array back to Image
        public Image Photo
        {
            get => PhotoData != null ? Image.FromStream(new MemoryStream(PhotoData)) : null;
            set
            {
                using (var ms = new MemoryStream())
                {
                    value.Save(ms, System.Drawing.Imaging.ImageFormat.Png); // Save image as PNG
                    PhotoData = ms.ToArray(); // Convert image to byte array
                }
            }
        }
    }
}
