using System;
using System.IO;

namespace EntityHelper
{
    public class Class1
    {
        public static string UploadFile(string srcFile, string destFile)
        {
            string result = "true";
            FileInfo fileInfo;
            FileStream fs = null;
            try
            {
                fileInfo = new FileInfo(srcFile);
                // 要上传的文件地址   
                fs = File.OpenRead(fileInfo.FullName);
                var fileLength = fs.Length;
                // 实例化服务客户
                IFileService proxy = Init();

                using (proxy as IDisposable)
                {
                    int maxSize = 1024 * 1024;

                    string[] fileNames = destFile.Split('.');
                    string newName = string.Empty;
                    foreach (string item in fileNames)
                    {
                        if (item == fileNames[fileNames.Length - 2].ToString())
                        {
                            string toName = item + "_File.";
                            newName += toName;
                            continue;
                        }
                        newName += item + ".";
                    }
                    newName = newName.Substring(0, newName.Length - 1);
                    if (File.Exists(newName)) { DeleteFile(newName); }
                    CustomFileInfo customFileInfo = new CustomFileInfo();
                    customFileInfo.OffSet = 0;
                    customFileInfo.Name = newName;
                    customFileInfo.Length = fs.Length;

                    while (customFileInfo.Length != customFileInfo.OffSet)
                    {
                        customFileInfo.SendByte = new byte[customFileInfo.Length - customFileInfo.OffSet <= maxSize ? customFileInfo.Length - customFileInfo.OffSet : maxSize]; //设置传递的数据的大小   

                        fs.Position = customFileInfo.OffSet; //设置本地文件数据的读取位置   
                        fs.Read(customFileInfo.SendByte, 0, customFileInfo.SendByte.Length);//把数据写入到file.Data中   
                        customFileInfo = proxy.UpLoadFile(customFileInfo); //上传   
                    }
                    if (customFileInfo.Length == customFileInfo.OffSet && fileLength == customFileInfo.OffSet)
                    {
                        DeleteFile(destFile); //删掉老版本文件
                        UpFileNmae(newName, destFile); //更新新版本文件名
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
                //Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

            }
            return result;
        }
    }
}
