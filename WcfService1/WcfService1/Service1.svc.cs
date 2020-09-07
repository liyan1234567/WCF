using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 Service1.svc 或 Service1.svc.cs，然后开始调试。
    public class Service1 : IService1
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        public CustomFileInfo UpLoadFile(CustomFileInfo fileInfo)
        {
            // 获取服务器文件上传路径
            // string fileUpLoadPath = System.Web.Hosting.HostingEnvironment.MapPath("~/UpLoadFile/");
            string fileUpLoadPath = Path.GetDirectoryName(fileInfo.Name);
            // 如需指定新的文件夹，需要进行创建操作。
            if (!Directory.Exists(fileUpLoadPath))
            {
                DirectoryInfo dir = new DirectoryInfo(fileUpLoadPath);
                dir.Create();
            }
            // 创建FileStream对象   
            FileStream fs = new FileStream(fileInfo.Name, FileMode.OpenOrCreate);

            long offSet = fileInfo.OffSet;
            // 使用提供的流创建BinaryWriter对象   
            var binaryWriter = new BinaryWriter(fs, Encoding.UTF8);

            binaryWriter.Seek((int)offSet, SeekOrigin.Begin);
            binaryWriter.Write(fileInfo.SendByte);
            fileInfo.OffSet = fs.Length;
            fileInfo.SendByte = null;

            binaryWriter.Close();
            fs.Close();
            return fileInfo;
        }

        /// <summary>   
        /// 删除文件   
        /// </summary>   
        public void DeleteFile(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// 更改文件名
        /// </summary>
        /// <param name="oldName"></param>
        /// <param name="newName"></param>
        public void UpdataFileName(string oldName, string newName)
        {
            if (File.Exists(oldName))
            {
                File.Move(oldName, newName);
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
