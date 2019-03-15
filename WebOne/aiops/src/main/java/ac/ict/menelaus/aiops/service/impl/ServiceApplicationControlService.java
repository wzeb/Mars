package ac.ict.menelaus.aiops.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import ac.ict.menelaus.aiops.dal.intf.IServiceApplicationControlDal;
import ac.ict.menelaus.aiops.object.dao.ServiceApplicationControl;
import ac.ict.menelaus.aiops.service.intf.IServiceApplicationControlService;

@Service
public class ServiceApplicationControlService implements IServiceApplicationControlService {

	@Resource
	private IServiceApplicationControlDal serviceApplicationControlDal;
	
	@Override
	public List<ServiceApplicationControl> showPage(String StartDate, String EndDate) {
		Map<String, Object> params = new HashMap<>();
		params.put("StartDate", StartDate);
		params.put("EndDate", EndDate);
		return serviceApplicationControlDal.selectByPage(params);
	}

}
