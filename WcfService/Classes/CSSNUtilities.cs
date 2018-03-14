using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace WcfService
{
    public static class CSSNUtilities
    {
        public static int GetRegionIntValue(string region)
        {
            switch (region)
            {
                case "United States":
                    return 0;
                case "Australia":
                    return 4;
                case "Asia":
                    return 5;
                case "Canada":
                    return 1;
                case "America":
                    return 2;
                case "Europe":
                    return 3;
                case "Africa":
                    return 7;
                case "General doc.":
                    return 6;
                default:
                    return -1;
            }
        }

        public static void Alert(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                MessageBox.Show(message, "Id Demo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool IsCalibrateButtonRequired(int scanner_type)
        {
            bool ret = false;
            if (scanner_type == CSSNConstants.CSSN_600 || scanner_type == CSSNConstants.CSSN_800 || scanner_type == CSSNConstants.CSSN_800N || scanner_type == CSSNConstants.CSSN_1000 || scanner_type == CSSNConstants.CSSN_2000 || scanner_type == CSSNConstants.CSSN_2000N || scanner_type == CSSNConstants.CSSN_800E || scanner_type == CSSNConstants.CSSN_800EN || scanner_type == CSSNConstants.CSSN_3000 || scanner_type == CSSNConstants.CSSN_800DX || scanner_type == CSSNConstants.CSSN_800DXN || scanner_type == CSSNConstants.CSSN_IP || scanner_type == CSSNConstants.CSSN_1000N || scanner_type == CSSNConstants.CSSN_3000DN || scanner_type == CSSNConstants.CSSN_3100 || scanner_type == CSSNConstants.CSSN_3100N)
            {
                ret = true;
            }
            return ret;
        }

        public static bool IsPassportScanner(int scanner_type)
        {
            bool ret = false;
            if ((scanner_type == CSSNConstants.CSSN_1000 || scanner_type == CSSNConstants.CSSN_1000N || scanner_type == CSSNConstants.CSSN_PASS ||
               scanner_type == CSSNConstants.CSSN_PASS_R2))
            {
                ret = true;
            }
            return ret;
        }

        public static Image _GetImageFromBuffer(string imagebuffer)
        {
            byte[] imgBuffer;
            // If image buffer found process buffer
            if (imagebuffer != null)
            {
                // Change image buffer to byte[] to be  able to save in DataBase.
                char[] tempBuffer = imagebuffer.ToCharArray();
                byte[] imageDataBuffer = new byte[tempBuffer.Length];
                for (int i = 0; i < tempBuffer.Length; i++)
                {
                    imageDataBuffer[i] = (byte)tempBuffer[i];
                }
                imgBuffer = imageDataBuffer;
                MemoryStream ms = new MemoryStream(imgBuffer);
                Image returnImage = Image.FromStream(ms);
                ms.Close();
                return returnImage;
            }
            else
            {
                return null;
            }
        }

        public static Image _GetImageFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return null;
            if (filePath.Contains(".ejpg"))
            {
                return null;
            }
            
            FileStream fs = new FileStream(filePath, FileMode.Open);
            Image returnImage = Image.FromStream(fs);
            fs.Close();
            fs = null;
            return returnImage;
        }
    }
}
