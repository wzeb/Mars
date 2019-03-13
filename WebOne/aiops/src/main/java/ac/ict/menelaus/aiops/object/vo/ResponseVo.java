package ac.ict.menelaus.aiops.object.vo;

import java.io.Serializable;

public class ResponseVo implements Serializable{

	/**
	 * 
	 */
	private static final long serialVersionUID = -3761139257310644620L;
	
	private Integer Code;
	private String Message;
	private Object Data;
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
	public Object getData() {
		return Data;
	}
	public void setData(Object data) {
		Data = data;
	}
	
	
}
