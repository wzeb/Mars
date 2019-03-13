package ac.ict.menelaus.aiops.service.impl;

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
	public ServiceApplicationControl showPage(Integer offset, Integer count) {
		// TODO Auto-generated method stub
		return null;
	}

}
