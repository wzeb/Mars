package ac.ict.menelaus.aiops.statics;

public enum ResponseCode {
	OK(0,"OK");	
	
	private Integer Code;
	private String Message;
		
	private ResponseCode(Integer code, String message) {
		Code = code;
		Message = message;
	}
	public Integer getCode() {
		return Code;
	}
	public void setCode(Integer code) {
		Code = code;
	}
	public String getMessage() {
		return Message;
	}
	public void setMessage(String message) {
		Message = message;
	}	
}
