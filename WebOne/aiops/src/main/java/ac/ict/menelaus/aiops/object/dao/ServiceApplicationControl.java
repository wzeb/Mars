package ac.ict.menelaus.aiops.object.dao;

import java.util.Date;

public class ServiceApplicationControl {
	private Integer Id;
	private Date Timestamp;
	private String RuleName;
	private String User;
	private String SourceIp;
	private Integer SourcePort;
	private String DestinationIp;
	private Integer DestinationPort;
	private String ApplicationType;
	private String ApplicationName;
	private String SystemAction;
	
	public Integer getId() {
		return Id;
	}
	public void setId(Integer id) {
		Id = id;
	}
	public Date getTimestamp() {
		return Timestamp;
	}
	public void setTimestamp(Date timestamp) {
		Timestamp = timestamp;
	}
	public String getRuleName() {
		return RuleName;
	}
	public void setRuleName(String ruleName) {
		RuleName = ruleName;
	}
	public String getUser() {
		return User;
	}
	public void setUser(String user) {
		User = user;
	}
	public String getSourceIp() {
		return SourceIp;
	}
	public void setSourceIp(String sourceIp) {
		SourceIp = sourceIp;
	}
	public Integer getSourcePort() {
		return SourcePort;
	}
	public void setSourcePort(Integer sourcePort) {
		SourcePort = sourcePort;
	}
	public String getDestinationIp() {
		return DestinationIp;
	}
	public void setDestinationIp(String destinationIp) {
		DestinationIp = destinationIp;
	}
	public Integer getDestinationPort() {
		return DestinationPort;
	}
	public void setDestinationPort(Integer destinationPort) {
		DestinationPort = destinationPort;
	}
	public String getApplicationType() {
		return ApplicationType;
	}
	public void setApplicationType(String applicationType) {
		ApplicationType = applicationType;
	}
	public String getApplicationName() {
		return ApplicationName;
	}
	public void setApplicationName(String applicationName) {
		ApplicationName = applicationName;
	}
	public String getSystemAction() {
		return SystemAction;
	}
	public void setSystemAction(String systemAction) {
		SystemAction = systemAction;
	}	
}
