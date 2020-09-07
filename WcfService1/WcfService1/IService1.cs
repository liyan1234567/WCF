using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        /// <summary>   
        /// 上传操作   
        /// </summary>   
        /// <param name="fileInfo"></param>   
        /// <returns></returns>   
        [OperationContract]
        CustomFileInfo UpLoadFile(CustomFileInfo fileInfo);

        /// <summary>   
        /// 文件重命名   
        /// </summary>     
        /// <returns></returns> 
        [OperationContract]
        void UpdataFileName(string oldName, string newName);

        /// <summary>   
        /// 删除文件操作   
        /// </summary>   
        /// <param name="fileName">文件名</param>   
        /// <returns></returns>   
        [OperationContract]
        void DeleteFile(string fileName);
        // TODO: 在此添加您的服务操作
    }

    /// <summary>   
    /// 自定义文件属性类   
    /// </summary>   
    [DataContract]
    public class CustomFileInfo
    {
        /// <summary>   
        /// 文件名称   
        /// </summary>   
        [DataMember]
        public string Name { get; set; }

        /// <summary>   
        /// 文件大小   
        /// </summary>   
        [DataMember]
        public long Length { get; set; }

        /// <summary>   
        /// 最后更新时间   
        /// </summary>   
        [DataMember]
        public DateTime UpdateTime { get; set; }

        /// <summary>   
        /// 文件偏移量   
        /// </summary>   
        [DataMember]
        public long OffSet { get; set; }

        /// <summary>   
        /// 发送的字节   
        /// </summary>   
        [DataMember]
        public byte[] SendByte { get; set; }
    }


    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
