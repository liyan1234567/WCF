﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lesson1Test.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IFlyService")]
    public interface IFlyService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFlyService/Fly", ReplyAction="http://tempuri.org/IFlyService/FlyResponse")]
        string Fly();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFlyService/Fly", ReplyAction="http://tempuri.org/IFlyService/FlyResponse")]
        System.Threading.Tasks.Task<string> FlyAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFlyServiceChannel : Lesson1Test.ServiceReference1.IFlyService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FlyServiceClient : System.ServiceModel.ClientBase<Lesson1Test.ServiceReference1.IFlyService>, Lesson1Test.ServiceReference1.IFlyService {
        
        public FlyServiceClient() {
        }
        
        public FlyServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FlyServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FlyServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FlyServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string Fly() {
            return base.Channel.Fly();
        }
        
        public System.Threading.Tasks.Task<string> FlyAsync() {
            return base.Channel.FlyAsync();
        }
    }
}
