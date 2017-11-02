<Query Kind="Program">
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
</Query>

void Main()
{
	var file = @"c:\temp\azmon\0a1a4a8e-ac21-4390-b5cd-1ad049918298.json";
	var content = File.ReadAllText(file);
	
	//var obj = JsonConvert.DeserializeObject(content);
	//obj.Dump("record");
	
	var jobj = JObject.Parse(content);
	var records = (JArray)jobj["records"];
	foreach (var r in records)
	{
		r.Dump("record");
	}
	
}

// Define other methods and classes here
public class AzureRecordBase
{
	public DateTime time { get; set; }
	public string resourceId { get; set; }
	public string operationName { get; set; }
	public string category { get; set; }
	public string resultType { get; set; }
	public string resultSignature { get; set; }
	public int durationMs { get; set; }
	public string callerIpAddress { get; set; }
	public string correlationId { get; set; }
	public Identity identity { get; set; }
	public string level { get; set; }
	public string location { get; set; }
	public Properties properties { get; set; }
	
	/*
	public class Record
	{
		public DateTime time { get; set; }
		public string resourceId { get; set; }
		public string operationName { get; set; }
		public string category { get; set; }
		public string resultType { get; set; }
		public string resultSignature { get; set; }
		public int durationMs { get; set; }
		public string callerIpAddress { get; set; }
		public string correlationId { get; set; }
		public Identity identity { get; set; }
		public string level { get; set; }
		public string location { get; set; }
		public Properties properties { get; set; }
	}*/

}

