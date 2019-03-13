package ac.ict.menelaus.aiops.utils;

import ac.ict.menelaus.aiops.object.vo.ResponseVo;
import ac.ict.menelaus.aiops.statics.ResponseCode;

public final class WebUtils {

	public static ResponseVo Response(ResponseCode code) {
		return Response(code, null);
	}
	
	public static ResponseVo Response(Object data) {
		return Response(ResponseCode.OK, data);
	}
	
	public static ResponseVo Response(ResponseCode code, Object data) {
		ResponseVo vo = new ResponseVo();
		vo.setCode(code.getCode());
		vo.setMessage(code.getMessage());
		vo.setData(data);
		return vo;
	}
}
