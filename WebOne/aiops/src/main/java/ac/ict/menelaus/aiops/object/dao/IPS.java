package ac.ict.menelaus.aiops.object.dao;

import java.util.Date;

public class IPS {
	private Integer Id;
	private Date Timestamp;
	private String RuleName;
	private Integer VulnerabilityId;
	private String VulerabilityName;
	private String SourceIp;
	private Integer SourcePort;
	private String DestinationIp;
	private Integer DestinationPort;
	private String Protocol;
	private String AttackType;
	private String Severity;
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
	public Integer getVulnerabilityId() {
		return VulnerabilityId;
	}
	public void setVulnerabilityId(Integer vulnerabilityId) {
		VulnerabilityId = vulnerabilityId;
	}
	public String getVulerabilityName() {
		return VulerabilityName;
	}
	public void setVulerabilityName(String vulerabilityName) {
		VulerabilityName = vulerabilityName;
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
	public void setDestinationIp(String destinationId) {
		DestinationIp = destinationId;
	}
	public Integer getDestinationPort() {
		return DestinationPort;
	}
	public void setDestinationPort(Integer destinationPort) {
		DestinationPort = destinationPort;
	}
	public String getProtocol() {
		return Protocol;
	}
	public void setProtocol(String protocol) {
		Protocol = protocol;
	}
	public String getAttackType() {
		return AttackType;
	}
	public void setAttackType(String attackType) {
		AttackType = attackType;
	}
	public String getSeverity() {
		return Severity;
	}
	public void setSeverity(String severity) {
		Severity = severity;
	}
	public String getSystemAction() {
		return SystemAction;
	}
	public void setSystemAction(String systemAction) {
		SystemAction = systemAction;
	}
}
