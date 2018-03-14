using System;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;


namespace WcfService
{
    public static class CSSNCore
    {
        public static string license
        {
            get
            {
                return CSSNCore.GetLicenseValue();
            }
        }

        public static string tempFolderPath
        {
            get
            {
                return CSSNCore.GetSettingsValue("TempFolderPath");
            }
        }

        public static string imageSuffix
        {
            get
            {
                return CSSNCore.GetSettingsValue("ImageFileSuffix");
            }
        }

        private static string _assemblyName
        {
            get
            {
                string[] _assemblyNameArray = Assembly.GetEntryAssembly().EntryPoint.ReflectedType.ToString().Split('.');
                return _assemblyNameArray[0];
            }
        }

        public static string GetSettingsValue(string value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.Settings", Assembly.GetExecutingAssembly()).GetString(value);
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetLicenseValue()
        {
            string ret_license_value = string.Empty;
            InputBox obj = new InputBox();
            {
                string filename = (Path.Combine(Environment.CurrentDirectory, @"SDKLicense.txt"));
                if (File.Exists(filename))
                {
                    using (StreamReader sr = File.OpenText(filename))
                    {
                        ret_license_value = sr.ReadLine().Trim();
                    }
                }
                else
                {
                    string value = string.Empty;
                    bool loop_flag = false;
                    string msg = string.Empty;

                    DialogResult diagRes;
                    do
                    {
                        if (!loop_flag)
                        {
                            msg = "SDK License file not found." + Environment.NewLine + "Please enter the SDK license key.";
                        }
                        else
                        {
                            if (ret_license_value.Equals(string.Empty))
                                msg = "The textbox cannot be empty." + Environment.NewLine + "Please enter the SDK license key.";
                            else
                                msg = "The license key should be 16 characters long." + Environment.NewLine + "Please enter the correct SDK license key.";
                        }
                        diagRes = obj.Show(msg, ref value);
                        if (diagRes == DialogResult.OK)
                        {
                            loop_flag = true;
                            ret_license_value = value.Trim();
                        }
                        else
                        {
                            obj.FormClose();
                            Application.Exit();
                        }
                    }
                    while ((ret_license_value.Equals(string.Empty)) || (ret_license_value.Length != 16));

                    FileStream fs = File.Open(filename, FileMode.CreateNew, FileAccess.Write);
                    StreamWriter sw = null;
                    sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
                    sw.WriteLine(ret_license_value);
                    sw.Close();
                }
            }

            return ret_license_value;
        }

        public static string GetImageClassError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.ImageErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetScannerClassError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.SlibErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetBarcodeClassError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.BarcodeErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetIdDataClassError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.IdDataErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetMagClassError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.MagErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetPassportErrors(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.PassportErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }

        public static string GetRFIDPassportError(int value)
        {
            string errorValue = new ResourceManager(_assemblyName + ".Resources.RFIDErrors", Assembly.GetExecutingAssembly()).GetString(value.ToString());
            if (string.IsNullOrEmpty(errorValue))
            {
                return (value.ToString());
            }
            else
            {
                return errorValue;
            }
        }
    }
}

public class InputBox
{
    Form form;
    TextBox textBox;
    Label label;
    Button buttonOk;
    Button buttonCancel;
    public InputBox()
    {
        form = new Form();
        textBox = new TextBox();
        label = new Label();
        buttonOk = new Button();
        buttonCancel = new Button();
        form.Text = "SDK License Key";
       
        buttonOk.Text = "OK";
        buttonCancel.Text = "Cancel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        textBox.MaxLength = 16;

        label.SetBounds(9, 20, 250, 26);
        textBox.SetBounds(12, 50, 120, 20);
        buttonOk.SetBounds(12, 80, 75, 23);
        buttonCancel.SetBounds(100, 80, 75, 23);

        form.ClientSize = new System.Drawing.Size(190, 107);
        form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        form.StartPosition = FormStartPosition.CenterScreen;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;
        form.FormBorderStyle = FormBorderStyle.FixedSingle;
        form.ControlBox = false;
    }

    public DialogResult Show(string msg, ref string value)
    {
        label.Text = msg;
        DialogResult dialogResult = form.ShowDialog();
        value = textBox.Text;
        return dialogResult;
    }
    public void FormClose()
    {
        form.Close();

    }
}