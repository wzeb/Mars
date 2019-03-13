package ac.ict.menelaus.aiops.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.annotation.Resource;

import org.springframework.stereotype.Service;

import ac.ict.menelaus.aiops.dal.impl.IPSDal;
import ac.ict.menelaus.aiops.object.dao.IPS;
import ac.ict.menelaus.aiops.service.intf.IIPSService;

@Service
public class IPSService implements IIPSService {

	@Resource
	private IPSDal iPSDal;
	
	@Override
	public List<IPS> showPage(Integer offset, Integer count) {
		// TODO Auto-generated method stub
		Map<String, Object> params = new HashMap<>();
		return iPSDal.selectByPage(params);
	}

}
