﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IAuthenticationService")]
public interface IAuthenticationService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Authenticate", ReplyAction="http://tempuri.org/IAuthenticationService/AuthenticateResponse")]
    string Authenticate(string strCredentials);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Authenticate", ReplyAction="http://tempuri.org/IAuthenticationService/AuthenticateResponse")]
    System.Threading.Tasks.Task<string> AuthenticateAsync(string strCredentials);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Logout", ReplyAction="http://tempuri.org/IAuthenticationService/LogoutResponse")]
    void Logout(string strCredentials);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAuthenticationService/Logout", ReplyAction="http://tempuri.org/IAuthenticationService/LogoutResponse")]
    System.Threading.Tasks.Task LogoutAsync(string strCredentials);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IAuthenticationServiceChannel : IAuthenticationService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class AuthenticationServiceClient : System.ServiceModel.ClientBase<IAuthenticationService>, IAuthenticationService
{
    
    public AuthenticationServiceClient()
    {
    }
    
    public AuthenticationServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public AuthenticationServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public AuthenticationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public AuthenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string Authenticate(string strCredentials)
    {
        return base.Channel.Authenticate(strCredentials);
    }
    
    public System.Threading.Tasks.Task<string> AuthenticateAsync(string strCredentials)
    {
        return base.Channel.AuthenticateAsync(strCredentials);
    }
    
    public void Logout(string strCredentials)
    {
        base.Channel.Logout(strCredentials);
    }
    
    public System.Threading.Tasks.Task LogoutAsync(string strCredentials)
    {
        return base.Channel.LogoutAsync(strCredentials);
    }
}
