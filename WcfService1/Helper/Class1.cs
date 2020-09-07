using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WcfService1;

namespace Helper
{
    public class Class1: EntityHelper
    {
        public static string url = string.Format("{0}Service1.svc", WCF_SERVER_URL);
        //http://localhost:49278/Service1.svc
        /// <summary>    
        /// 服务初始化
        /// </summary>     
        /// <returns>返回服务代理</returns>   
        private static IService1 Init()
        {
            EndpointAddress address = new EndpointAddress(url);
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Security.Mode = BasicHttpSecurityMode.None;
            binding.TransferMode = TransferMode.Streamed;
            binding.MessageEncoding = WSMessageEncoding.Mtom;
            ChannelFactory<IService1> channelFactory = new ChannelFactory<IService1>(binding, address);
            return channelFactory.CreateChannel();
        }

        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="srcFile"></param>
        /// <param name="destFile"></param>
        /// <returns></returns>
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
                IService1 proxy = Init();

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

        /// <summary>    
        /// 删除远程文件
        /// </summary>
        /// <param name="fileName">远程文件名（含路径）</param>
        public static void DeleteFile(string fileName)
        {
            IService1 proxy = Init();
            using (proxy as IDisposable)
            {
                proxy.DeleteFile(fileName);
            }
        }

        /// <summary>    
        /// 更改远程文件名
        /// </summary>
        /// <param name="newName">远程文件名（含路径）</param>
        public static void UpFileNmae(string newName, string destFile)
        {
            IService1 proxy = Init();
            using (proxy as IDisposable)
            {
                proxy.UpdataFileName(newName, destFile);
            }
        }
    }
}