/*
{
   "records": [
	   {
		   "time": "2017-09-18T22:57:56.0473211Z",
		   "resourceId": "/SUBSCRIPTIONS/3E9C25FC-55B3-4837-9BBA-02B6EB204331/RESOURCEGROUPS/MASDEMO-K8-RG/PROVIDERS/MICROSOFT.CONTAINERSERVICE/CONTAINERSERVICES/DEMOK8",
		   "operationName": "MICROSOFT.CONTAINERSERVICE/CONTAINERSERVICES/WRITE",
		   "category": "Write",
		   "resultType": "Accept",
		   "resultSignature": "Accepted.",
		   "durationMs": 0,
		   "callerIpAddress": "167.220.0.154",
		   "correlationId": "24c6d5c0-fa07-4b03-99a6-885854712d4f",
		   "identity": {
			   "authorization": {
				   "scope": "/subscriptions/3e9c25fc-55b3-4837-9bba-02b6eb204331/resourcegroups/masdemo-k8-rg/providers/Microsoft.ContainerService/containerServices/demok8",
				   "action": "Microsoft.ContainerService/containerServices/write"
			   },
			   "claims": {
				   "aud": "https://management.core.windows.net/",
				   "iss": "https://sts.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/",
				   "iat": "1505771927",
				   "nbf": "1505771927",
				   "exp": "1505775827",
				   "_claim_names": "{\"groups\":\"src1\"}",
				   "_claim_sources": "{\"src1\":{\"endpoint\":\"https://graph.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/users/a992d446-47d1-4579-96f0-496047c02d86/getMemberObjects\"}}",
				   "http://schemas.microsoft.com/claims/authnclassreference": "1",
				   "aio": "Y2VgYEi4+cpln8vZf5xcsW48Gll3Lh3rb/AMTGLvdzv5Uz3NgBEA",
				   "http://schemas.microsoft.com/claims/authnmethodsreferences": "rsa,mfa",
				   "appid": "04b07795-8ddb-461a-bbee-02f9e1bf7b46",
				   "appidacr": "0",
				   "http://schemas.microsoft.com/2012/01/devicecontext/claims/identifier": "da4bd1b0-b856-4eb5-992b-25920546f217",
				   "e_exp": "262800",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname": "Simms",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname": "Mark",
				   "ipaddr": "167.220.0.154",
				   "name": "Mark Simms",
				   "http://schemas.microsoft.com/identity/claims/objectidentifier": "a992d446-47d1-4579-96f0-496047c02d86",
				   "onprem_sid": "S-1-5-21-2127521184-1604012920-1887927527-4832699",
				   "puid": "10037FFE801BACCE",
				   "http://schemas.microsoft.com/identity/claims/scope": "user_impersonation",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "Jm5BEvNknJAUaafFTkkiQHn1hfFtdWpSrKYKXnXQJ3g",
				   "http://schemas.microsoft.com/identity/claims/tenantid": "72f988bf-86f1-41af-91ab-2d7cd011db47",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "masimms@microsoft.com",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn": "masimms@microsoft.com",
				   "uti": "21GBbliKbkm0zrqQV44BAA",
				   "ver": "1.0"
			   }
		   },
		   "level": "Information",
		   "location": "global",
		   "properties": {
			   "statusCode": "Accepted",
			   "statusMessage": "\"Resource provisioning is in progress.\""
		   }
	   },
	   {
		   "time": "2017-09-18T22:59:02.4933612Z",
		   "resourceId": "/SUBSCRIPTIONS/3E9C25FC-55B3-4837-9BBA-02B6EB204331/RESOURCEGROUPS/MASDEMO-K8-RG/PROVIDERS/MICROSOFT.CONTAINERSERVICE/CONTAINERSERVICES/DEMOK8",
		   "operationName": "MICROSOFT.CONTAINERSERVICE/CONTAINERSERVICES/WRITE",
		   "category": "Write",
		   "resultType": "Accept",
		   "resultSignature": "Accepted.",
		   "durationMs": 0,
		   "callerIpAddress": "167.220.0.154",
		   "correlationId": "24c6d5c0-fa07-4b03-99a6-885854712d4f",
		   "identity": {
			   "authorization": {
				   "scope": "/subscriptions/3e9c25fc-55b3-4837-9bba-02b6eb204331/resourcegroups/masdemo-k8-rg/providers/Microsoft.ContainerService/containerServices/demok8",
				   "action": "Microsoft.ContainerService/containerServices/write"
			   },
			   "claims": {
				   "aud": "https://management.core.windows.net/",
				   "iss": "https://sts.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/",
				   "iat": "1505771927",
				   "nbf": "1505771927",
				   "exp": "1505775827",
				   "_claim_names": "{\"groups\":\"src1\"}",
				   "_claim_sources": "{\"src1\":{\"endpoint\":\"https://graph.windows.net/72f988bf-86f1-41af-91ab-2d7cd011db47/users/a992d446-47d1-4579-96f0-496047c02d86/getMemberObjects\"}}",
				   "http://schemas.microsoft.com/claims/authnclassreference": "1",
				   "aio": "Y2VgYEi4+cpln8vZf5xcsW48Gll3Lh3rb/AMTGLvdzv5Uz3NgBEA",
				   "http://schemas.microsoft.com/claims/authnmethodsreferences": "rsa,mfa",
				   "appid": "04b07795-8ddb-461a-bbee-02f9e1bf7b46",
				   "appidacr": "0",
				   "http://schemas.microsoft.com/2012/01/devicecontext/claims/identifier": "da4bd1b0-b856-4eb5-992b-25920546f217",
				   "e_exp": "262800",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname": "Simms",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname": "Mark",
				   "ipaddr": "167.220.0.154",
				   "name": "Mark Simms",
				   "http://schemas.microsoft.com/identity/claims/objectidentifier": "a992d446-47d1-4579-96f0-496047c02d86",
				   "onprem_sid": "S-1-5-21-2127521184-1604012920-1887927527-4832699",
				   "puid": "10037FFE801BACCE",
				   "http://schemas.microsoft.com/identity/claims/scope": "user_impersonation",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": "Jm5BEvNknJAUaafFTkkiQHn1hfFtdWpSrKYKXnXQJ3g",
				   "http://schemas.microsoft.com/identity/claims/tenantid": "72f988bf-86f1-41af-91ab-2d7cd011db47",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": "masimms@microsoft.com",
				   "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn": "masimms@microsoft.com",
				   "uti": "21GBbliKbkm0zrqQV44BAA",
				   "ver": "1.0"
			   }
		   },
		   "level": "Information",
		   "location": "global",
		   "properties": {
			   "statusCode": "Accepted",
			   "statusMessage": "\"Resource provisioning is in progress.\""
		   }
	   }
   ]
}
*/
