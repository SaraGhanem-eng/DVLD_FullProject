using DVLD.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.GLobal_Classes
{

    public class clsUtil
    {
        static string ImagesFolder = @"C:\DVLD-People-Images";


        static public  bool DeleteImage(string ImagePath)
        {
            if (string.IsNullOrEmpty(ImagePath))
                return true;

            string FullImagePath = Path.Combine(ImagesFolder, ImagePath);

            if (!File.Exists(FullImagePath))
                return true;

            try
            {
                File.Delete(FullImagePath);
                return true;
            }
            catch
            {
                return false;
            }
        }


        static public bool _SaveImage(string SelectedImagePath, ref string FileName)
        {
            if (string.IsNullOrEmpty(SelectedImagePath))
                return false;

            if (!Directory.Exists(ImagesFolder))
            {
                Directory.CreateDirectory(ImagesFolder);
            }

            string FileExtension = Path.GetExtension(SelectedImagePath);

            string NewFileName = Guid.NewGuid().ToString() + FileExtension;

            string NewImagePath = Path.Combine(ImagesFolder, NewFileName);

            try
            {
                File.Copy(SelectedImagePath, NewImagePath);

                FileName = NewFileName;

                return true;
            }
            catch
            {
                return false;
            }
        }



    }
}
